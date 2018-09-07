using PartageTCP.Enum;
using PartageTCP.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;


class Noeuds
{
    private Thread receivingThread;
    private Thread sendingThread;

    List<Result> res = new List<Result>();

    public TcpClient TcpClient { get; set; }
    public String Address { get; private set; }
    public int Port { get; private set; }
    public StatusEnum Status { get; private set; }
    public List<Thread> lstThreads { get;  set; }
    public List<AdnLinePackage> AdnListMessage { get; private set; }

    public Result Result { get;  set; }

    public Noeuds()
    {
        AdnListMessage = new List<AdnLinePackage>();
        Result = null;
        Status = StatusEnum.Disconnected;
        lstThreads = new List<Thread>();
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
            if (Result != null)
            {
                try
                {
                    BinaryFormatter f = new BinaryFormatter();
                    f.Binder = new AllowAllAssemblyVersionsDeserializationBinder();
                    f.Serialize(TcpClient.GetStream(), Result);
                    Result = null;
                    Console.WriteLine("Message envoyé");
                }
                catch
                {
                    Disconnect();
                }
            }
            Thread.Sleep(30);
        }
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
                   Exception ex = new Exception("Unknown message recieved. Could not deserialize the stream.", e);
                }
            }

            Thread.Sleep(30);
        }
    }

    protected virtual void OnMessageReceived(AdnLinePackage msg)
    {
        //Ici traitement du message selon le code
        Console.WriteLine("Code reçu {0}", msg.code);
        AdnListMessage.Add(msg);
        int nbCoeur = getHeartsProcessor();
        
        switch (msg.code)
        {
            case 1:

                int nbLig = AdnListMessage[0].adnList.Count;
                int nombre = nbLig / nbCoeur;


                for (int i = 0; i < nbCoeur - 1; i++)
                {
                    List<AdnLine> petiteListeAdn = msg.adnList.GetRange(i * nombre, nombre);
                    CreateThreads(petiteListeAdn);

                    //Result r = new Result();
                    //res.Add(r);

                    lstThreads[i].Start();
                }

                //lstThreads
                foreach (Thread th in lstThreads)
                {
                    if (th.IsAlive) // Si le thread n'est pas déjà fini
                    {
                        th.Join(); // On attend que le thread soit terminé
                    }
                }

                Reduce();

                break;
            case 2:
                break;
            default:
                break;
        }
     }

    // Récupère de nombre de coeurs sur le processeur
    public int getHeartsProcessor()
    {
        var nbHearts = Environment.ProcessorCount;
        return nbHearts;
    }
    public void CreateThreads(List<AdnLine> petiteListeAdn)
    {
        Thread th = new Thread(() => { Map(petiteListeAdn); });
        lstThreads.Add(th);
    }

    private void Map(List<AdnLine> petiteListeAdn )
    {
        int paires = 0;
        int baseA = 0;
        int baseT = 0;
        int baseG = 0;
        int baseC = 0;
        int baseInconnue = 0;

        foreach (var item in petiteListeAdn)
        {
            paires++;

            // occurance des bases
            if (item.genotype.Contains("A"))
            {
                baseA++;
            }

            if (item.genotype.Contains("T"))
            {
                baseT++;
            }

            if (item.genotype.Contains("G"))
            {
                baseG++;
            }

            if (item.genotype.Contains("C"))
            {
                baseC++;
            }
            // occurence des bases inconnues
            if (item.genotype.Contains("-"))
            {
                baseInconnue++;
            }
        }

        Result r = new Result();
        r.aNumber = baseA;
        r.cNumber = baseC;
        r.tNumber = baseT;
        r.gNumber = baseG;
        r.unknownNumber = baseInconnue;
        res.Add(r);

        
    }

    private void Reduce()
    {
        Result r = new Result();
        foreach (var item in res)
        {
            r.aNumber += item.aNumber;
            r.cNumber += item.cNumber;
            r.tNumber += item.tNumber;
            r.gNumber += item.gNumber;
            r.unknownNumber += item.unknownNumber;
        }
        Result = r;
    }
  
    public static List<List<T>> splitList<T>(List<T> locations, int nSize = 30)
    {
        var list = new List<List<T>>();

        for (int i = 0; i < locations.Count; i += nSize)
        {
            list.Add(locations.GetRange(i, Math.Min(nSize, locations.Count - i)));
        }

        return list;
    }
}
