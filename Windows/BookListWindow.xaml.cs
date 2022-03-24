﻿using System;
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
    /// Логика взаимодействия для BookListWindow.xaml
    /// </summary>
    public partial class BookListWindow : Window
    {


        List<Book> bookList = new List<Book>();

        List<string> listSort = new List<string>() { "По умолчанию","По названию книги", "По издателю", "По году публикации", "По жанру", "По автору", "По кол-ву страниц в книге" , "По цене" , "По статусу удаления" };
        public BookListWindow()
        {
            InitializeComponent();

            listBook.ItemsSource = AppData.Context.Book.ToList();

            cmbSort.ItemsSource = listSort;
            cmbSort.SelectedIndex = 0;

            Filter();

            
        }


        private void Update_Click(object sender, RoutedEventArgs e)
        {
            BookAddWindow window = new BookAddWindow();
            this.Opacity = 0.2;
            window.ShowDialog();
            listBook.ItemsSource = AppData.Context.Book.ToList();
            this.Opacity = 1;
        }

            private void Filter()
            {
            bookList = AppData.Context.Book.ToList();
            bookList = bookList.
                Where(i => i.NameBook.ToLower().Contains(txtSearch.Text.ToLower()) || i.Publishing.NamePublishing.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    bookList = bookList.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    bookList = bookList.OrderBy(i => i.NameBook).ToList();
                    break;
                case 2:
                    bookList = bookList.OrderBy(i => i.Publishing.NamePublishing).ToList();
                    break;
                case 3:
                    bookList = bookList.OrderByDescending(i => i.YearOfPublishing).ToList();
                    break;
                case 4:
                    //bookList = bookList.OrderByDescending(i => i.Genre).ToList();
                    break;
                case 5:
                    //bookList = bookList.OrderByDescending(i => i.Author).ToList();
                    break;
                case 6:
                    bookList = bookList.OrderByDescending(i => i.NumberOfPages).ToList();
                    break;
                case 7:
                    bookList = bookList.OrderByDescending(i => i.Cost).ToList();
                    break;
                case 8:
                    bookList = bookList.OrderBy(i => i.IsDeleted).ToList();
                    break;
                default:
                    bookList = bookList.OrderBy(i => i.ID).ToList();
                   break;
            }

            listBook.ItemsSource = bookList;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void listBook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }  
        private void listBook_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Delete || e.Key == System.Windows.Input.Key.Back)
            {
               var item = listBook.SelectedItem as EF.Book;
                 if (listBook.SelectedItem is EF.Book || listBook.SelectedIndex != -1)
                 {
                     try
                     {
                         var resultClick = MessageBox.Show("Вы уверены?", "Подтверите Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                         if (resultClick == MessageBoxResult.Yes)
                         {
                             //Изменения IsDeleted на True
                             item.IsDeleted = true;
                             AppData.Context.SaveChanges();
                             MessageBox.Show("Книга успешно удалена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                             Filter();
                         }
                     }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.Message.ToString());
                     }
                 }
            }
        }

        private void listBook_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var editBook = new EF.Book();
            if (listBook.SelectedItem is EF.Book)
            {
                editBook = listBook.SelectedItem as EF.Book;
            }

            BookAddWindow addReaderWindow = new BookAddWindow(editBook);
            this.Opacity = 0.2;
            addReaderWindow.ShowDialog();
            listBook.ItemsSource = AppData.Context.Book.ToList();
            this.Opacity = 1;
        }
    }
}
