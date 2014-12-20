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

        #region Stare nieużywane Kodowanie i dekodowanie
        public void encryption() // kodowanie
        {
            var originalbmp = new Bitmap(Bitmap.FromFile(Sciezka_obrazu.Text));
    //var originalbmp = new Bitmap(Bitmap.FromFile("steganografia.png")); //Actual image used to encrypt the message
           
      var encryptbmp = new Bitmap(originalbmp.Width, originalbmp.Height); // To hold the encrypted image     

            var originalText = "To ten tekst szyfruje";
      var ascii = new List<int>(); // To store individual value of the pixels 
      foreach (char character in originalText)
      {
             int asciiValue = Convert.ToInt16(character); // Convert the character to ASCII
             var firstZnak = asciiValue / 1000; // Extract the first Znak of the ASCII
             var secondZnak = (asciiValue - (firstZnak * 1000)) / 100; //Extract the second Znak of the ASCII
             var thirdZnak = (asciiValue - ((firstZnak * 1000) + (secondZnak * 100)))/10;//Extract the third Znak of the ASCII
             var fourthZnak = (asciiValue - ((firstZnak * 1000) + (secondZnak * 100)+(thirdZnak * 10))); //Extract the third Znak of the ASCII
             ascii.Add(firstZnak); // Add the first Znak of the ASCII
             ascii.Add(secondZnak); // Add the second Znak of the ASCII
             ascii.Add(thirdZnak); // Add the third Znak of the ASCII
             ascii.Add(fourthZnak ); // Add the fourth Znak of the ASCII
     }
     var count = 0; // Have a count
            int numer = 0;
     for (int row = 0; row < originalbmp.Width; row++) // Indicates row number
     {
           for (int column = 0; column < originalbmp.Height; column++) // Indicate column number
           {
                 var color = originalbmp.GetPixel(row, column); // Get the pixel from each and every row and column

                 encryptbmp.SetPixel(row, column, Color.FromArgb(color.A - ((count < ascii.Count) ? ascii[column] : 0), color)); // Set ascii value in A of the pixel
               
           }
           
    }
    encryptbmp.Save("Kodowany.png", ImageFormat.Png); // Save the encrypted image   
}

        private void decryption()
        {
            var characterValue = 0; // Have a variable to store the ASCII value of the character
            string encryptedText = string.Empty; // Have a variable to store the encrypted text
            var ascii = new List<int>(); // Have a collection to store the collection of ASCII
            var encryptbmp = new Bitmap(Bitmap.FromFile("Kodowany.png")); // Load the transparent image

            for (int row = 0; row < encryptbmp.Width; row++) // Indicates row number
            {
                for (int column = 0; column < encryptbmp.Height; column++) // Indicate column number
                {
                    var color = encryptbmp.GetPixel(row, column); // Get the pixel from each and every row and column
                    ascii.Add(255 - color.A); // Get the ascii value from A value since 255 is default value
                }
            }
            for (int i = 0; i < ascii.Count; i++)
            {
                characterValue = 0;
                characterValue += ascii[i] * 1000; // Get the first digit of the ASCII value of the encrypted character
                i++;
                characterValue += ascii[i] * 100; // Get the second digit of the ASCII value of the encrypted character
                i++;
                characterValue += ascii[i] * 10;  // Get the first third digit of the ASCII value of the encrypted character
                i++;
                characterValue += ascii[i]; // Get the first fourth digit of the ASCII value of the encrypted character
                if (characterValue != 0)
                    encryptedText += char.ConvertFromUtf32(characterValue); // Convert the ASCII characterValue into character
            }
            MessageBox.Show(encryptedText); // Showing the encrypted message in message box
        }

        #endregion

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
