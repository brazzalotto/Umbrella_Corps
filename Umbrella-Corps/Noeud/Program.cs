using PartageTCP.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{

    static void Main(string[] args)
    {
        Noeud Noeud = new Noeud();
        Noeud.Connect("localhost", 8888);
        //System.Threading.Thread.Sleep(1000);

        AdnLinePackage lignepaquet = new AdnLinePackage();
        lignepaquet.code = 2;

        while (Console.ReadLine() != "a")
        {
            Noeud.SendMessage(lignepaquet);
        }
    }


}

