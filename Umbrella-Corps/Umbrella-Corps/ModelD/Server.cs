using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Umbrella_Corps.ModelD
{
    class Server
    {
        public TcpListener Listener { get; set; }
   
        public int Port { get; set; }

        public Server(int port, MainWindow fenetre)
        {
            Port = port;
            //string IpAddress = "127.0.0.1";
            //Socket ServerListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IpAddress),Port);
            //ServerListener.Bind(ep);
            //ServerListener.Listen(100);

            TcpListener myList = new TcpListener(IPAddress.Any, 8001);


            // var b=new MainWindow();

            fenetre.ZoneTexte.AppendText("Le serveur est lancé\n");


        }





    }
}
