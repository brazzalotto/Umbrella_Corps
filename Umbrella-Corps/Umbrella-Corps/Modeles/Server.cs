using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Umbrella_Corps.Modeles
{
    public class Server
    {
        public TcpListener Listener { get; set; }
        public int Port { get; set; }
        public bool IsStarted { get; private set; }
        public List<Receiver> Receivers { get; private set; }
        public MainWindow fenetre { get; private set; }


        public event Action<Receiver> ClientConnected;


        public Server(int port,MainWindow fen)
        {
            Receivers = new List<Receiver>();
            Port = port;
            fenetre = fen;
            fenetre.logs.Text += ("Server started : " + DateTime.Now.ToString("HH:mm:ss tt")+"\n");
        }

        public void Start()
        {
            if (!IsStarted)
            {
                Listener = new TcpListener(System.Net.IPAddress.Any, Port);
                Listener.Start();
                IsStarted = true;
                //Debug.WriteLine("Server Started!");

                //Start Async pattern for accepting new connections
                WaitForConnection();
            }
        }

        public void Stop()
        {
            if (IsStarted)
            {
                Listener.Stop();
                IsStarted = false;

                //Debug.WriteLine("Server Stoped!");
            }
        }

        private void WaitForConnection()
        {
            Listener.BeginAcceptTcpClient(new AsyncCallback(ConnectionHandler), null);
        }

        private void ConnectionHandler(IAsyncResult ar)
        {
            lock (Receivers)
            {
                Receiver newClient = new Receiver(Listener.EndAcceptTcpClient(ar), this);
                newClient.Start();
                Receivers.Add(newClient);
                OnClientConnected(newClient);
            }


            //this.fenetre.ZoneTexte.AppendText("client connecté   " + Receivers.Last().ID);
            //Console.WriteLine("New Client Connected");
            WaitForConnection();
        }


        //[SecurityPermissionAttribute(SecurityAction.Demand, ControlThread = true)]
        public virtual void OnClientConnected(Receiver receiver)
        {
            Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
                //fenetre.ZoneTexte.AppendText("\nClient connecté   " + Receivers.Last().ID);
            }), DispatcherPriority.Normal, null);


            //receiver.receivingThread.Resume();
            if (ClientConnected != null) ClientConnected(receiver);
        }

    }
}
