namespace csclient
{
    internal static class Program
    {
        
        [STAThread]
        static void Main()
        {
            
            ApplicationConfiguration.Initialize();
            //ClientManager.Init();
            ClientManager.modalform = new Modalform();
            Application.Run(ClientManager.modalform);
            //Application.Run(new Form1());
        }
    }
}