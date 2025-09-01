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
    public partial class Mainroom : Form
    {
        //public Form1 form1;
        //public Createroom createroom;
        public Mainroom()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //await RoomIn();
        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            ClientManager.createroom.Show();

        }

        private async Task RoomIn(int getroomnum)
        {
            //int getroomnum = 0; //여기 room값필요
            await ClientManager.client.SendDataAsync(Clients.Sendtypes.roomin, getroomnum, "");
        }

        private void listView1_SelectedIndexChanged_2(object sender, EventArgs e)
        {

        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            //
            try
            {
                int num = Int32.Parse(roomnumtext.Text);
                await RoomIn(num);
                ClientManager.form1.Show();
                this.Hide();
            }
            catch (FormatException ee)
            {
                Console.WriteLine(ee.Message);
            }
            
        }


    }
}
