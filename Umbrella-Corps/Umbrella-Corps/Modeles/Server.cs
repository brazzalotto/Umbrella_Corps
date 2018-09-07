using PartageTCP.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Permissions;
using System.Text;
using System.Threading;
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
        public Thread reduceModule1 { get; private set; }

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
                reduceModule1 = new Thread(Reduce);
                reduceModule1.IsBackground = true;
                reduceModule1.Start();
                Listener = new TcpListener(System.Net.IPAddress.Any, Port);
                Listener.Start();
                IsStarted = true;
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
            WaitForConnection();
        }
        
        public virtual void OnClientConnected(Receiver receiver)
        {
            Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
                fenetre.clients_liste.ItemsSource = Receivers.ToList();

            }), DispatcherPriority.Normal, null);

            if (ClientConnected != null) ClientConnected(receiver);
        }

        private void Reduce()
        {

            var allResultsReceived = false;
            while (allResultsReceived !=true)
            {
               // Application.Current.Dispatcher.BeginInvoke((Action)(() =>
               // {
                    //var test = true;
                foreach (var item in Receivers)
                {
                    if (item.ResultList==null)
                    {
                            allResultsReceived = false;
                    }
                }
                //allResultsReceived = test;
               // }), DispatcherPriority.Normal, null);
            }

            if (allResultsReceived)
            {
                Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    Result r = new Result();
                    foreach (var item in Receivers)
                    {
                        r.aNumber += item.ResultList[0].aNumber;
                        r.cNumber += item.ResultList[0].cNumber;
                        r.tNumber += item.ResultList[0].tNumber;
                        r.gNumber += item.ResultList[0].gNumber;
                        r.unknownNumber += item.ResultList[0].unknownNumber;
                    }
                    fenetre.logs.Text += ("Result Module 1 :  " + DateTime.Now.ToString("HH:mm:ss tt")
                        + "  A count" + r.aNumber
                        + "  C count" + r.cNumber
                        + "  T count" + r.tNumber
                        + "  G count" + r.gNumber
                        + "  Unknown count" + r.unknownNumber
                        + "\n");
                    }), DispatcherPriority.Normal, null);
            }
        }
    }
}
