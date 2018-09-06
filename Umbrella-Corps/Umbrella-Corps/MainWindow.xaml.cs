using MahApps.Metro.Controls;
using System;
using System.Windows;
using Umbrella_Corps.Modeles;

namespace Umbrella_Corps
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow() {
            InitializeComponent();
            show_activity.IsChecked = true;
        }

        private async void btn_load_file(object sender, RoutedEventArgs e) {
            int noeuds = getNbNoeuds();
            var File = new Fichier();
            resultat.Text = File.lineCount.ToString();
            File.setListePaquets(noeuds);
            var paquets = File.listePaquets;

            pourcentage_traitement.IsIndeterminate = true;
            var max_pourcent = File.lineCount;
            pourcentage_traitement.Maximum = max_pourcent;
            pourcentage_traitement.Value = max_pourcent;
            nb_pourcent_traitement.Content = "100%";

            if(File.listePaquets.Count > 0) {
                pourcentage_traitement.IsIndeterminate = false;
            }

            // Affiche le chemin du fichier
            file_path.Content = File.filePath;
        }

        // Nombre de noeuds
        private int getNbNoeuds()
        {
            int neuds = new Random().Next(1,5);
            return neuds;
        }

        private void show_activity_Checked(object sender, RoutedEventArgs e)
        {
            if (show_activity.IsChecked == true) {
                log_box.Height = 180;
                logs.Height = 150;
                log_hide.Content = "";
            }
            else {
                log_box.Height = 40;
                logs.Height = 0;
                log_hide.Content = "La liste des activitées n'est pas afficher !";
            }
        }
    }
}
