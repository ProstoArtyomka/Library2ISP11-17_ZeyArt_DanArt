using Library2ISP11_17_ZeyArt_DanArt.ClassHelper;
using Library2ISP11_17_ZeyArt_DanArt.Windows;
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
using Library2ISP11_17_ZeyArt_DanArt.EF;

namespace Library2ISP11_17_ZeyArt_DanArt
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void AuthEnter_Click(object sender, RoutedEventArgs e)
        {
            var userAuth = AppData.Context.Employee.ToList().
                Where(i => i.Login == UserLogin.Text && i.Password == UserPassword.Text).
                FirstOrDefault();
            if (userAuth!=null)
            {
                //это верное решение
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь с такими данными не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
