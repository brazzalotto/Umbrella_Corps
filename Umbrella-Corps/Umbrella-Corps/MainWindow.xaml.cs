using MahApps.Metro.Controls;
using PartageTCP.Messages;
using System;
using System.Windows;
using System.Windows.Threading;
using Umbrella_Corps.Modeles;

namespace Umbrella_Corps
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public Server Server;
        public MainWindow() {
            InitializeComponent();
            show_activity.IsChecked = true;
            Server = new Server(8888, this);
            Server.Start();
        }

        private async void btn_load_file(object sender, RoutedEventArgs e) {
            //int noeuds = getNbNoeuds();
            //var File = new Fichier();
            //resultat.Text = File.lineCount.ToString();
            //File.setListePaquets(noeuds);
            //var paquets = File.listePaquets;

            //pourcentage_traitement.IsIndeterminate = true;
            //var max_pourcent = File.lineCount;
            //pourcentage_traitement.Maximum = max_pourcent;
            //pourcentage_traitement.Value = max_pourcent;
            //nb_pourcent_traitement.Content = "100%";

            //if(File.listePaquets.Count > 0) {
            //    pourcentage_traitement.IsIndeterminate = false;
            //}

            //// Affiche le chemin du fichier
            //file_path.Content = File.filePath;

            AdnLine adn = new AdnLine();
            adn.chromosome = "jekjhek";
            adn.genotype = "lkjhjh";
            adn.position = "mkkjlkjk";
            adn.rsId = "mkokjklj";
            AdnLine adn2 = new AdnLine();
            adn2.chromosome = "jekjhek";
            adn2.genotype = "lkjhjh";
            adn2.position = "mkkjlkjk";
            adn2.rsId = "mkokjklj";
            AdnLinePackage lignepaquet = new AdnLinePackage();
            GenericAdnList tamere = new GenericAdnList();
            tamere.Add(adn);
            tamere.Add(adn2);
            lignepaquet.adnList = tamere;
            lignepaquet.code = 2;

            await Application.Current.Dispatcher.BeginInvoke((Action)(() =>
             {
                 Server.Receivers[0].SendMessage(lignepaquet);
             }), DispatcherPriority.Normal, null);

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
                log_hide.Content = "La liste des activités n'est pas afficher !";
            }
        }

    }
}
