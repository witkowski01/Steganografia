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

        public void encryption()
{
    var originalbmp = new Bitmap(Bitmap.FromFile("steganografia.png")); //Actual image used to encrypt the message
           
      var encryptbmp = new Bitmap(originalbmp.Width, originalbmp.Height); // To hold the encrypted image     

            var originalText = "To ten tekst szyfruje";
      var ascii = new List<int>(); // To store individual value of the pixels 
      foreach (char character in originalText)
      {
             int asciiValue = Convert.ToInt16(character); // Convert the character to ASCII
             var firstDigit = asciiValue / 1000; // Extract the first digit of the ASCII
             var secondDigit = (asciiValue - (firstDigit * 1000)) / 100; //Extract the second digit of the ASCII
             var thirdDigit = (asciiValue - ((firstDigit * 1000) + (secondDigit * 100)))/10;//Extract the third digit of the ASCII
             var fourthDigit = (asciiValue - ((firstDigit * 1000) + (secondDigit * 100)+(thirdDigit * 10))); //Extract the third digit of the ASCII
             ascii.Add(firstDigit); // Add the first digit of the ASCII
             ascii.Add(secondDigit); // Add the second digit of the ASCII
             ascii.Add(thirdDigit); // Add the third digit of the ASCII
             ascii.Add(fourthDigit ); // Add the fourth digit of the ASCII
     }
     var count = 0; // Have a count
     for (int row = 0; row < originalbmp.Width; row++) // Indicates row number
     {
           for (int column = 0; column < originalbmp.Height; column++) // Indicate column number
           {
                 var color = originalbmp.GetPixel(row, column); // Get the pixel from each and every row and column
                 encryptbmp.SetPixel(row, column, System.Drawing.Color.FromArgb(color.A -((count < ascii .Count ) ? ascii[count]:0), color)); // Set ascii value in A of the pixel
           }
    }
    encryptbmp.Save("EncryptedImage.png", ImageFormat.Png); // Save the encrypted image   
}

        private void decryption()
        {
            var characterValue = 0; // Have a variable to store the ASCII value of the character
            string encryptedText = string.Empty; // Have a variable to store the encrypted text
            var ascii = new List<int>(); // Have a collection to store the collection of ASCII
            var encryptbmp = new Bitmap(Bitmap.FromFile("EncryptedImage.png")); // Load the transparent image

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            decryption();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            encryption();
        }
    }
}
