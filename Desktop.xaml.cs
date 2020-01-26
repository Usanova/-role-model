using Microsoft.Win32;
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
    /// Логика взаимодействия для Desktop.xaml
    /// </summary>
    public partial class Desktop : Window
    {
        Administration Administrator;
        string UserRole;
        string UserName;
        FileOnDesk fileOnDesk;
        StackPanel CurSp;
        int CountFileInPanel = 0;
        public Desktop()
        {
            InitializeComponent();
        }
        public void ShowDialog(Administration Administrator, string UserRole, string UserName)
        {
            this.Administrator = Administrator;
            this.UserRole = UserRole;
            this.UserName = UserName;
            tbUserRole.Text += $" {Roles.RoleNameToRussian(UserRole)}";
            fileOnDesk = new FileOnDesk(Administrator, UserRole, (Style)this.Resources["ImageButtonStyle"], Delete_File);
            FillDesktop();
            this.ShowDialog();
        }

        private void FillDesktop()
        {
            spForFiles.Children.Clear();
            CountFileInPanel = 0;
            foreach (string FileName in Administrator.FilesNames)
            {
                fileOnDesk.AddFileOnDesk(FileName, GetSpRow());
            }
        }

        private void CreateFile(object sender, RoutedEventArgs e)
        {
            fileOnDesk.CreateFile(GetSpRow());
        }
        private void Delete_File(object sender, RoutedEventArgs e)
        {
            Button FileButton = (Button)((ContextMenu)((MenuItem)sender).Parent).PlacementTarget;
            fileOnDesk.Delete(FileButton);
            FillDesktop();
        }
        private StackPanel GetSpRow()
        {
            if (CountFileInPanel == 0)
            {
                StackPanel NewSp = new StackPanel()
                {
                    MinHeight = 110,
                    Width = 800,
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 10, 0, 0)
                };
                spForFiles.Children.Add(NewSp);
                CurSp = NewSp;
            }
            CountFileInPanel++;
            if (CountFileInPanel == 5)
                CountFileInPanel = 0;
            return CurSp;
        }
        private void AddFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Text files|*.txt;";

            if (!(bool)openFile.ShowDialog())
            {
                return;
            }
            string[] name = openFile.FileName.Split(new char[] { '/', '\\', '.' });
            string PrelimFileName = name[name.Length - 2];
            string FileName = fileOnDesk.CreateFile(GetSpRow(), PrelimFileName);

            File.Copy(openFile.FileName, @"Files\" + FileName, true);
        }

        private void EndSession_Click(object sender, RoutedEventArgs e)
        {
            Administrator.SetPreviousRole(UserName, null);
            this.Close();
        }

        private void NotEndSession_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
