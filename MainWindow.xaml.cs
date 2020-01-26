using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
using System.IO;

namespace Ролевая_модель
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Administration Administrator = new Administration();
        public MainWindow()
        {
            InitializeComponent();
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(@"Files/Administrator.txt", FileMode.Open);
            Administrator = (Administration)formatter.Deserialize(fs);
            fs.Close();

            tbRegister.TextDecorations = TextDecorations.Underline;
        }
        private void BtRegister_Click(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.ShowDialog(Administrator);
        }

        private void BtRegister_MouseEnter(object sender, MouseEventArgs e)
        {
            tbRegister.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFED134A"));
        }

        private void BtRegister_MouseLeave(object sender, MouseEventArgs e)
        {
            tbRegister.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2E6EAE"));

        }
        private void BtLogin_Click(object sender, RoutedEventArgs e)
        {
            string Name = tbUserName.Text;
            string Password = pbUserPassword.Password.ToString();
            if (Administrator.ContainsUser(Name, Password) && Name == "Администратор")
            {
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Owner = this;
                adminWindow.ShowDialog(Administrator);
            }
            else if (Administrator.ContainsUser(Name, Password))
            {
                string PreviousRole = Administrator.GetPreviousRole(Name);
                if (PreviousRole == null)
                {
                    RoleChoice desktop = new RoleChoice();
                    desktop.ShowDialog(Administrator, Name);
                }
                else if (MessageBox.Show("Продолжить предыдущую ссесию?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Desktop desktop = new Desktop();
                    desktop.ShowDialog(Administrator, PreviousRole, Name);  
                }
                else
                {
                    Administrator.SetPreviousRole(Name, null);
                    RoleChoice desktop = new RoleChoice();
                    desktop.ShowDialog(Administrator, Name);
                }

            }
            else
            {
                MessageBox.Show(this, $"Неверное имя пользователя или пароль!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            tbUserName.Text = "";
            pbUserPassword.Password = "";
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(@"Files/Administrator.txt", FileMode.Open);
            formatter.Serialize(fs, Administrator);
            fs.Close();
        }

    }
}
