using MahApps.Metro.Controls;
using System;
using System.Windows;

namespace Umbrella_Corps
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow() {
            InitializeComponent();
            var nbHearts = getHeartsProcessor();


            MessageBox.Show("Nombre de coeurs : "+ nbHearts + "");
        }

        // Récupère de nombre de coeurs sur le processeur
        public int getHeartsProcessor() {
            var nbHearts = Environment.ProcessorCount;
            return nbHearts;
        }
    }
}
