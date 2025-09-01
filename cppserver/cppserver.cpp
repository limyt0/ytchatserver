#include "cppserver.h"

namespace YT{
     namespace Server{

         static const int PORT = 2345;
         static const int MAXEVENTS = 32;
         static const int BUFFERSIZE = 1024;
         static std::set<int> activeUsers;
         static std::vector<std::string> messages;
         static std::map<int, std::string> userNames;
         

         SManager::SManager() { }
         SManager::~SManager(){ }
         
         void SManager::error_exit(const char *msg)
         {
             std::cerr << "Error: " << msg << std::endl;
             std::cerr << "System Error Code: " << errno << std::endl;
             std::cerr << "Error Message: " << strerror(errno) << std::endl;
             error(EXIT_FAILURE, errno, "%s\n", msg);
         }
         
         //논블록 설정
         int SManager::set_nonblock(int fd)
         {
         #if defined(O_NONBLOCK)
             int flags;
             if((flags = fcntl(fd, F_GETFL, 0))) 
                 flags = 0;
             //fcntl는 파일 컨트롤 , File Descriptor, FD
             //F_GETFD : fd 파일 상태 플래그들을 조회
             return fcntl(fd, F_SETFL, flags | O_NONBLOCK);        
         #else
             int flags = 1;
             //return ioctl(fd, FIOBIO, &flags);
             return ioctl(fd, FIONBIO, &flags); // 입출력(Input/Oupt)장치의 제어(Contol)
             //FIONBIO는 non blocking모드로 설정하는 것
         #endif
         }
         
         
         void SManager::configure_server_socket(int serverSocket)
         {
             //serverSocket는 fd로 정의되어있음.
             struct sockaddr_in socketAddress;
             socketAddress.sin_family = AF_INET; // IPv4 주소 체계를 사용
             socketAddress.sin_port = htons(PORT); // htons 함수는 호스트 바이트 순서를 네트워크 바이트 순서로 변환(short)
             socketAddress.sin_addr.s_addr = htonl(INADDR_ANY);  //htons 함수는 호스트 바이트 순서를 네트워크 바이트 순서로 변환(long)
             //INADDR_ANY는 0.0.0.0를 의미
             if(bind(serverSocket, (struct sockaddr *)(&socketAddress), sizeof(sockaddr)) == -1)
                 error_exit("error in bind()");
             //int bind(int sockfd, const struct sockaddr *addr, socklen_t addrlen);
         
             if(set_nonblock(serverSocket) == -1)
                 error_exit("cannot unblock the master socket");
             if(listen(serverSocket, SOMAXCONN) == -1) //서버 소켓을 클라이언트의 연결 요청을 수신 대기하는 상태로 
                 error_exit("cannot listen");
                 //SOMAXCONN은 시스템에서 허용하는 최대 동시 연결 대기 수
         }
         
         
         void SManager::register_in_epoll(int sock, int epollId)
         {
             struct epoll_event event; //epoll이 모니터링할 이벤트의 종류와 해당 이벤트가 발생했을 때 어떤 파일 디스크립터(소켓)와 연결될지를 정의
             event.data.fd = sock; //epoll은 이 data 멤버를 통해 어떤 파일 디스크립터에 이벤트가 발생했는지 알려줌
             event.events = EPOLLIN; //EPOLLIN은 해당 파일 디스크립터(소켓)에 읽을 데이터가 있거나(readable) 연결 요청(accept)이 들어왔을 때 알림을 받겠다는 의미
             if (epoll_ctl(epollId, EPOLL_CTL_ADD, sock, &event) == -1) //epoll에 fd들을 등록/수정/삭제를 하는 함수. sock을 epollId로 지정된 epoll 인스턴스에 등록
                         error_exit("cannot register in epoll");
             
             
             
         }
         
         
         void SManager::register_new_client(int serverSocket, int epollId)
         {
             //socklen_t 는 소켓 관련 매개 변수에 사용되는 것으로 길이 및 크기 값에 대한 정의를 내려준다
             socklen_t clientAddrSize = sizeof(struct sockaddr_in); // struct sockaddr_in socketAddress;를 통해 클라이언트의 주소 정보를 저장할 변수를 준비
             struct sockaddr_in socketAddress;
             int clientSocket = accept(serverSocket, (struct sockaddr *) &socketAddress, &clientAddrSize);
             //accept 함수는 새로운 클라이언트와 통신하는 데 사용될 **새로운 소켓 디스크립터(clientSocket)**를 반환. 이 소켓은 serverSocket과는 독립적이며,
             // 이후 클라이언트와의 개별적인 통신에 사용
             if (clientSocket == -1)
                 error_exit("cannot accept new client");
         
             if (set_nonblock(clientSocket))
                 error_exit("cannot unblock");
         
             activeUsers.insert(clientSocket);//set<int>로 정의됨
             char *ipAddress = inet_ntoa(socketAddress.sin_addr);
             //클라이언트의 IP 주소(네트워크 바이트 순서)를 사람이 읽을 수 있는 문자열 형식으로 변환
             //ipAddresses.insert(std::make_pair(clientSocket, ipAddress)); // map<int,string>
             //clientSocket과 해당 클라이언트의 IP 주소 문자열을 매핑하여 저장합니다.
             //특정 클라이언트 소켓에 대한 IP 정보를 나중에 조회할 때 유용
             register_in_epoll(clientSocket, epollId);
             
             //std::string message = std::to_string(clientSocket) + "님이 입장하셨습니다.";
             //messages.push_back(message);
         }
         
         
         void SManager::serve_client(int clientSocket)
         {
         
             std::array<char, BUFFERSIZE> buffer = {};
             int recvSize = recv(clientSocket, buffer.data(), BUFFERSIZE, MSG_NOSIGNAL);
             //MSG_NOSIGNAL: 데이터 송수신 중 파이프가 끊어지는(소켓이 닫히는) 경우 SIGPIPE 시그널을 발생시키지 않도록 함.
             //이 플래그가 없으면 클라이언트가 연결을 닫았을 때 서버 프로세스가 시그널로 인해 종료될 수 있음.
             
             if (recvSize <= 0 && errno != EAGAIN) {
                 if(userNames.find(clientSocket) != userNames.end()){
                     std::string message = userNames.find(clientSocket)->second + "님이 나가셨습니다.";
                     messages.push_back(message);
                     userNames.erase(clientSocket);
                 }
                 //클라이언트가 연결을 정상적으로 종료했거나 (0 반환), 데이터 수신 중 오류가 발생했음 (-1 반환)
                 //EAGAIN은 논블로킹 소켓에서 현재 읽을 데이터가 없음. EAGAIN이 아니라면 실제 오류나 연결 종료로 간주
                 shutdown(clientSocket, SHUT_RDWR); //lientSocket의 읽기 및 쓰기 기능을 모두 비활성화하여 소켓 통신을 종료
                 close(clientSocket); // 소켓 디스크립터를 닫고 관련 자원을 해제.
                 activeUsers.erase(clientSocket);
             } else if (recvSize > 0) {
                 if(userNames.find(clientSocket) != userNames.end()){
         
                     //std::string message = ipAddresses.find(clientSocket)->second + ": " + buffer.data();
                     //해당 clientSocket에 매핑된 클라이언트의 IP 주소 문자열을 가져옴
                     //std::string message = std::to_string(clientSocket) + ": " + buffer.data();
                     std::string message = "["+userNames.find(clientSocket)->second + "] : " + buffer.data();
                     //buffer.data(): 수신된 데이터를 문자열로 변환
                     //** 널 종료 문자열이 아닐 수 있으므로, std::string(buffer.data(), recvSize)와 같이 recvSize를 사용하여 정확한 크기만큼만 복사하는 것이 안전 */
                     messages.push_back(message);
                 }else{
                     std::string nickname = buffer.data();
                     userNames.insert(std::make_pair(clientSocket, nickname));//map<int,string>
                     //ipAddresses.insert(std::make_pair(clientSocket, ipAddress)); // map<int,string>
                     //buffer.data(): 수신된 데이터를 문자열로 변환
                     //** 널 종료 문자열이 아닐 수 있으므로, std::string(buffer.data(), recvSize)와 같이 recvSize를 사용하여 정확한 크기만큼만 복사하는 것이 안전 */
                     std::string message = nickname + "님이 입장하셨습니다." ;
                     messages.push_back(message);
                 }
             }
         }
         
         
         void SManager::send_messages()
         {
             std::string totalMessage = "";
             for (std::string &message : messages)
                 totalMessage += message;//메시지 추가
             size_t messageLength = totalMessage.size() + 1;
         
             for (int sock : activeUsers) {
                 send(sock, totalMessage.c_str(), messageLength, MSG_NOSIGNAL);
             }
             messages.clear();
         }
         
         
         int SManager::Init()
         {
         
             int serverSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);//소켓 생성
             //AF_INET는 IPv4 주소 체계를 사용, SOCK_STREAM: 순서대로 데이터 전달(연결지향형 소켓)
             if (serverSocket == -1)
                 error_exit("error in socket()");
             configure_server_socket(serverSocket);//bind nonblock listen
         
             // create epoll id
             int epollId = epoll_create1(0);
             //epoll_create()는 구식 메서드 epoll_create1(int size)를 통해 epoll 인스턴스 생성, fd 반환
             if (epollId == -1)
                 error_exit("error in epoll_create1()");
         
             // register server in epoll struct
             register_in_epoll(serverSocket, epollId); // event, epoll_ctl에 add
             
             // go to infinite cycle
             while (true) {
                 struct epoll_event currentEvents[MAXEVENTS]; //events = malloc(sizeof (struct epoll_event) * MAX_EVENTS);
                 int nEvents = epoll_wait(epollId, currentEvents, MAXEVENTS, -1);
                 //int  epoll_wait(int epfd, struct epoll_event * events, int maxevents, int timeout)
                 //실패: -1, 타임아웃:0, 성공시:fd갯수(이벤트 갯수)
                 for (int i = 0; i < nEvents; ++i) {
                     int sock = currentEvents[i].data.fd;
                     if (sock == serverSocket) {
                         // server socket gets request to join
                         register_new_client(serverSocket, epollId);
                     } else {
                         // client socket is ready to something
                         serve_client(sock);
                     }
                 }
                 send_messages();
             }
         
             // stop the server
             if (shutdown(serverSocket, SHUT_RDWR) == -1)
                 error_exit("error in shutdown()");
         
             if (close(serverSocket) == -1)
                 error_exit("error in close()");
             return EXIT_SUCCESS;
         }
     }
}
