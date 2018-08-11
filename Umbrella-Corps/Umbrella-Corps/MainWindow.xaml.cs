using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
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
            InitializeComponent();
            var nbHearts = getHeartsProcessor();
            MessageBox.Show("Nombre de coeurs : " + nbHearts + "");
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
            MessageBox.Show(filename);
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
            resultats.Text = File.ReadAllText(filename);

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "/ConsoleApp1/yourprogram.exe");
            Process.Start(new ProcessStartInfo(path));
            Process.Start(ConsoleApp1);

        }
    }
}