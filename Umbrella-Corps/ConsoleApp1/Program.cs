using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {


            System.IO.StreamReader file = new System.IO.StreamReader(@"c:\genome.txt");
            string line;
            int paires = 0;
            int baseA = 0;
            int baseT = 0;
            int baseG = 0;
            int baseC = 0;
            int lignes = 0;
            int baseInconnue = 0;
            string listeBase = "";
            while ((line = file.ReadLine()) != null)
            {
                // nombre de paires totales 
                if (line.Contains("AT") || line.Contains("TA") || line.Contains("CG") || line.Contains("GC"))
                {
                    paires++;

                    if (line.Contains("AT"))
                    {
                        listeBase += "AT";
                    }

                    if (line.Contains("TA"))
                    {
                        listeBase += "TA";
                    }
                    if (line.Contains("CG"))
                    {
                        listeBase += "CG";
                    }
                    if (line.Contains("GC"))
                    {
                        listeBase += "GC";
                    }

                }
                // occurance des bases
                if (line.Contains("A"))
                {
                    baseA++;
                }

                if (line.Contains("T"))
                {
                    baseT++;

                }

                if (line.Contains("G"))
                {
                    baseG++;

                }

                if (line.Contains("C"))
                {
                    baseC++;
                }
                // occurence des bases inconnues
                if (line.Contains("-"))
                {
                    baseInconnue++;
                }

                // recuperation des sequences de 4 bases

                lignes++;

            }

            Console.WriteLine("il existe " + paires + " paires de base au total");

            int ratioBaseA = (baseA * 100) / lignes;
            int ratioBaseT = (baseT * 100) / lignes;
            int ratioBaseG = (baseG * 100) / lignes;
            int ratioBaseC = (baseC * 100) / lignes;

            Console.WriteLine("il y à " + ratioBaseA + "% de base A.");
            Console.WriteLine("il y à " + ratioBaseT + "% de base T.");
            Console.WriteLine("il y à " + ratioBaseG + "% de base G.");
            Console.WriteLine("il y à " + ratioBaseC + "% de base C.");
            Console.WriteLine("il y à " + baseInconnue + " base(s) inconue.");

            //Console.WriteLine(listeBase);

            int i = 0;
            string combinaison = "";
            Dictionary<string, int> combinaisons = new Dictionary<string, int>();


            foreach (var lettre in listeBase)
            {
                
                

                if (i == 4)
                {
                    if (combinaisons.ContainsKey(combinaison))
                    {
                        combinaisons[combinaison]++;
                    }
                    else
                    {
                        combinaisons.Add(combinaison, 1);
                    }
                    i = 0;
                    combinaison = "";
                    
                }
                combinaison += lettre;
                i++;
            }

            foreach (KeyValuePair<string, int> items in combinaisons)
            {
                Console.WriteLine("Key = {0}, Value = {1}", items.Key, items.Value);
            }
        }
    }
}
