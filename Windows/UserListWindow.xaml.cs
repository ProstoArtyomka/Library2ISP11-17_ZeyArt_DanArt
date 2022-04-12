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
using Library2ISP11_17_ZeyArt_DanArt.ClassHelper;
using Library2ISP11_17_ZeyArt_DanArt.EF;

namespace Library2ISP11_17_ZeyArt_DanArt.Windows
{
    /// <summary>
    /// Логика взаимодействия для UserListWindow.xaml
    /// </summary>
    public partial class UserListWindow : Window
    {

        List<Client> readerList = new List<Client>();

        List<string> listSort = new List<string>() { "По умолчанию", "По фамилии", "По имени", "По адресу", "По рейтингу" };

        List<string> listSortIsDeleted = new List<string>() {"Не удален", "Удален" };


        public UserListWindow()
        {
            InitializeComponent();

            listReader.ItemsSource = AppData.Context.Client.ToList();

            cmbSortIsDeleted.ItemsSource = listSortIsDeleted;
            cmbSortIsDeleted.SelectedIndex = 0;

            cmbSort.ItemsSource = listSort;
            cmbSort.SelectedIndex = 0;

            Filter();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.ShowDialog();
        }

        private void listReader_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            AddReaderWindow window = new AddReaderWindow();
            this.Opacity = 0.2;
            window.ShowDialog();
            listReader.ItemsSource = AppData.Context.Client.ToList();
            this.Opacity = 1;
        }

        private void Filter()
        {
            readerList = AppData.Context.Client.ToList();
            readerList = readerList.
                Where(i => i.LastName.ToLower().Contains(txtSearch.Text.ToLower()) 
                || i.FirstName.ToLower().Contains(txtSearch.Text.ToLower())
                || i.Address.ToLower().Contains(txtSearch.Text.ToLower())
                || i.FirstName.ToLower().Contains(txtSearch.Text.ToLower())
                || i.Rating.ToString().Contains(txtSearch.Text.ToLower())
                || i.IsDeleted.ToString().Contains(txtSearch.Text.ToLower())).ToList();

            if (cmbSortIsDeleted.SelectedIndex == 0)
            {
                readerList = readerList.Where(i => Convert.ToInt32(i.IsDeleted) == 0).ToList();
            }
            else 
            {
                readerList = readerList.Where(i => Convert.ToInt32(i.IsDeleted) == 1).ToList();
            }

            switch (cmbSort.SelectedIndex)
            { 
            case 0:
                readerList = readerList.OrderBy(i => i.ID).ToList();
                break;
            case 1:
                readerList = readerList.OrderBy(i => i.LastName).ToList();
                break;
            case 2:
                readerList = readerList.OrderBy(i => i.FirstName).ToList();
                break;
            case 3:
                readerList = readerList.OrderBy(i => i.Address).ToList();
                break;
            case 4:
                readerList = readerList.OrderByDescending(i => i.Rating).ToList();
                break;
            default:
                readerList = readerList.OrderBy(i => i.ID).ToList();
                break;
            }
    
          listReader.ItemsSource = readerList;
        }

         

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void cmbSortIsDeleted_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }


        private void lvReader_KeyUp(object sender,System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Delete || e.Key == System.Windows.Input.Key.Back)
            {
                var item = listReader.SelectedItem as EF.Client;
                if (listReader.SelectedItem is EF.Client || listReader.SelectedIndex != -1)
                {
                    try
                    {
                        var resultClick = MessageBox.Show("Вы уверены?", "Подтверите Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resultClick == MessageBoxResult.Yes)
                        {
                            //изменение IsDeleted на True
                            item.IsDeleted = true;
                            AppData.Context.SaveChanges();
                            MessageBox.Show("Пользователь успешно удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                            Filter();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void listReader_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var editReader = new EF.Client();
            if (listReader.SelectedItem is EF.Client)
            {
                editReader = listReader.SelectedItem as EF.Client;
            }

            AddReaderWindow addReaderWindow = new AddReaderWindow(editReader);
            this.Opacity = 0.2;
            addReaderWindow.ShowDialog();
            listReader.ItemsSource = AppData.Context.Client.ToList();
            this.Opacity = 1;
        }
    }
}
