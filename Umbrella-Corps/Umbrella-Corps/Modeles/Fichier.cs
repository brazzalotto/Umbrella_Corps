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
        private  string filePath { get; set; }
        public int lineCount { get; set; }
        private List<Paquet> listePaquets { get; set; }

    public Fichier()
        {
            getFilePath();
            getTotalLine();
            listePaquets = new List<Paquet>();
        }

        // Choix du fichier, retourne le chemin || message d'erreurz
        public void getFilePath() {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                filePath=dlg.FileName;
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
            var b = 0;
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
        public string cuttingFile(int neuds) {

            // Contenu du fichier
            var file_content = File.ReadAllText(filePath);
            var nb_cut = lineCount / neuds;


            var list = new List<string>();
            var position = 0;
            for (int i=0; i< nb_cut; i++) {
                for (int j = 0; j< lineCount; j++) {
                    if (nb_cut < j)
                    {
                        //list.Add = file_content.Substring() file_content.ReadLine();
                    }
                    position++;
                }
            }

            //file_Cuts.Add(file);
            return file_content;
        }




        // Récupère de nombre de coeurs sur le processeur
        public int getHeartsProcessor()
        {
            var nbHearts = Environment.ProcessorCount;
            return nbHearts;
        }
    }
}
