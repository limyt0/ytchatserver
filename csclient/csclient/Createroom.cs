using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace csclient
{
    public partial class Createroom : Form
    {
        //public Form1 form1;
        //public Mainroom mainroom;
        public Createroom()
        {
            InitializeComponent();
        }

        private void createroom_Load(object sender, EventArgs e)
        {
            //form1.Show();
            //mainroom.Hide();
            //this.Hide();
        }

        private async void yesbutton_Click(object sender, EventArgs e)
        {

            ClientManager.form1.Show();
            await Createnewroom();
            this.Hide();
            ClientManager.mainroom.Hide();
        }

        private async Task Createnewroom()
        {
            string roomname = roomtitle.Text;
            if (!string.IsNullOrWhiteSpace(roomname))
            {                
                await ClientManager.client.SendDataAsync(Clients.Sendtypes.createroom, 0,roomname);
                await RoomIn();
                //roomtitle.Clear();
            }
        }


        private async Task RoomIn()
        {
            int roomnum = 0;
            await ClientManager.client.SendDataAsync(Clients.Sendtypes.roomin, roomnum,"roomin");
        }

        private void nobutton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void roomtitle_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
