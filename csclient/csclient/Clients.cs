using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace csclient
{
    public class Clients
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private const string ServerIp = "192.168.0.68";//"127.0.0.1"; // 서버 IP 주소를 여기에 입력하세요
        private const int ServerPort = 12345;
        private CancellationTokenSource _cancellationTokenSource;

        // 이벤트 정의 (Form1에서 구독할 수 있도록 public으로 선언)
        public event Action<string> MessageReceived;
        public event Action Connected;
        public event Action Disconnected;

        public enum Sendtypes {
            createroom, 
            deleteroom,
            roomin,
            roomout,
            roomchange,
            sendmessage,
            newclient
        };

        public async Task ConnectAsync()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                _client = new TcpClient();
                await _client.ConnectAsync(ServerIp, ServerPort);
                _stream = _client.GetStream();

                Connected?.Invoke(); // 연결 성공 이벤트 발생

                // 첫 메시지로 사용자 이름 전송
                //await SendMessageInternal(username + '\0'); // 널 종료 문자 직접 추가
                // SendMessageInternal은 private 헬퍼 메서드로 만들었습니다.

                // 메시지 수신 루프 시작
                _ = Task.Run(() => ReceiveMessagesAsync(_cancellationTokenSource.Token));
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"연결 오류: {ex.Message}"); // 콘솔에도 로그 남김
                Disconnected?.Invoke(); // 연결 끊김 이벤트 발생
            }
            catch (Exception ex)
            {
                Console.WriteLine($"오류 발생: {ex.Message}");
                Disconnected?.Invoke();
            }
        }

        public async Task SendDataAsync(Sendtypes sendtype,int roomnum ,string str)
        {
            if (_client == null || !_client.Connected)
            {
                Console.WriteLine("서버에 연결되어 있지 않습니다.");
                return;
            }

            try
            {
                // 사용자 메시지는 널 종료 문자를 포함하여 전송
                await SendDatasByteData(sendtype,roomnum ,str + '\0');
            }
            catch (Exception ex)
            {
                Console.WriteLine($"메시지 전송 오류: {ex.Message}");
                Disconnect();
            }
        }

        // 실제 데이터를 바이트로 변환하고 네트워크 스트림에 쓰는 내부 메서드
        private async Task SendDatasByteData(Sendtypes sendtypes,int roomnum ,string str)
        {
            byte[] strBytes = Encoding.UTF8.GetBytes(str);


            int totalLength = 2 + strBytes.Length;

            byte[] combinedData = new byte[totalLength];
                        
            combinedData[0] = (byte)((int)sendtypes);

            combinedData[1] = (byte)roomnum;

            Buffer.BlockCopy(strBytes, 0, combinedData, 2, strBytes.Length);

            await _stream.WriteAsync(combinedData, 0, combinedData.Length);


            //byte[] data = Encoding.UTF8.GetBytes(message);
            //await _stream.WriteAsync(data, 0, data.Length);
        }


        private async Task ReceiveMessagesAsync(CancellationToken cancellationToken)
        {
            byte[] buffer = new byte[1024]; // 서버 BUFFERSIZE와 동일
            StringBuilder receivedData = new StringBuilder();

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    int bytesRead = 0;
                    try
                    {
                        bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                    }
                    catch (OperationCanceledException)
                    {
                        break; // 취소 요청 시 루프 종료
                    }
                    catch (ObjectDisposedException)
                    {
                        break; // 스트림이 닫히면 루프 종료
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"데이터 수신 오류: {ex.Message}");
                        break;
                    }

                    if (bytesRead == 0)
                    {
                        Console.WriteLine("서버 연결이 종료되었습니다.");
                        break; // 서버가 연결을 종료함
                    }

                    string part = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    receivedData.Append(part);

                    // 서버가 totalMessage를 전송하고 messages.clear()를 하므로,
                    // 수신된 데이터를 하나의 큰 메시지로 처리하고 널 문자를 제거합니다.
                    string finalMessage = receivedData.ToString().TrimEnd('\0');
                    if (!string.IsNullOrEmpty(finalMessage))
                    {
                        MessageReceived?.Invoke(finalMessage); // 메시지 수신 이벤트 발생
                    }
                    receivedData.Clear(); // 다음 메시지를 위해 초기화
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"수신 루프 오류: {ex.Message}");
            }
            finally
            {
                Disconnect(); // 오류 발생 시 또는 루프 종료 시 연결 해제
            }
        }

        public void Disconnect()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }

            if (_stream != null)
            {
                _stream.Close();
                _stream.Dispose();
                _stream = null;
            }

            if (_client != null)
            {
                _client.Close();
                _client.Dispose();
                _client = null;
            }
            Console.WriteLine("클라이언트 연결이 종료되었습니다.");
            Disconnected?.Invoke(); // 연결 끊김 이벤트 발생
        }
    }
}
