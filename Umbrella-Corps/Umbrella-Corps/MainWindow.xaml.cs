using MahApps.Metro.Controls;
using PartageTCP.Messages;
using System;
using System.Windows;
using System.Windows.Threading;
using Umbrella_Corps.ModelD;

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
            ZoneTexte.Clear();
            var nbHearts = getHeartsProcessor();

            Server = new Server(8888,this);
            Server.Start();
            
            //MessageBox.Show("Nombre de coeurs : "+ nbHearts + "");
        }

        // Récupère de nombre de coeurs sur le processeur
        public int getHeartsProcessor() {
            var nbHearts = Environment.ProcessorCount;
            return nbHearts;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
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

            Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
                Server.Receivers[0].SendMessage(lignepaquet);
            }), DispatcherPriority.Normal, null);
           
        }
    }
}
