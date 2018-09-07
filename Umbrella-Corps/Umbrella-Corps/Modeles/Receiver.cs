using PartageTCP.Enum;
using PartageTCP.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Umbrella_Corps;
using Umbrella_Corps.Modeles;

public class Receiver
{
    public Thread receivingThread;
    public Thread sendingThread;

    public Guid ID { get; set; }
    public Server Server { get; set; }
    public TcpClient Client { get; set; }
    public StatusEnum Status { get; set; }
    
    public List<AdnLinePackage> AdnListMessage { get; private set; }
    public List<Result> ResultList { get;  set; }
    
    public long TotalBytesUsage { get; set; }

    public Receiver()
    {
        ID = Guid.NewGuid();
        AdnListMessage = new List<AdnLinePackage>();
        ResultList = new List<Result>();
        Status = StatusEnum.Connected;
    }

    public Receiver(TcpClient client, Server server) : this()
    {
        Server = server;
        Client = client;
        Client.ReceiveBufferSize = 1024;
        Client.SendBufferSize = 1024;
    }
        
    public void Start()
    {
        receivingThread = new Thread(ReceivingMethod);
        receivingThread.IsBackground = true;
        receivingThread.Start();

        sendingThread = new Thread(SendingMethod);
        sendingThread.IsBackground = true;
        sendingThread.Start();
    }

    private void Disconnect()
    {
        if (Status == StatusEnum.Disconnected) return;

        Status = StatusEnum.Disconnected;
        Client.Client.Disconnect(false);
        Client.Close();
    }

    public void SendMessage(AdnLinePackage message)
    {
        AdnListMessage.Add(message);
    }
    
    public void SendingMethod()
    {
        while (Status != StatusEnum.Disconnected)
        {
            if (AdnListMessage.Count > 0)
            {
                var message = AdnListMessage[0];

                try
                {
                    BinaryFormatter f = new BinaryFormatter();
                    f.Binder = new AllowAllAssemblyVersionsDeserializationBinder();
                    f.Serialize(Client.GetStream(), message);
                    Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        Server.fenetre.logs.Text += ("Data send to node : " + this.ID + "  " + DateTime.Now.ToString("HH:mm:ss tt") + "\n");
                    }), DispatcherPriority.Normal, null);
                }
                catch
                {
                    Disconnect();
                }
                finally
                {
                    AdnListMessage.Remove(message);
                }
            }
            Thread.Sleep(30);
        }
    }

    public void ReceivingMethod()
    {
        while (Status != StatusEnum.Disconnected)
        {
            if (Client.Available > 0)
            {
                TotalBytesUsage += Client.Available;

                try
                {
                    BinaryFormatter f = new BinaryFormatter();
                    f.Binder = new AllowAllAssemblyVersionsDeserializationBinder();
                    var b = f.Binder;
                    Result msg = f.Deserialize(Client.GetStream()) as Result;
                    OnMessageReceived(msg);
                }
                catch (Exception e)
                {
                    Exception ex = new Exception("Unknown message received. Could not deserialize the stream.", e);
                    Debug.WriteLine(ex.Message);
                }
            }
            Thread.Sleep(30);
        }
    }
        
    private void OnMessageReceived(Result msg)
    {
        
        Application.Current.Dispatcher.BeginInvoke((Action)(() =>
        {
            Server.fenetre.logs.Text+=("\n code  " + msg.cNumber);
        }), DispatcherPriority.Normal, null);
        if (msg !=null)
        {
            this.ResultList.Add((Result)msg);
        }
      
    }
}

