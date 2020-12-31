using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;


namespace pc_cleaner
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        DirectoryInfo winTemp = new DirectoryInfo(System.IO.Path.GetTempPath());
        public long Sizef()
        {            
            long size = 0;
            

            foreach (FileInfo fi in winTemp.EnumerateFiles("*", SearchOption.AllDirectories))
            {
                size += fi.Length;
            }
            
            return (size / 1024) / 1024;
        }

        public void Delete()
        {
            using (StreamWriter sw = File.AppendText("file.txt"))
            {
                sw.WriteLine($"taille analyse : {Sizef()} Mo");
                sw.WriteLine("Date de l'analyse: " + DateTime.Now.ToString());
                sw.WriteLine(" ");

            }
            try
            {
                foreach (FileInfo file in winTemp.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in winTemp.EnumerateDirectories())
                {
                    dir.Delete(true);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"message erreur : \n {ex}");
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("premiers analyse");
            lbl_net.Visibility = Visibility.Hidden;
            lbl_net_compt.Visibility = Visibility.Hidden;
            prog.Visibility = Visibility.Visible;

            lbl_analyse.Content = "Annulé";
            text_affiche.Text = "Analyse en cours";
            lbl_time_anal.Content = DateTime.Now.ToString();
            
            Task.Run(() =>
            {
                for (int i = 0; i <= 100; i++)
                {

                    Thread.Sleep(50);
                    this.Dispatcher.Invoke(() =>
                    {
                        prog.Value = i;
                        lbl_prog.Content = i.ToString() + "%";

                        if (prog.Value == 100)
                        {
                            System.Windows.Forms.MessageBox.Show("Analyse complete");
                            lbl_net.Visibility = Visibility.Visible;
                            lbl_net_compt.Visibility = Visibility.Visible;
                            prog.Visibility = Visibility.Visible;
                            prog.Visibility = Visibility.Hidden;
                            lbl_prog.Visibility = Visibility.Hidden;
                            lbl_analyse.Content = "ANALYSER";
                            text_affiche.Text = "Analyse du pc nécessaire";

                            lbl_net_compt.Content = $"{Sizef()} Mo";

                        }

                    });

                }
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            Delete();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Process.Start("file.txt");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Process.Start("file.txt");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();

            try
            {
                if (!webClient.DownloadString("https://pastebin.com/raw/C8AENLFK").Contains("1.0.2"))
                {
                    if (System.Windows.Forms.MessageBox.Show("il y a une mise à jour! Voulez-vous le télécharger?", "Demo", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) using (var client = new WebClient())

                        {
                            Process.Start(@"C:\Users\Administrateur\OneDrive\Bureau\MAJ\bin\Release\MAJ.exe");
                            this.Close();
                        }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Vous êtes déjà à jour !");
                }
            }
            catch
            {

            }
        }
    }
}
