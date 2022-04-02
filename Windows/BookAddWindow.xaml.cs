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
using Library2ISP11_17_ZeyArt_DanArt.ClassHelper;
using Library2ISP11_17_ZeyArt_DanArt.EF;
using Microsoft.Win32;

namespace Library2ISP11_17_ZeyArt_DanArt.Windows
{
    /// <summary>
    /// Логика взаимодействия для BookAddWindow.xaml
    /// </summary>
    public partial class BookAddWindow : Window
    {

        EF.ViewBook editBook = new EF.ViewBook();
        string pathPhoto = null;
        bool isEdit = true;



        public BookAddWindow(EF.ViewBook book)
        {
            InitializeComponent();

            if (book.Preview != null)
            {
                using (MemoryStream stream = new MemoryStream((int)book.Preview))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    imgBook.Source = bitmapImage;
                }
            }

            tbTitle.Text = "Изменения данных книги";
            btAdd.Content = "Изменить";

            editBook = book;

            txtNameBook.Text = editBook.NameBook;
            txtPublishing.Text = editBook.NamePublishing;
            txtYearOfPublishing.Text = Convert.ToString(editBook.YearOfPublishing);
            txtGenre.Text = editBook.NameGenre;
            txtAuthor.Text = editBook.Nickname;
            txtNumberOfPages.Text = Convert.ToString(editBook.NumberOfPages);
            txtCost.Text = Convert.ToString(editBook.Cost);

            isEdit = true;
        }
            public BookAddWindow()
            {
                InitializeComponent();

                isEdit = false;
            }
            private void txtNameBook_TextChanged(object sender, TextChangedEventArgs e)
            {

            }

            private void txtPublishing_TextChanged(object sender, TextChangedEventArgs e)
            {

            }

            private void txtYearOfPublishing_TextChanged(object sender, TextChangedEventArgs e)
            {

            }

            private void txtGenre_TextChanged(object sender, TextChangedEventArgs e)
            {

            }

            private void txtAuthor_TextChanged(object sender, TextChangedEventArgs e)
            {

            }

            private void txtNumberOfPages_TextChanged(object sender, TextChangedEventArgs e)
            {

            }

            private void btAdd_Click(object sender, RoutedEventArgs e)
            {
                //проверка на пустоту
                if (string.IsNullOrWhiteSpace(txtNameBook.Text))
                {
                    MessageBox.Show("Поле Название книги не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCost.Text))
                {
                    MessageBox.Show("Поле Цена книги не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPublishing.Text))
                {
                    MessageBox.Show("Поле Издатель не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtYearOfPublishing.Text))
                {
                    MessageBox.Show("Поле Год публикации не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                //в разработке
                if (string.IsNullOrWhiteSpace(txtGenre.Text))
                {
                    MessageBox.Show("Поле Жанр не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtAuthor.Text))
                {
                    MessageBox.Show("Поле Автор не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNumberOfPages.Text))
                {
                    MessageBox.Show("Поле Кол-во страниц в книге не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                //проверка на кол-во символов

                if (txtNameBook.Text.Length > 100)
                {
                    MessageBox.Show("Недопустимое количество символов для поля Название книги", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (txtPublishing.Text.Length > 100)
                {
                    MessageBox.Show("Недопустимое количество символов для поля Издатель", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (txtYearOfPublishing.Text.Length > 5)
                {
                    MessageBox.Show("Недопустимое количество символов для поля Год публикации", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (txtGenre.Text.Length > 50)
                {
                    MessageBox.Show("Недопустимое количество символов для поля Жанр", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (txtAuthor.Text.Length > 100)
                {
                    MessageBox.Show("Недопустимое количество символов для поля Автор", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (txtNumberOfPages.Text.Length > 100)
                {
                    MessageBox.Show("Недопустимое количество символов для поля Кол-во страниц в книге", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (txtCost.Text.Length > 10)
                {
                    MessageBox.Show("Недопустимое количество символов для поля Цена книги ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            if (isEdit)
            {
                try
                {
                    //Изменение данных Книги
                    editBook.NameBook = txtNameBook.Text;
                    editBook.NamePublishing = txtPublishing.Text;
                    editBook.YearOfPublishing = Convert.ToInt32(txtYearOfPublishing.Text);
                    editBook.NameGenre = txtGenre.Text;
                    editBook.Nickname = txtAuthor.Text;
                    editBook.NumberOfPages = Convert.ToInt32(txtNumberOfPages.Text);
                    editBook.Cost = Convert.ToInt32(txtCost.Text);

                    if (pathPhoto != null)
                    {
                        editBook.Preview = File.ReadAllBytes(pathPhoto);
                    }

                    AppData.Context.SaveChanges();
                    MessageBox.Show("Успех", "Информация о книге изменена", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                try
                {
                    var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите добавление книги", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultClick == MessageBoxResult.Yes)
                    {
                        //Добавление новой книги
                        EF.ViewBook newBook = new EF.ViewBook();
                        newBook.NameBook = txtNameBook.Text;
                        newBook.NamePublishing = txtPublishing.Text;
                        newBook.YearOfPublishing = Convert.ToInt32(txtYearOfPublishing.Text);
                        newBook.NameGenre = txtGenre.Text;
                        newBook.Nickname = txtAuthor.Text;
                        newBook.NumberOfPages = Convert.ToInt32(txtNumberOfPages.Text);
                        newBook.Cost = Convert.ToInt32(txtCost.Text);
                        newBook.IsDeleted = false;


                        if (pathPhoto != null)
                        {
                            newBook.Preview = File.ReadAllBytes(pathPhoto);
                        }

                        AppData.Context.ViewBook.Add(newBook);

                        AppData.Context.SaveChanges();
                        MessageBox.Show("Успех", "Книга успешно добавлена", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

           }

        private void btnChoosePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                imgBook.Source = new BitmapImage(new Uri(openFileDialog.FileName));

                pathPhoto = openFileDialog.FileName;
            }

        }
    }
}


