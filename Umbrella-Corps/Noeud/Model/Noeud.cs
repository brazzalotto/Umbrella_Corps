using PartageTCP.Enum;
using PartageTCP.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;


class Noeud
{
    private Thread receivingThread;
    private Thread sendingThread;

    public TcpClient TcpClient { get; set; }
    public String Address { get; private set; }
    public int Port { get; private set; }
    public StatusEnum Status { get; private set; }

    public List<AdnLinePackage> AdnListMessage { get; private set; }

    public event Action<Noeud> ClientDisconnected;

    public Noeud()
    {
        //callBacks = new List<ResponseCallbackObject>();
        //MessageQueue = new List<MessageBase>();
        AdnListMessage = new List<AdnLinePackage>();
        Status = StatusEnum.Disconnected;
    }

    public void Connect(String address, int port)
    {
        Address = address;
        Port = port;
        TcpClient = new TcpClient();
        TcpClient.Connect(Address, Port);
        Status = StatusEnum.Connected;
        TcpClient.ReceiveBufferSize = 1024;
        TcpClient.SendBufferSize = 1024;

        receivingThread = new Thread(ReceivingMethod);
        receivingThread.IsBackground = true;
        receivingThread.Start();

        sendingThread = new Thread(SendingMethod);
        sendingThread.IsBackground = true;
        sendingThread.Start();
    }

    public void Disconnect()
    {
        AdnListMessage.Clear();
        //callBacks.Clear();
        try
        {
            //SendMessage(new DisconnectRequest());
        }
        catch { }
        Thread.Sleep(1000);
        Status = StatusEnum.Disconnected;
        TcpClient.Client.Disconnect(false);
        TcpClient.Close();
        //ClientDisconnected(this);
    }

    private void SendingMethod()
    {
        while (Status != StatusEnum.Disconnected)
        {
            if (AdnListMessage.Count > 0)
            {
                AdnLinePackage al = AdnListMessage[0];
                
                try
                {
                    BinaryFormatter f = new BinaryFormatter();
                    f.Binder = new AllowAllAssemblyVersionsDeserializationBinder();
                    f.Serialize(TcpClient.GetStream(), al);
                    Console.WriteLine("Message envoyé");
                }
                catch
                {
                    Disconnect();
                }

                AdnListMessage.Remove(al);
            }
            Thread.Sleep(30);
        }
    }

    public void SendMessage(AdnLinePackage message)
    {
        AdnListMessage.Add(message);
    }

    private void ReceivingMethod()
    {
        while (Status != StatusEnum.Disconnected)
        {
            if (TcpClient.Available > 0)
            {
                try
                {
                BinaryFormatter f = new BinaryFormatter();
                f.Binder = new AllowAllAssemblyVersionsDeserializationBinder();
                AdnLinePackage msg = f.Deserialize(TcpClient.GetStream()) as AdnLinePackage;
                OnMessageReceived(msg);
                }
               catch (Exception e)
                {
                //    Exception ex = new Exception("Unknown message recieved. Could not deserialize the stream.", e);
                //    OnClientError(this, ex);
                //    Debug.WriteLine(ex.Message);
                }
            }

            Thread.Sleep(30);
        }
    }

    protected virtual void OnMessageReceived(AdnLinePackage msg)
    {
        //Ici traitement du message selon le code
        Console.WriteLine("Code reçu {0}", msg.code);
    }

}
