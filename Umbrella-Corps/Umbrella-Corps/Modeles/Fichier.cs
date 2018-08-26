using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbrella_Corps.Modeles
{
    class Fichier
    {
        // Choix du fichier, retourne le chemin || message d'erreur
        public string loadFileTxt() {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true) {
                return dlg.FileName;
            }
            return "Erreur de chargement du fichier.";
        }

        // Récupère de nombre de coeurs sur le processeur
        public int getHeartsProcessor() {
            var nbHearts = Environment.ProcessorCount;
            return nbHearts;
        }

        // Retourne le nombre de lignes du fichier
        public int getTotalLine(string filepath) {
            var nb_lines = File.ReadAllText(filepath).Length;
            return Convert.ToInt32(nb_lines);
        }

        // Découpe le fichier
        public string cuttingFile(string filepath, int neuds) {

            // Contenu du fichier
            var file_content = File.ReadAllText(filepath);
            int nb_total_lines = getTotalLine(filepath);
            var nb_cut = nb_total_lines / neuds;


            var list = new List<string>();
            var position = 0;
            for (int i=0; i< nb_cut; i++) {
                for (int j = 0; j< nb_total_lines; j++) {
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

    }
}
