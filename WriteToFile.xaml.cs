using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace Ролевая_модель
{
    /// <summary>
    /// Логика взаимодействия для WriteToFile.xaml
    /// </summary>
    public partial class WriteToFile : Window
    {
        string FileName;
        public WriteToFile()
        {
            InitializeComponent();
        }
        public void ShowDialog(string FileName)
        {
            this.FileName = FileName;
            this.ShowDialog();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(@"Files\" + FileName, true, Encoding.GetEncoding(1251));
            string s = new TextRange(rtbReference.Document.ContentStart, rtbReference.Document.ContentEnd).Text;
            s = s.Substring(0, s.Length - 2);
            sw.Write(s);
            sw.Close();
        }
    }
}
