using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csclient
{
    public partial class Modalform : Form
    {
        //Form1 form;
        //Mainroom mainroom;
        //Createroom createroom;

        public Modalform()
        {
            InitializeComponent();
            ClientManager.Init();
            //Inits();
            
        }

        private void Inits() { 
            
            //form = new Form1();
            //mainroom = new Mainroom();
            //createroom = new Createroom();

            //mainroom.createroom = createroom;
            //mainroom.form1 = form;
            //createroom.mainroom = mainroom;
            //createroom.form1 = form;
            //form.mainroom = mainroom;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            
            ClientManager.form1.FormClosed += Formcloseds;
            ClientManager.mainroom.FormClosed += Formcloseds;

            ClientManager.mainroom.Show();

            await Login();
            this.Hide();
        }

        private async Task Login() {
            ClientManager.client = new Clients();
            ClientManager.username = textBox1.Text;
            await ClientManager.client.ConnectAsync();
            await ClientManager.client.SendDataAsync(Clients.Sendtypes.newclient, 0, ClientManager.username);
            //ClientManager.client.Connected += ChatClient_Connected;
            //ClientManager.client.Disconnected += ChatClient_Disconnected;
            //ClientManager.client.MessageReceived += ChatClient_MessageReceived;
        }

        private void Formcloseds(object? sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void modalform_Load(object sender, EventArgs e)
        {
            
        }

        private void modalform_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ClientManager.client != null) { 
                ClientManager.client.Disconnect();
            }
        }
    }
}
