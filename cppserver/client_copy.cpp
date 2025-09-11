#include <iostream> // for std::cout, std::cin
#include <string>   // for std::string
#include <thread>   // for std::thread
#include <vector>   // for std::vector
#include <array>    // for std::array
#include <mutex>    // for std::mutex

#include <errno.h>      // for errno
#include <stdlib.h>     // for EXIT_FAILURE
#include <unistd.h>     // for close
#include <arpa/inet.h>  // for inet_addr
#include <netinet/in.h> // for sockaddr_in
#include <sys/socket.h> // for socket, connect, send, recv
#include <sys/types.h>  // for socket types
#include <fcntl.h>      // for fcntl, O_NONBLOCK

// 전역 변수 및 상수
static const int SERVER_PORT = 12345;
static const std::string SERVER_IP = "127.0.0.1";//"0.0.0.0";//"127.0.0.1"; // 서버가 실행되는 IP 주소 (여기서는 로컬호스트)
//포트 열기+포트 포워딩 해야 접근 가능
static const int BUFFERSIZE = 1024;
static std::mutex cout_mutex; // 다중 스레드에서 cout 사용 시 충돌 방지

// 오류 발생 시 메시지 출력 및 종료
void error_exit(const char *msg) {
    // std::cerr는 버퍼링되지 않으므로 즉시 출력에 유리
    std::cerr << "Error: " << msg << " (errno: " << errno << ")" << std::endl;
    exit(EXIT_FAILURE);
}

// 소켓을 논블로킹 모드로 설정
int set_nonblock(int fd) {
    int flags;
    if (-1 == (flags = fcntl(fd, F_GETFL, 0))) {
        std::cerr << "if -1\n";
        flags = 0; // fcntl이 실패하면 0으로 초기화
    }else{
        std::cerr << "else -1\n";
    }
    return fcntl(fd, F_SETFL, flags | O_NONBLOCK);
}

// 서버로부터 메시지를 수신하는 스레드 함수
void receive_messages(int sock) {
    std::array<char, BUFFERSIZE> buffer;
    while (true) {
        buffer.fill(0); // 버퍼 초기화
        ssize_t bytes_received = recv(sock, buffer.data(), BUFFERSIZE - 1, 0); // -1은 널 문자 공간 확보
        
        if (bytes_received > 0) {
            buffer[bytes_received] = '\0'; // 문자열 끝에 널 문자 추가
            std::lock_guard<std::mutex> guard(cout_mutex);
            std::cout << "\r" << std::string(buffer.data()) << std::endl; // 받은 메시지 출력
            std::cout << "> "; // 사용자 입력 프롬프트 재출력 (메시지 덮어쓰기 방지)
            std::cout.flush(); // 즉시 출력
        } else if (bytes_received == 0) {
            // 서버가 연결을 닫음
            std::lock_guard<std::mutex> guard(cout_mutex);
            std::cout << "Server disconnected." << std::endl;
            break; // 스레드 종료
        } else { // bytes_received < 0
            if (errno == EAGAIN || errno == EWOULDBLOCK) {
                // 논블로킹 소켓에서 아직 데이터가 없음. 잠시 대기 후 재시도
                std::this_thread::sleep_for(std::chrono::milliseconds(100));
                continue;
            } else {
                // 다른 오류 발생
                std::lock_guard<std::mutex> guard(cout_mutex);
                std::cerr << "Error receiving data from server (errno: " << errno << ")." << std::endl;
                break; // 스레드 종료
            }
        }
    }
}

int main() {
    int client_sock;
    struct sockaddr_in server_addr;
    
    // 1. 소켓 생성
    client_sock = socket(AF_INET, SOCK_STREAM, 0);//1, 2인자로 인해 명시적으로 0을 입력해도 IPPROTO_TCP임
    //AF_INET는 IPv4 주소 체계를 사용, SOCK_STREAM: 순서대로 데이터 전달(연결지향형 소켓)
    if (client_sock == -1) {
        error_exit("Failed to create socket");
    }
    // 2. 서버 주소 설정
    server_addr.sin_family = AF_INET;
    server_addr.sin_port = htons(SERVER_PORT);
    if (inet_pton(AF_INET, SERVER_IP.c_str(), &server_addr.sin_addr) <= 0) {
        // IPv4 와 IPv6 주소를 binary 형태로 변환 하는 기능
        error_exit("Invalid address/ Address not supported");
    }
    // 3. 서버에 연결
    if (connect(client_sock, (struct sockaddr *)&server_addr, sizeof(server_addr)) == -1) {
        error_exit("Failed to connect to server");
    }
    
    
    // 4. 소켓을 논블로킹 모드로 설정 (recv 스레드에서 블로킹되지 않도록)
    if (set_nonblock(client_sock) == -1) {
        error_exit("Failed to set socket to non-blocking mode");
    }
    // 5. 메시지 수신을 위한 별도 스레드 시작
    // 클라이언트 소켓의 수신 부분만 논블로킹으로 설정했으므로,
    // 여기서 recv는 데이터를 기다리다가 블록될 수 있으나,
    // 별도 스레드로 분리하여 메인 스레드는 메시지 전송에 집중하도록 함.
    std::thread receiver_thread(receive_messages, client_sock);
    receiver_thread.detach(); // 메인 스레드와 독립적으로 실행되도록 분리

    // 6. 사용자로부터 메시지를 입력받아 서버로 전송
    std::string message;
    while (true) {
        std::cout << "> ";
        std::getline(std::cin, message); // 사용자 입력 받기

        if (message == "/quit") { // 종료 명령어
            std::cout << "Disconnecting from server." << std::endl;
            break;
        }
        
        // 메시지 전송
        ssize_t bytes_sent = send(client_sock, message.c_str(), message.length(), MSG_NOSIGNAL);
        if (bytes_sent == -1) {
            if (errno == EAGAIN || errno == EWOULDBLOCK) {
                // 논블로킹 소켓에서 버퍼가 가득 참. 잠시 대기 후 재시도 또는 오류 처리
                // 여기서는 간단하게 다시 시도하지 않고 오류 메시지 출력
                std::cerr << "Warning: Send buffer full, message might not have been sent immediately." << std::endl;
            } else {
                std::cerr << "Error sending data (errno: " << errno << ")." << std::endl;
                break; // 전송 오류 시 루프 종료
            }
        }
    }
    // 7. 소켓 닫기
    close(client_sock);
    return 0;
}