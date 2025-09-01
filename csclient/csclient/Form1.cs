using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csclient
{
    public partial class Form1 : Form
    {
        //private Clients clients;
        private string username;
        private int roomnum = 0;
        //public Mainroom mainroom;
        public Form1()
        {
            InitializeComponent();
            username = ClientManager.username;
            //ClientManager.client = new Clients();

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            ClientManager.client.Connected += ChatClient_Connected;
            ClientManager.client.Disconnected += ChatClient_Disconnected;
            ClientManager.client.MessageReceived += ChatClient_MessageReceived;
            //string username = "WinFormUser_" + new Random().Next(100, 999);
            //AppendLogMessage($"채팅 서버에 연결 중... 사용자 이름: {username}", System.Drawing.Color.Gray);
            //await ClientManager.client.ConnectAsync();
            //AppendLogMessage($"채팅에 사용할 이름을 입력해주세요!", System.Drawing.Color.Gray);

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //ClientManager.client.Disconnect(); // 폼이 닫힐 때 클라이언트 연결 안전하게 해제
            //Form1 form = new Form1();
        }
        // 메시지 입력 TextBox에서 Enter 키 눌렀을 때
        private async void txtMessageInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Enter 키가 TextBox에 새 줄을 생성하는 것을 방지
                await SendChatMessage();
            }
        }
        private async Task SendChatMessage()
        {
            string message = textBox1.Text.Trim();
            if (!string.IsNullOrWhiteSpace(message))
            {
                await ClientManager.client.SendDataAsync(Clients.Sendtypes.sendmessage, roomnum,message);
                

                textBox1.Clear();

            }
        }

        private void ChatClient_Connected()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    //lblStatus.Text = "연결됨";
                    button1.Enabled = true;
                    textBox1.Enabled = true;
                    textBox1.Focus(); // 메시지 입력 필드에 포커스
                    AppendLogMessage("서버에 성공적으로 연결되었습니다.", System.Drawing.Color.Green);
                }));
            }
            else
            {
                //lblStatus.Text = "연결됨";
                button1.Enabled = true;
                textBox1.Enabled = true;
                textBox1.Focus();
                AppendLogMessage("서버에 성공적으로 연결되었습니다.", System.Drawing.Color.Green);
            }
        }

        private void ChatClient_MessageReceived(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    AppendLogMessage(message, System.Drawing.Color.Black); // 일반 메시지는 검은색
                }));
            }
            else
            {
                AppendLogMessage(message, System.Drawing.Color.Black);
            }
        }

        private void ChatClient_Disconnected()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    //lblStatus.Text = "연결 끊김";
                    button1.Enabled = false;
                    textBox1.Enabled = false;
                    AppendLogMessage("서버 연결이 끊어졌습니다.", System.Drawing.Color.Red);
                }));
            }
            else
            {
                //lblStatus.Text = "연결 끊김";
                button1.Enabled = false;
                textBox1.Enabled = false;
                AppendLogMessage("서버 연결이 끊어졌습니다.", System.Drawing.Color.Red);
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }



        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await SendChatMessage();
        }





        private void AppendLogMessage(string message, System.Drawing.Color color)
        {

            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.SelectionLength = 0;
            richTextBox1.SelectionColor = color;
            richTextBox1.AppendText($"{message}{Environment.NewLine}");
            richTextBox1.SelectionColor = richTextBox1.ForeColor; // 색상 초기화
            richTextBox1.ScrollToCaret(); // 스크롤을 최신 메시지로 이동
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }

        private void fontDialog1_Apply_1(object sender, EventArgs e)
        {

        }

        private void exitbutton_Click(object sender, EventArgs e)
        {
            this.Hide();
            ClientManager.mainroom.Show();
        }
    }
}
