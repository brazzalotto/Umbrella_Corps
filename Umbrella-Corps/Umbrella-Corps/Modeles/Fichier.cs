using PartageTCP.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Umbrella_Corps.Modeles
{
    class Fichier
    {
        public string filePath { get; set; }
        public int lineCount { get; set; }
        public List<Paquet> listePaquets { get; set; }

        public List<AdnLinePackage> ListPackageToSend = new List<AdnLinePackage>();

        public Fichier() {
            getFilePath();
            getTotalLine();
            listePaquets = new List<Paquet>();
            ListPackageToSend = new List<AdnLinePackage>();
        }

        // Choix du fichier, retourne le chemin || message d'erreurz
        public void getFilePath() {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                filePath = dlg.FileName;
            }
        }

        // Retourne le nombre de lignes du fichier
        public void getTotalLine() {
            string line = string.Empty;
            string[] ligne = new string[4];

            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            // Read first line
            line = file.ReadLine();

            // Second Line
            line = file.ReadLine();
            lineCount = 1;

            // Count
            while ((line = file.ReadLine()) != null && line.Split('\t')[1] != "MT")
            {
                lineCount++;
            }
        }

        // Création de la liste des paquets pour les noeuds connectés 

        public void setListePaquets(int nbNoeuds)
        {
            int ligneEnCours=0;
            int indice = 1;

            List<int> LignesPaquets = DistributeInteger(lineCount, nbNoeuds).ToList();

            foreach (int nbLigne in LignesPaquets)
            {
                listePaquets.Add(new Paquet(indice, ligneEnCours+1, ligneEnCours + nbLigne));
                ligneEnCours+= nbLigne;
                indice++;
            }
        }

        public static IEnumerable<int> DistributeInteger(int total, int divider)
        {
            if (divider == 0)
            {
                yield return 0;
            }
            else
            {
                int rest = total % divider;
                double result = total / (double)divider;

                for (int i = 0; i < divider; i++)
                {
                    if (rest-- > 0)
                        yield return (int)Math.Ceiling(result);
                    else
                        yield return (int)Math.Floor(result);
                }
            }
        }

        // Découpe le fichier
        public void cuttingFile(Fichier f) {

            System.IO.StreamReader file = new System.IO.StreamReader(filePath);

            //1ere ligne
            file.ReadLine();

            //for (int i = 0; i < p.ligneDebut; i++)
            //{
            //    if (true)
            //    {

            //    }
            //    file.ReadLine();
            //}

            foreach (Paquet item in f.listePaquets)
            {
                AdnLinePackage dna = new AdnLinePackage();
                GenericAdnList gen = new GenericAdnList();

                for (int i = item.ligneDebut; i < item.ligneFin; i++)
                {
                    AdnLine aze = new AdnLine();
                    var line = file.ReadLine();
                    aze.rsId = line.Split('\t')[0];
                    aze.chromosome = line.Split('\t')[1];
                    aze.position = line.Split('\t')[2];
                    aze.genotype = line.Split('\t')[3];

                    gen.Add(aze);
                }
                dna.adnList = gen;
                dna.code = 1;

                ListPackageToSend.Add(dna);
            }
        }


        
    }
}
