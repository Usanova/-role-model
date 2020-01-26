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
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        Administration Administrator;
        public AddUser()
        {
            InitializeComponent();
        }
        public void ShowDialog(Administration Administrator)
        {
            this.Administrator = Administrator;
            this.ShowDialog();
        }

        private void BtAddNewUser_Click(object sender, RoutedEventArgs e)
        {
            string Name = tbNewUserName.Text;
            string Password = pbNewUserPassword.Password.ToString();
            if (Name == "")
            {
                MessageBox.Show(this, $"Введите имя!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Password == "")
            {
                MessageBox.Show(this, $"Введите пароль!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Administrator.AddUser(Name, Password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Пользователь с таким именем уже существует!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            this.Close();
        }
    }
}
