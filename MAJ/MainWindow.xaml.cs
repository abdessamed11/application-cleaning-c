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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Compression;

namespace MAJ
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            progg.Visibility = Visibility.Visible;
            lbl_progg.Visibility = Visibility.Visible;
            lbl_maj.Content = "En cours";

            Task.Run(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    
                    Thread.Sleep(50);

                    this.Dispatcher.Invoke(() => //Use Dispather to Update UI Immediately  
                    {
                        progg.Value = i;
                        lbl_progg.Content = i + "%";
                        if (i == 100)
                        {

                            WebClient webClient = new WebClient();
                            var client = new WebClient();

                            string fileName = "test.txt";
                            string sourcePath = @"C:\Users\Administrateur\OneDrive\Bureau\pc cleaner\bin\Release";
                            string targetPath = @"C:\Users\Administrateur\OneDrive\Bureau\pc cleaner\bin\Release1";
                            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                            string destFile = System.IO.Path.Combine(targetPath, fileName);

                            if (System.IO.Directory.Exists(sourcePath))
                            {
                                string[] filles = System.IO.Directory.GetFiles(sourcePath);

                                // Copy the files and overwrite destination files if they already exist.
                                foreach (string s in filles)
                                {
                                    // Use static Path methods to extract only the file name from the path.
                                    fileName = System.IO.Path.GetFileName(s);
                                    destFile = System.IO.Path.Combine(targetPath, fileName);
                                    System.IO.File.Copy(s, destFile, true);
                                }
                            }

                            Thread.Sleep(50);
                            string[] files = Directory.GetFiles(@"C:\Users\Administrateur\OneDrive\Bureau\pc cleaner\bin\Release");

                            foreach (string file in files)
                            {
                                File.Delete(file);
                            }



                            //pour telecharger le dossier de mise à jour
                            client.DownloadFile("http://download854.mediafire.com/lze8m7c1ucog/h986rh5nemv5rs3/Release.zip", @"C:\Users\Administrateur\OneDrive\Bureau\pc cleaner\bin\Release\Release.zip");
                            string zipPath = @"C:\Users\Administrateur\OneDrive\Bureau\pc cleaner\bin\Release\Release.zip";
                            //. pour extraire le dossier télécharger 
                            string extractPath = @"C:\Users\Administrateur\OneDrive\Bureau\pc cleaner\bin\Release";
                            ZipFile.ExtractToDirectory(zipPath, extractPath);

                            // not delate the zip file and leave it as backup by rename
                            File.Delete(@"C:\Users\Administrateur\OneDrive\Bureau\pc cleaner\bin\Release\Release.zip");
                            Process.Start(@"C:\Users\Administrateur\OneDrive\Bureau\pc cleaner\bin\Release\pc cleaner.exe");
                            this.Close();

                            //btn_quiter.Visibility = Visibility.Visible;
                            lbl_etat.Content = "La mise à jour à été effectuée avec succès !";
                        }
                        

                    });
                }
            });
        }
    }
}
