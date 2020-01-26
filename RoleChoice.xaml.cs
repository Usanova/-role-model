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
    /// Логика взаимодействия для RoleChoice.xaml
    /// </summary>
    public partial class RoleChoice : Window
    {
        Administration Administrator;
        string UserName;
        public RoleChoice()
        {
            InitializeComponent();
        }
        public void ShowDialog(Administration Administrator, string UserName)
        {
            this.Administrator = Administrator;
            this.UserName = UserName;
            foreach(string Role in Roles.roles)
                if (Administrator.GetRoles(UserName, Role))
                    spRoles.Children.Add(GetRadioButton(Roles.RoleNameToRussian(Role)));
            this.ShowDialog();
        }
        private RadioButton GetRadioButton(string Content)
        {
            return new RadioButton()
            {
                Content = Content,
                GroupName = "Roles",
                FontSize = 14,
                FontFamily = new FontFamily("Segoe UI Semibold"),
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(10, 10, 0, 0)
            };
        }
        private void BtEntrance_Click(object sender, RoutedEventArgs e)
        {
            foreach (RadioButton rb in spRoles.Children)
            {
                if (rb.IsChecked == true)
                {
                    this.Close();
                    string Role = Roles.RoleNameToEnglish((string)rb.Content);
                    Administrator.SetPreviousRole(UserName, Role);
                    Desktop desktop = new Desktop();
                    desktop.ShowDialog(Administrator, Role, UserName);
                    return;
                }
            }
            MessageBox.Show("Выберите роль!", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
