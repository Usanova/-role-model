using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для CreateFile.xaml
    /// </summary>
    public partial class CreateFile : Window
    {
        Administration Administrator;
        string FileName;
        public CreateFile()
        {
            InitializeComponent();
        }
        public string ShowDialog(Administration Administrator, string PrelimFileName)
        {
            this.Administrator = Administrator;
            tbNewFileName.Text = PrelimFileName;
            this.ShowDialog();
            return FileName;
        }

        private void BtAddNewFile_Click(object sender, RoutedEventArgs e)
        {
            string NewFileName = tbNewFileName.Text;
            if (NewFileName == "")
            {
                MessageBox.Show(this, $"Введите имя файла!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Administrator.FilesNames.Contains(NewFileName + ".txt"))
            {
                MessageBox.Show(this, $"Файл с таким именем уже существует!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            FileName = NewFileName + ".txt";
            this.Close();
        }
    }
}
