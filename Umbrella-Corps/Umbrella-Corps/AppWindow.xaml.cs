using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
namespace Umbrella_Corps
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        string filename;
        public MainWindow()
        {
            //InitializeComponent();
            var nbHearts = getHeartsProcessor();
            
            //MessageBox.Show("Nombre de coeurs : "+ nbHearts + "");
        }
        // Récupère de nombre de coeurs sur le processeur
        public int getHeartsProcessor()
        {
            var nbHearts = Environment.ProcessorCount;
            return nbHearts;
        }
        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";
            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();
            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                filename = dlg.FileName;
                parcourir.Text = filename;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String mode = modes.Text;
            //MessageBox.Show(filename);
            StreamReader sr = new StreamReader(filename);
            try
            {
                resultats.Text = sr.ReadToEnd();
                Console.WriteLine(sr);
            }
            catch (Exception)
            {
                MessageBox.Show("Le path ne pas pas être vide");
            }




            System.IO.StreamReader file = new System.IO.StreamReader(filename);
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

            String nombreDePaires = "il existe " + paires + " paires de base au total";

            int ratioBaseA = (baseA * 100) / lignes;
            int ratioBaseT = (baseT * 100) / lignes;
            int ratioBaseG = (baseG * 100) / lignes;
            int ratioBaseC = (baseC * 100) / lignes;

            String nombreDeBaseA = "il y à " + ratioBaseA + "% de base A.";
            String nombreDeBaseT = "il y à " + ratioBaseT + "% de base T.";
            String nombreDeBaseG = "il y à " + ratioBaseG + "% de base G.";
            String nombreDeBaseC = "il y à " + ratioBaseC + "% de base C.";
            String nombreDeBaseIconnues = "il y à " + baseInconnue + " base(s) inconue.";

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

            String combin = "";
            foreach (KeyValuePair<string, int> items in combinaisons)
            {
                String combi = items.Key;
                int valeur = items.Value;

                combin += items.Key + " = " + items.Value + "\n";                       
            }

            resultats.Text = nombreDeBaseA + "\n" + nombreDeBaseT + "\n" + nombreDeBaseG + "\n" + nombreDeBaseC + "\n" + nombreDeBaseIconnues + "\n" + combin;
        }
    }
}