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
            //AppendLogMessage($"ä�� ������ ���� ��... ����� �̸�: {username}", System.Drawing.Color.Gray);
            //await ClientManager.client.ConnectAsync();
            //AppendLogMessage($"ä�ÿ� ����� �̸��� �Է����ּ���!", System.Drawing.Color.Gray);

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //ClientManager.client.Disconnect(); // ���� ���� �� Ŭ���̾�Ʈ ���� �����ϰ� ����
            //Form1 form = new Form1();
        }
        // �޽��� �Է� TextBox���� Enter Ű ������ ��
        private async void txtMessageInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Enter Ű�� TextBox�� �� ���� �����ϴ� ���� ����
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
                    //lblStatus.Text = "�����";
                    button1.Enabled = true;
                    textBox1.Enabled = true;
                    textBox1.Focus(); // �޽��� �Է� �ʵ忡 ��Ŀ��
                    AppendLogMessage("������ ���������� ����Ǿ����ϴ�.", System.Drawing.Color.Green);
                }));
            }
            else
            {
                //lblStatus.Text = "�����";
                button1.Enabled = true;
                textBox1.Enabled = true;
                textBox1.Focus();
                AppendLogMessage("������ ���������� ����Ǿ����ϴ�.", System.Drawing.Color.Green);
            }
        }

        private void ChatClient_MessageReceived(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    AppendLogMessage(message, System.Drawing.Color.Black); // �Ϲ� �޽����� ������
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
                    //lblStatus.Text = "���� ����";
                    button1.Enabled = false;
                    textBox1.Enabled = false;
                    AppendLogMessage("���� ������ ���������ϴ�.", System.Drawing.Color.Red);
                }));
            }
            else
            {
                //lblStatus.Text = "���� ����";
                button1.Enabled = false;
                textBox1.Enabled = false;
                AppendLogMessage("���� ������ ���������ϴ�.", System.Drawing.Color.Red);
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
            richTextBox1.SelectionColor = richTextBox1.ForeColor; // ���� �ʱ�ȭ
            richTextBox1.ScrollToCaret(); // ��ũ���� �ֽ� �޽����� �̵�
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
