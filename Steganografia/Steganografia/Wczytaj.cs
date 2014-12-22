using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Steganografia
{
    class Wczytaj
    {
        private string filename;

        public Wczytaj()
        {

        }

        public string odczyt_zawartosci()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog { DefaultExt = ".txt", Filter = "Text documents (.txt)|*.txt" };



            // Set filter for file extension and default file extension 


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                filename = dlg.FileName;
                var plik = new StreamReader(filename, Encoding.GetEncoding("Unicode"));
                return plik.ReadToEnd();
            }
            else
            {
                MessageBox.Show("Problem z otwarciem pliku");
                return "Błąd";
            }
        }
    }
}
