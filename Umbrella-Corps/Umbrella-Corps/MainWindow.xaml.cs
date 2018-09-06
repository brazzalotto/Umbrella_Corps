using MahApps.Metro.Controls;
using System;
using System.Windows;
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

            var k=new Server(8888,this);
            k.Start();
            
            //MessageBox.Show("Nombre de coeurs : "+ nbHearts + "");
        }

        // Récupère de nombre de coeurs sur le processeur
        public int getHeartsProcessor() {
            var nbHearts = Environment.ProcessorCount;
            return nbHearts;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in Server.Receivers)
            {
                foreach (var item1 in item.AdnListMessage)
                {
                    ZoneTexte.AppendText(""+item1.code);
                }
            }
        }
    }
}
