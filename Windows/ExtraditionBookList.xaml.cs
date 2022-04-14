using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
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
using System.Xml.Schema;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Wordprocessing;
using Library2ISP11_17_ZeyArt_DanArt.ClassHelper;
using Library2ISP11_17_ZeyArt_DanArt.EF;

namespace Library2ISP11_17_ZeyArt_DanArt.Windows
{
    /// <summary>
    /// Логика взаимодействия для ExtraditionBookList.xaml
    /// </summary>
    public partial class ExtraditionBookList : Window
    {
        List<Extradition> ExtraditionList = new List<Extradition>();
    
        List<string> listSort = new List<string>() { "По умолчанию", "По дате выдачи", "По дате возврата", "По названию книги", "По фамилии клиента", "По имени клиента", "По телефону клиента", "По адрессу клиента", "По фамилии сотрудника", "По цене долга", };

        List<string> listSortIsDebt = new List<string>() { "Долга нету", "Долг есть" };

        public ExtraditionBookList()
        {
            InitializeComponent();

            listExtradition.ItemsSource = AppData.Context.Extradition.ToList();


            cmbSortIsDebt.ItemsSource = listSortIsDebt;
            cmbSortIsDebt.SelectedIndex = 1;

            cmbSort.ItemsSource = listSort;
            cmbSort.SelectedIndex = 0;

            Filter();
        }

        private void Filter()
        {
            ExtraditionList = AppData.Context.Extradition.ToList();
            ExtraditionList = ExtraditionList.
               Where(i => i.DateExtradition.ToString().Contains(txtSearch.Text.ToLower()) 
                     || i.DateReturn.ToString() .Contains(txtSearch.Text.ToLower())
                     || i.Book.NameBook.ToLower().Contains(txtSearch.Text.ToLower())
                     || i.Client.LastName.ToLower().Contains(txtSearch.Text.ToLower())
                     || i.Client.FirstName.ToLower().Contains(txtSearch.Text.ToLower())
                     || i.Client.Phone.ToLower().Contains(txtSearch.Text.ToLower())
                     || i.Client.Address.ToLower().Contains(txtSearch.Text.ToLower())
                     || i.Employee.LastName.ToLower().Contains(txtSearch.Text.ToLower())
                     || i.ClientDebt.ToString().Contains(txtSearch.Text.ToLower())).ToList();

            if (cmbSortIsDebt.SelectedIndex == 0)
            {
                ExtraditionList = ExtraditionList.Where(i => Convert.ToInt32(i.ClientDebt) == 0).ToList();
            }
            else
            {
                ExtraditionList = ExtraditionList.Where(i => Convert.ToInt32(i.ClientDebt) > 0).ToList();
            }
            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    ExtraditionList = ExtraditionList.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    ExtraditionList = ExtraditionList.OrderBy(i => i.DateExtradition).ToList();
                    break;
                case 2:
                    ExtraditionList = ExtraditionList.OrderByDescending(i => i.DateReturn).ToList();
                    break;
                case 3:
                    ExtraditionList = ExtraditionList.OrderBy(i => i.Book.NameBook).ToList();
                    break;
                case 4:
                    ExtraditionList = ExtraditionList.OrderBy(i => i.Client.LastName).ToList();
                    break;
                case 5:
                    ExtraditionList = ExtraditionList.OrderBy(i => i.Client.FirstName).ToList();
                    break;
                case 6:
                    ExtraditionList = ExtraditionList.OrderBy(i => i.Client.Phone).ToList();
                    break;
                case 7:
                    ExtraditionList = ExtraditionList.OrderBy(i => i.Client.Address).ToList();
                    break;
                case 8:
                    ExtraditionList = ExtraditionList.OrderBy(i => i.Employee.LastName).ToList();
                    break;
                case 9:
                    ExtraditionList = ExtraditionList.OrderByDescending(i => i.ClientDebt).ToList();
                    break;
                default:
                    ExtraditionList = ExtraditionList.OrderBy(i => i.ID).ToList();
                    break;
            }
           listExtradition.ItemsSource = ExtraditionList;
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.ShowDialog();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            AddExtradutionWindow window = new AddExtradutionWindow();
            this.Opacity = 0.2;
            window.ShowDialog();
            listExtradition.ItemsSource = AppData.Context.Extradition.ToList();
            this.Opacity = 1;
            Filter();
        }

        private void listExtradition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        
        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void cmbSortIsDebt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void listExtradition_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Delete || e.Key == System.Windows.Input.Key.Back)
            {
                var item = listExtradition.SelectedItem as EF.Extradition;
                if (listExtradition.SelectedItem is EF.Extradition || listExtradition.SelectedIndex != -1)
                {
                    try
                    {
                        var resultClick = MessageBox.Show("Вы уверены?", "Подтверите Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resultClick == MessageBoxResult.Yes)
                        {
                            AppData.Context.Extradition.Remove(item);
                            AppData.Context.SaveChanges();
                            MessageBox.Show("Запись о выдаче книги успешно удалена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                            Filter();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            Filter();
        }

        private void listExtradition_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            var editExtradition = new EF.Extradition();
            if (listExtradition.SelectedItem is EF.Extradition)
            {
                editExtradition = listExtradition.SelectedItem as EF.Extradition;          
            }

            AddExtradutionWindow extradutionWindow = new AddExtradutionWindow(editExtradition);
            this.Opacity = 0.2;
            extradutionWindow.ShowDialog();
            listExtradition.ItemsSource = AppData.Context.Extradition.ToList();
            this.Opacity = 1;
            Filter();
        }
    }
}
