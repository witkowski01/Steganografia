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

        private int zamiana_na_pseudopolskie(int wejsciowy)
        {
            if (wejsciowy > 255)
            {
                int wyjscie=0;
                if (wejsciowy == 260)  //Ą
                {
                    wyjscie = 0;
                }
                else if (wejsciowy == 261)  //ą
                {
                    wyjscie = 2;
                }
                else if (wejsciowy == 262)  //ć duże
                {
                    wyjscie = 3;
                }
                else if (wejsciowy == 263)  //ć
                {
                    wyjscie = 4;
                }
                else if (wejsciowy == 280)  //Ę
                {
                    wyjscie = 5;
                }
                else if (wejsciowy == 281)  //ę
                {
                    wyjscie = 6;
                }
                else if (wejsciowy == 321)  //Ł
                {
                    wyjscie = 7;
                }
                else if (wejsciowy == 322)  //ł
                {
                    wyjscie = 8;
                }
                else if (wejsciowy == 323)  //Ń
                {
                    wyjscie = 9;
                }
                else if (wejsciowy == 324)  //ń
                {
                    wyjscie = 10;
                }
                else if (wejsciowy == 346)  //Ś
                {
                    wyjscie = 11;
                }
                else if (wejsciowy == 347)  //ś
                {
                    wyjscie = 12;
                }
                else if (wejsciowy == 377)  //Ź
                {
                    wyjscie = 13;
                }
                else if (wejsciowy == 378)  //ź
                {
                    wyjscie = 14;
                }
                else if (wejsciowy == 379)  //Ż
                {
                    wyjscie = 15;
                }
                else if (wejsciowy == 380)  //ż
                {
                    wyjscie = 16;
                }

                return wyjscie;
            }
            else
            {
                return wejsciowy;
            }

        }

        private int zamiana_na_polskiezpseudo(int wejsciowy)
        {
            if (wejsciowy < 17)
            {
                int wyjscie = 0;

                if (wejsciowy == 0)  //Ą
                {
                    wyjscie = 260;
                }
                else if (wejsciowy == 2)  //ą
                {
                    wyjscie = 261;
                }
                else if (wejsciowy == 3)  //ć duże
                {
                    wyjscie = 262;
                }
                else if (wejsciowy == 4)  //ć
                {
                    wyjscie = 263;
                }
                else if (wejsciowy == 5)  //Ę
                {
                    wyjscie = 280;
                }
                else if (wejsciowy == 6)  //ę
                {
                    wyjscie = 281;
                }
                else if (wejsciowy == 7)  //Ł
                {
                    wyjscie = 321;
                }
                else if (wejsciowy == 8)  //ł
                {
                    wyjscie = 322;
                }
                else if (wejsciowy == 9)  //Ń
                {
                    wyjscie = 323;
                }
                else if (wejsciowy == 10)  //ń
                {
                    wyjscie = 324;
                }
                else if (wejsciowy == 11)  //Ś
                {
                    wyjscie = 346;
                }
                else if (wejsciowy == 12)  //ś
                {
                    wyjscie = 347;
                }
                else if (wejsciowy == 13)  //Ź
                {
                    wyjscie = 377;
                }
                else if (wejsciowy == 14)  //ź
                {
                    wyjscie = 378;
                }
                else if (wejsciowy == 15)  //Ż
                {
                    wyjscie = 379;
                }
                else if (wejsciowy == 16)  //ż
                {
                    wyjscie = 380;
                }

                return wyjscie;
            }
            else
            {
                return wejsciowy;
            }

        }


        #region Kodowanie click
        private void Kodowanie_click(object sender, RoutedEventArgs e)
        {
            //encryption();
            Bitmap img = new Bitmap(Sciezka_obrazu.Text);
            int wielkosc = img.Height*img.Width;
            if (TextBoxmassage.Text.Length>wielkosc)
            {
                MessageBox.Show("Tekst jest za duży dla tego obrazka, zmień wiadomość albo obrazek.");
            }
            else
            {
           //TextBoxmassage.Text = RemoveAccent(TextBoxmassage.Text);
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
                        int value1 = Convert.ToInt32((letter));
                        
                        int value = zamiana_na_pseudopolskie(value1);

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

                img.Save(Sciezka_obrazu.Text, System.Drawing.Imaging.ImageFormat.Bmp);

                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                Obraz.Source = bitmap;
            }
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

                        int value1 = pixel.B;
                        int value = zamiana_na_polskiezpseudo(value1);
                        char c = Convert.ToChar(value);
                        
                        //string letter = System.Text.Encoding.ASCII.GetString(new byte[] {Convert.ToByte(c)});

                        massage = massage + c;
                        
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

        public string RemoveAccent(string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        private void Button_Click(object sender, RoutedEventArgs e) // wczytaj
        {
            var wczyt = new Wczytaj();
            //takie coś działa
            TextBoxmassage.Text = wczyt.odczyt_zawartosci();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // zapisz
        {
            var save = new Zapisz(TextBoxmassage.Text);
        }
    }
}
