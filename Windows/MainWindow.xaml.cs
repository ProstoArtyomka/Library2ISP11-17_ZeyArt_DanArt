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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Library2ISP11_17_ZeyArt_DanArt.ClassHelper;
using Library2ISP11_17_ZeyArt_DanArt.EF;

namespace Library2ISP11_17_ZeyArt_DanArt
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Book_Click(object sender, RoutedEventArgs e)
        {
            BookListWindow mainWindow = new BookListWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Reader_Click(object sender, RoutedEventArgs e)
        {
            UserListWindow mainWindow = new UserListWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
