using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csclient
{
    public sealed class ClientManager
    {
        private static readonly ClientManager instance = new ClientManager();
        internal static Form1 form1;
        internal static Clients client;
        internal static Createroom createroom;
        internal static Mainroom mainroom;
        internal static Modalform modalform;
        
        internal static string username;

        static ClientManager()
        {

        }

        private ClientManager()
        {

        }

        public static ClientManager Instance
        {
            get
            {
                return instance;
            }
        }

        public static void Init() {
            //modalform = new Modalform();
            form1 = new Form1();
            mainroom = new Mainroom();
            createroom = new Createroom();
            //client = new Clients();
        }
    }
}
