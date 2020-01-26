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
    /// Логика взаимодействия для FileWin.xaml
    /// </summary>
    public partial class FileWin : Window
    {
        string FileName;
        public FileWin()
        {
            InitializeComponent();
        }
        public void ShowDialog(string FileName, bool isWrite)
        {
            this.FileName = FileName;
            if (!isWrite)
            {
                rtbReference.IsReadOnly = true;
                btGrid.Children.Remove(btChange);
            }
            StreamReader sr = new StreamReader(@"Files\" + FileName, Encoding.GetEncoding(1251));
            string s = sr.ReadToEnd();
            rtbReference.Selection.Text = s;
            sr.Close();
            this.Show();

        }

        private void BtChange_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter(@"Files\" + FileName, false, Encoding.GetEncoding(1251));
            string s = new TextRange(rtbReference.Document.ContentStart, rtbReference.Document.ContentEnd).Text;
            s = s.Substring(0, s.Length - 2);
            sw.Write(s);
            sw.Close();
        }
    }
}
