using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Ролевая_модель.Properties;

namespace Ролевая_модель
{
    public class FileOnDesk
    {
        Administration Administrator;
        string CurrentRole;
        public Dictionary<Button, string> ButtonFileName = new Dictionary<Button, string>();
        public Dictionary<Button, TextBlock> ButtonTextBlock = new Dictionary<Button, TextBlock>();
        public Dictionary<string, StackPanel> FileSmallPanel = new Dictionary<string, StackPanel>();
        public Dictionary<string, StackPanel> FileBigPanel = new Dictionary<string, StackPanel>();
        Style ButtonStyle;
        RoutedEventHandler Delete_File;
        public FileOnDesk(Administration Administrator, string CurrentRole, Style ButtonStyle, RoutedEventHandler Delete_File)
        {
            this.Administrator = Administrator;
            this.CurrentRole = CurrentRole;
            this.ButtonStyle = ButtonStyle;
            this.Delete_File = Delete_File;
        }
        //создаем файл
        public string CreateFile(StackPanel spDesktop, string PrelimFileName = "")
        {
            CreateFile createFile = new CreateFile();
            string FileName = createFile.ShowDialog(Administrator, PrelimFileName);

            Administrator.AddFile(FileName, CurrentRole);
            AddFileOnDesk(FileName, spDesktop);
            return FileName;
        }

        //добавляем новый файл в на панель
        public void AddFileOnDesk(string FileName, StackPanel spDesktop)
        {
            StackPanel spNewFile = new StackPanel()
            {
                MinHeight = 110,
                Width = 100,
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(50, 0, 0, 0)
            };
            Button btNewFile = new Button
            {
                Style = ButtonStyle,
                ContextMenu = GetContextMenu(FileName, Administrator.GetRights(CurrentRole, FileName).Delete)
            };
            btNewFile.Click += File_Click;
            btNewFile.MouseEnter += File_MouseEnter;
            btNewFile.MouseLeave += File_MouseLeave;
            var handel = Resources.txt.GetHbitmap();
            Image txt = new Image()
            {
                Height = 70,
                Source = Imaging.CreateBitmapSourceFromHBitmap(handel, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
            };
            btNewFile.Content = txt;
            TextBlock tbNewFile = new TextBlock()
            {
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 5, 0, 0),
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF7F0F0")),
                TextWrapping = TextWrapping.Wrap,
                Text = FileName
            };
            spNewFile.Children.Add(btNewFile);
            spNewFile.Children.Add(tbNewFile);
            FileSmallPanel[FileName] = spNewFile;
            ButtonFileName[btNewFile] = FileName;
            ButtonTextBlock[btNewFile] = tbNewFile;
            spDesktop.Children.Add(spNewFile);
            FileBigPanel[FileName] = spDesktop;
        }
        private ContextMenu GetContextMenu(string FileName, bool Delete)
        {
            ContextMenu contextMenu = new ContextMenu();
            MenuItem delete = new MenuItem()
            {
                Header = "Удалить"
            };
            delete.Click += Delete_File;
            if (!Delete)
                delete.IsEnabled = false;
            contextMenu.Items.Add(delete);
            return contextMenu;
        }
        private void File_Click(object sender, RoutedEventArgs e)
        {
            string FileName = ButtonFileName[(Button)sender];
            Rights rights = Administrator.GetRights(CurrentRole, FileName);
            if (rights.Read)
            {
                FileWin fileWin = new FileWin();
                fileWin.ShowDialog(FileName, rights.Write);
            }
            else if (rights.Write)
            {
                WriteToFile writeToFile = new WriteToFile();
                writeToFile.ShowDialog(FileName);
            }
            else
                MessageBox.Show($"У роли {Roles.RoleNameToRussian(CurrentRole)} нет доступа к этому файлу!", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void File_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonTextBlock[(Button)sender].Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4479C0"));
        }
        private void File_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonTextBlock[(Button)sender].Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF7F0F0"));
        }
        public void Delete(Button buttonFile)
        {
            string FileName = ButtonFileName[buttonFile];
            Delete(FileName);
        }
        public void Delete(string FileName)
        {
            FileBigPanel[FileName].Children.Remove(FileSmallPanel[FileName]);
            Administrator.RemoveFile(FileName);
        }
    }
}
