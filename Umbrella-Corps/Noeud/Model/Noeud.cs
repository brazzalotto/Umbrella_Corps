using PartageTCP.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Noeud.Model
{
    class Noeud
    {
        private Thread receivingThread;
        private Thread sendingThread;
        private List<ResponseCallbackObject> callBacks;


        public TcpClient TcpClient { get; set; }
        public String Address { get; private set; }
        public int Port { get; private set; }
        public StatusEnum Status { get; private set; }
        public List<MessageBase> MessageQueue { get; private set; }

        public event Action<Noeud, String> TextMessageReceived;
        public event Action<Noeud> ClientDisconnected;
        public event Action<Noeud, GenericRequest> GenericRequestReceived;


        public Noeud()
        {
            callBacks = new List<ResponseCallbackObject>();
            MessageQueue = new List<MessageBase>();
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
            MessageQueue.Clear();
            callBacks.Clear();
            try
            {
                //SendMessage(new DisconnectRequest());
            }
            catch { }
            Thread.Sleep(1000);
            Status = StatusEnum.Disconnected;
            TcpClient.Client.Disconnect(false);
            TcpClient.Close();
            ClientDisconnected(this);
        }

        private void SendingMethod(object obj)
        {
            while (Status != StatusEnum.Disconnected)
            {
                if (MessageQueue.Count > 0)
                {
                    MessageBase m = MessageQueue[0];

                    BinaryFormatter f = new BinaryFormatter();
                    try
                    {
                        f.Serialize(TcpClient.GetStream(), m);
                    }
                    catch
                    {
                        Disconnect();
                    }

                    MessageQueue.Remove(m);
                }

                Thread.Sleep(30);
            }
        }

        private void ReceivingMethod(object obj)
        {
            while (Status != StatusEnum.Disconnected)
            {
                if (TcpClient.Available > 0)
                {
                    //try
                    //{
                    BinaryFormatter f = new BinaryFormatter();
                    f.Binder = new AllowAllAssemblyVersionsDeserializationBinder();
                    MessageBase msg = f.Deserialize(TcpClient.GetStream()) as MessageBase;
                    OnMessageReceived(msg);
                    //}
                    //catch (Exception e)
                    //{
                    //    Exception ex = new Exception("Unknown message recieved. Could not deserialize the stream.", e);
                    //    OnClientError(this, ex);
                    //    Debug.WriteLine(ex.Message);
                    //}
                }

                Thread.Sleep(30);
            }
        }

        public void SendTextMessage(String message)
        {
            TextMessageRequest request = new TextMessageRequest();
            request.Message = message;
            SendMessage(request);
        }

        public void SendMessage(MessageBase message)
        {
            MessageQueue.Add(message);
        }

        protected virtual void OnMessageReceived(MessageBase msg)
        {
            Type type = msg.GetType();

            if (msg is ResponseMessageBase)
            {
                if (type == typeof(GenericResponse))
                {
                    msg = (msg as GenericResponse).ExtractInnerMessage();
                }
                InvokeMessageCallback(msg, (msg as ResponseMessageBase).DeleteCallbackAfterInvoke);
            }
            else
            {
                if (type == typeof(TextMessageRequest))
                {
                    TextMessageRequestHandler(msg as TextMessageRequest);
                }            
                else if (type == typeof(GenericRequest))
                {
                    GenericRequestReceived(this, (msg as GenericRequest).ExtractInnerMessage());
                }
            }
        }
        private void TextMessageRequestHandler(TextMessageRequest request)
        {
            TextMessageReceived(this, request.Message);
        }

        private void AddCallback(Delegate callBack, MessageBase msg)
        {
            if (callBack != null)
            {
                Guid callbackID = Guid.NewGuid();
                ResponseCallbackObject responseCallback = new ResponseCallbackObject()
                {
                    ID = callbackID,
                    CallBack = callBack
                };

                msg.CallbackID = callbackID;
                callBacks.Add(responseCallback);
            }
        }

        private void InvokeMessageCallback(MessageBase msg, bool deleteCallback)
        {
            var callBackObject = callBacks.SingleOrDefault(x => x.ID == msg.CallbackID);

            if (callBackObject != null)
            {
                if (deleteCallback)
                {
                    callBacks.Remove(callBackObject);
                }
                callBackObject.CallBack.DynamicInvoke(this, msg);
            }
        }
    }
}
