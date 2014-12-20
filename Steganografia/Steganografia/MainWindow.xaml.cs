using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
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
using System.Drawing;
using Microsoft.Win32;
using System.Drawing;
using Color = System.Drawing.Color;


namespace Steganografia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            
        }

        #region Kodowanie click
        private void Kodowanie_click(object sender, RoutedEventArgs e)
        {
            //encryption();
            Bitmap img = new Bitmap(Sciezka_obrazu.Text);

            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j);
                    if (i<1 && j<TextBoxmassage.Text.Length)
                    {
                        Console.WriteLine("R = [" + i + "][" + j + "] =" + pixel.R);
                        Console.WriteLine("G = [" + i + "][" + j + "] =" + pixel.G);
                        Console.WriteLine("B = [" + i + "][" + j + "] =" + pixel.B);
                        char letter = TextBoxmassage.Text[j];
                        //var letter = Convert.ToChar((TextBoxmassage.Text.Substring(j, i)));
                        int value = Convert.ToInt32((letter));
                        Console.WriteLine("letter :" + letter + " value: " + value);

                        img.SetPixel(i,j,Color.FromArgb(pixel.R,pixel.G, value));
                    }
                    if(i==img.Width-1&&j==img.Height-1)
                    {
                        img.SetPixel(i,j,Color.FromArgb(pixel.R,pixel.G,TextBoxmassage.Text.Length));
                       
                        Console.WriteLine(img.GetPixel(i, j));
                    }
                }
            }
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Image Files (*.bmp)|*.bmp";

            if (saveFile.ShowDialog() == true)
            {
                string selectedFileName = saveFile.FileName;
                Sciezka_obrazu.Text = selectedFileName;
                BitmapImage bitmap = new BitmapImage();

                img.Save(Sciezka_obrazu.Text);

                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                Obraz.Source = bitmap;
            }
        }

        #endregion


        #region Odkodowanie click
        private void Odkodowanie_click(object sender, RoutedEventArgs e)
        {
            //decryption();

            Bitmap img = new Bitmap(Sciezka_obrazu.Text);
            string massage = "";

            Color lastpixel = img.GetPixel(img.Width - 1, img.Height - 1);
            int msgLength = lastpixel.B;
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j);
                    if (i < 1 && j < msgLength)
                    {
                        Console.WriteLine("R = [" + i + "][" + j + "] =" + pixel.R);
                        Console.WriteLine("G = [" + i + "][" + j + "] =" + pixel.G);
                        Console.WriteLine("B = [" + i + "][" + j + "] =" + pixel.B);

                        int value = pixel.B;
                        char c = Convert.ToChar(value);
                        string letter = System.Text.Encoding.ASCII.GetString(new byte[] {Convert.ToByte(c)});

                        massage = massage + letter;
                        
                    }
                }
            }
            TextBoxmassage.Text = massage;
        }

        #endregion

        private void Wczytaj_obraz(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            //dlg.InitialDirectory = "c:\\Users\\"+userName+"\\Desktop";
            dlg.Filter = "Image files (*.bmp ) | *.bmp";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                string selectedFileName = dlg.FileName;
                Sciezka_obrazu.Text = selectedFileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                Obraz.Source = bitmap;
            }
            
        }
    }
}
