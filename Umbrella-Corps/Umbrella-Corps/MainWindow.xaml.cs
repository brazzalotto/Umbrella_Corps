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
        }

        private async void btn_load_file(object sender, RoutedEventArgs e) {
            int neuds = getNbNoeuds();

            var File = new Fichier();
            var filepath = File.loadFileTxt();

            string results = File.cuttingFile(filepath, neuds);

            text.Text = results;
        }

        // Nombre de noeuds
        private int getNbNoeuds()
        {
            int neuds = 10;
            return neuds;
        }
    }
}
