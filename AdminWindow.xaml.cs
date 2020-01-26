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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        Administration Administrator;
        public AdminWindow()
        {
            InitializeComponent();
        }
        public void ShowDialog(Administration Administrator)
        {
            this.Administrator = Administrator;
            FillRoleMatrix();
            FillRightsMatrix();
            FillCbUsers();
            FillCbFiles();
            this.ShowDialog();
        }
        void FillRoleMatrix()
        {
            if (RolesGrid.RowDefinitions.Count != 0)
            { RolesGrid.RowDefinitions.RemoveRange(0, RolesGrid.RowDefinitions.Count); }
            if (RolesGrid.ColumnDefinitions.Count != 0)
            { RolesGrid.ColumnDefinitions.RemoveRange(0, RolesGrid.ColumnDefinitions.Count); }
            RolesGrid.Children.RemoveRange(0, RolesGrid.Children.Count);

            RolesGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(55) });
            for (int i = 0; i < Administrator.UsersNames.Count; i++)
            {
                RolesGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
            }
            RolesGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            for (int i = 0; i < Roles.roles.Count + 1; i++)
            {
                RolesGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(85) });
            }
            for (int i = 0; i < Administrator.UsersNames.Count - 1; i++)
            {
                AddInfOnGrid(Administrator.UsersNames[i + 1], i + 1, 0, RolesGrid);
            }
            for (int i = 0; i < Roles.roles.Count; i++)
            {
                AddInfOnGrid(Roles.RoleNameToRussian(Roles.roles[i]), 0, i + 1, RolesGrid);
            }
            for (int i = 0; i < Administrator.UsersNames.Count - 1; i++)
            {
                for (int j = 0; j < Roles.roles.Count; j++)
                {
                    AddInfoOfRoleGrid(Administrator.GetRoles(i + 1, j), i + 1, j + 1);
                }
            }
        }
        private void AddInfOnGrid(string Inf, int i, int j, Grid Matrix)
        {
            TextBlock tbName = new TextBlock() { Text = Inf, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, FontWeight = FontWeights.Bold };
            Matrix.Children.Add(tbName);
            Grid.SetRow(tbName, i);
            Grid.SetColumn(tbName, j);
        }
        //добавляем права в матрицу
        private void AddInfoOfRoleGrid(bool value, int i, int j)
        {
            GetCheckBoxForRole(i, j, value, Role_Cheked, Role_Uncheked);
        }
        private void GetCheckBoxForRole(int i, int j, bool IsRole, RoutedEventHandler Cheked, RoutedEventHandler Uncheked)
        {
            CheckBox cb = new CheckBox() { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            if (IsRole)
                cb.IsChecked = true;
            cb.Checked += Cheked;
            cb.Unchecked += Uncheked;
            RolesGrid.Children.Add(cb);
            Grid.SetRow(cb, i);
            Grid.SetColumn(cb, j);
        }
        private void Role_Cheked(object sender, RoutedEventArgs e)
        {
            SetRoles(sender, true);
        }
        private void Role_Uncheked(object sender, RoutedEventArgs e)
        {
            SetRoles(sender, false);
        }
        private void SetRoles(object sender, bool value)
        {
            CheckBox cb = (CheckBox)sender;
            int i = Grid.GetRow(cb) - 1;
            int j = Grid.GetColumn(cb) - 1;
            if(j == 0)
            {
                cb.IsChecked = true;
                return;
            }
            try
            {
                Administrator.SetRole(i + 1, j, value);
            }
            catch
            {
                MessageBox.Show("Вы не можете задавать пользователю более двух ролей одного типа!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                cb.IsChecked = false;
            }
        }


        void FillRightsMatrix()
        {
            if (RightsGrid.RowDefinitions.Count != 0)
            { RightsGrid.RowDefinitions.RemoveRange(0, RightsGrid.RowDefinitions.Count); }
            if (RightsGrid.ColumnDefinitions.Count != 0)
            { RightsGrid.ColumnDefinitions.RemoveRange(0, RightsGrid.ColumnDefinitions.Count); }
            RightsGrid.Children.RemoveRange(0, RightsGrid.Children.Count);

            RightsGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(55) });
            for (int i = 0; i < Administrator.FilesNames.Count + 1; i++)
            {
                RightsGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(65) });
            }
            RightsGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            for (int i = 0; i < Roles.roles.Count + 1; i++)
            {
                RightsGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(85) });
            }
            for (int i = 0; i < Administrator.FilesNames.Count; i++)
            {
                AddInfOnGrid(Administrator.FilesNames[i], i + 1, 0, RightsGrid);
            }
            for (int i = 0; i < Roles.roles.Count; i++)
            {
                AddInfOnGrid(Roles.RoleNameToRussian(Roles.roles[i]), 0, i + 1, RightsGrid);
            }
            for (int i = 0; i < Administrator.FilesNames.Count; i++)
            {
                for (int j = 0; j < Roles.roles.Count; j++)
                {
                    AddInfOnRightsGrid(Administrator.GetRights(i, j), i + 1, j + 1);
                }
            }
        }
        private void AddInfOnRightsGrid(Rights rights, int i, int j)
        {
            StackPanel sp = new StackPanel() { Orientation = Orientation.Vertical };
            GetCheckBoxForRight("read", rights.Read, Read_Cheked, Read_Uncheked, sp);
            GetCheckBoxForRight("write", rights.Write, Write_Cheked, Write_Uncheked, sp);
            GetCheckBoxForRight("delete", rights.Delete, Delete_Cheked, Delete_Uncheked, sp);
            RightsGrid.Children.Add(sp);
            Grid.SetRow(sp, i);
            Grid.SetColumn(sp, j);

        }
        private void GetCheckBoxForRight(string content, bool Right, RoutedEventHandler Cheked, RoutedEventHandler Uncheked, StackPanel sp)
        {
            CheckBox cb = new CheckBox() { Margin = new Thickness(15, 5, 0, 0) };
            cb.Content = content;
            if (Right)
                cb.IsChecked = true;
            cb.Checked += Cheked;
            cb.Unchecked += Uncheked;
            sp.Children.Add(cb);
        }
        private void Read_Cheked(object sender, RoutedEventArgs e)
        {
            SetRights(sender, "r", true);
        }
        private void Read_Uncheked(object sender, RoutedEventArgs e)
        {
            SetRights(sender, "r", false);
        }
        private void Write_Cheked(object sender, RoutedEventArgs e)
        {
            SetRights(sender, "w", true);
        }
        private void Write_Uncheked(object sender, RoutedEventArgs e)
        {
            SetRights(sender, "w", false);
        }
        private void Delete_Cheked(object sender, RoutedEventArgs e)
        {
            SetRights(sender, "d", true);
        }
        private void Delete_Uncheked(object sender, RoutedEventArgs e)
        {
            SetRights(sender, "d", false);
        }
        private void SetRights(object sender, string RightName, bool value)
        {
            int i = Grid.GetRow((StackPanel)((CheckBox)sender).Parent) - 1;
            int j = Grid.GetColumn((StackPanel)((CheckBox)sender).Parent) - 1;
            Administrator.SetRights(i, j, RightName, value);
            FillRightsMatrix();
        }
        private void FillCbUsers()
        {
            cbUsers.Items.Clear();
            for (int i = 1; i < Administrator.UsersNames.Count; i++)
            {
                cbUsers.Items.Add(new ComboBoxItem() { Content = Administrator.UsersNames[i] });
            }
        }
        private void FillCbFiles()
        {
            cbFiles.Items.Clear();
            foreach (string FilesName in Administrator.FilesNames)
            {
                cbFiles.Items.Add(new ComboBoxItem() { Content = FilesName });
            }
        }
        private void BtDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)cbUsers.SelectedItem;
            if (cbi != null)
            {
                string UserName = (string)cbi.Content;
                Administrator.RemoveUser(UserName);
                FillRoleMatrix();
                FillCbUsers();
            }
        }

        private void BtDeleteFile_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)cbFiles.SelectedItem;
            if (cbi != null)
            {
                string FileName = (string)cbi.Content;
                Administrator.RemoveFile(FileName);
                FillRightsMatrix();
                FillCbFiles();
            }
        }

        private void BtAddFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Text files|*.txt;";

            if (!(bool)openFile.ShowDialog())
            {
                return;
            }
            string[] name = openFile.FileName.Split(new char[] { '/', '\\', '.' });
            string PrelimFileName = name[name.Length - 2];

            CreateFile createFile = new CreateFile();
            string FileName = createFile.ShowDialog(Administrator, PrelimFileName);

            Administrator.AddFile(FileName);
            File.Copy(openFile.FileName, @"Files\" + FileName, true);
            FillRightsMatrix();
            FillCbFiles();
        }
    }
}
