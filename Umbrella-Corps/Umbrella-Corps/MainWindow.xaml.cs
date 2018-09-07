using MahApps.Metro.Controls;
using PartageTCP.Messages;
using System;
using System.Collections.Generic;
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

            if (File.listePaquets.Count > 0)
            {
                pourcentage_traitement.IsIndeterminate = false;
            }

            File.cuttingFile(File);

            // Affiche le chemin du fichier
            file_path.Content = File.filePath;

            await Application.Current.Dispatcher.BeginInvoke((Action)(() =>
              {
                  for (int i = 0; i < Server.Receivers.Count; i++)
                  {
                      Server.Receivers[i].SendMessage(File.ListPackageToSend[i]);
                      Server.fenetre.logs.Text += ("Data send to node :  " + DateTime.Now.ToString("HH:mm:ss tt") + "\n");
                  }
              }), DispatcherPriority.Normal, null);

        }

        // Nombre de noeuds
        private int getNbNoeuds()
        {
            int neuds = Server.Receivers.Count;
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
