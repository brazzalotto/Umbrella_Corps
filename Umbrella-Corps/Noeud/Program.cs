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

        //AdnLine adn = new AdnLine();
        //adn.chromosome = "jekjhek";
        //adn.genotype = "lkjhjh";
        //adn.position = "mkkjlkjk";
        //adn.rsId = "mkokjklj";

        //AdnLine adn2 = new AdnLine();
        //adn2.chromosome = "jekjhek";
        //adn2.genotype = "lkjhjh";
        //adn2.position = "mkkjlkjk";
        //adn2.rsId = "mkokjklj";

        //AdnLinePackage lignepaquet = new AdnLinePackage();
        //GenericAdnList tamere = new GenericAdnList();
        //tamere.Add(adn);
        //tamere.Add(adn2);
        //lignepaquet.adnList = tamere;
        //lignepaquet.code = 2;

        Result b = new Result();
        b.cNumber = 1455;

        while (Console.ReadLine() != "a")
        {
            Noeud.Result=b;
        }
    }


}

