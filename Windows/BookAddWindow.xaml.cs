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

        EF.Book editBook = new EF.Book();
        string pathPhoto = null;
        bool isEdit = true;



        public BookAddWindow(EF.Book book)
        {
            InitializeComponent();

            if (book.Preview != null)
            {
                using (MemoryStream stream = new MemoryStream((byte[])book.Preview))
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
            btAdd.Content = "Изменить книгу";

            editBook = book;

            txtNameBook.Text = editBook.NameBook;
            var Publishing = AppData.Context.Publishing.ToList().
                        Where(i => i.ID == book.IDPublishing).
                        FirstOrDefault();
            txtPublishing.Text = Publishing.NamePublishing;
            txtYearOfPublishing.Text = Convert.ToString(editBook.YearOfPublishing);
            var GenreBook = AppData.Context.Genre.ToList().
                        Where(i => i.ID == book.IDGenreBook).
                        FirstOrDefault();
            txtGenre.Text = GenreBook.NameGenre;
            var AuthorBook = AppData.Context.Author.ToList().
                        Where(i => i.ID == book.IDAuthorBook).
                        FirstOrDefault();
            txtAuthor.Text = AuthorBook.Nickname;
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

            if (txtYearOfPublishing.Text.Length > 4)
            {
                MessageBox.Show("Недопустимое количество символов для поля Год публикации", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtGenre.Text.Length > 50)
            {
                MessageBox.Show("Недопустимое количество символов для поля Жанр", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtAuthor.Text.Length > 50)
            {
                MessageBox.Show("Недопустимое количество символов для поля Автор", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if ((txtNumberOfPages.Text.Length > 4) || (txtNumberOfPages.Text.Length < 1))
            {
                MessageBox.Show("Недопустимое количество символов для поля Кол-во страниц в книге", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtCost.Text.Length > 8)
            {
                MessageBox.Show("Недопустимое количество символов для поля Цена книги ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Convert.ToDouble(txtCost.Text.Length) < 0.00)
            {
            MessageBox.Show("Недопустимая цена для поля Цена книги ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
            }

            //Валидация Издателя
            if (AppData.Context.Publishing.Where(i => i.NamePublishing == txtPublishing.Text).FirstOrDefault() == null)
            {
                MessageBox.Show("Такого Издателя не существует в базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Валидация Жанра
            if (AppData.Context.Genre.Where(i => i.NameGenre == txtGenre.Text).FirstOrDefault() == null)
            {
                MessageBox.Show("Такого Жанра не существует в базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Валидация Автора
            //if (AppData.Context.Author.Where(i => i.Nickname == txtAuthor.Text).FirstOrDefault() == null)
            //{
            //    MessageBox.Show("Такого Автора не существует в базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //Валидация Страниц в книге
            string NumberOfPages = txtNumberOfPages.Text;
            if (NumberOfPages.Any(Char.IsUpper) || (NumberOfPages.Any(Char.IsLower) || (NumberOfPages.Any(Char.IsPunctuation) || (NumberOfPages.Any(Char.IsWhiteSpace)))))
            {
                MessageBox.Show("Поле Кол-во страниц в книге может содержать только ПОЛОЖИТЕЛЬНЫЕ цифры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //Валидация года публикации
            string YearOfPublishing = txtYearOfPublishing.Text;
            if (YearOfPublishing.Any(Char.IsUpper) || (YearOfPublishing.Any(Char.IsLower) || (YearOfPublishing.Any(Char.IsPunctuation) || (YearOfPublishing.Any(Char.IsWhiteSpace)))))
            {
                MessageBox.Show("Поле Год публикации может содержать только ПОЛОЖИТЕЛЬНЫЕ цифры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Convert.ToInt32(YearOfPublishing) < 0)
            {
                MessageBox.Show("Поле Год публикации не может быть отрицательным значением", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (true)
            {

            }


            //Валидация Долга
            if ((txtCost.Text.Length > 8) || (txtCost.Text.Length < 1))
                {
                    MessageBox.Show("Недопустимое количество символов для поля Цена книги", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (Convert.ToDouble(txtCost.Text) < 0.00)
                {
                    MessageBox.Show("Недопустимое значение для поля Цена книги", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (isEdit)
                        {
                            try
                            {
                                //Изменение данных Книги
                                editBook.NameBook = txtNameBook.Text;
                                var Publishing = AppData.Context.Publishing.ToList().
                                                Where(i => i.NamePublishing == txtPublishing.Text).
                                                FirstOrDefault();
                                Publishing.NamePublishing = txtPublishing.Text;
                                editBook.YearOfPublishing = Convert.ToInt32(txtYearOfPublishing.Text);
                                var GenreBook = AppData.Context.Genre.ToList().
                                    Where(i => i.NameGenre == txtGenre.Text).
                                    FirstOrDefault();
                                GenreBook.NameGenre = txtGenre.Text;
                                var AuthorBook = AppData.Context.Author.ToList().
                                            Where(i => i.Nickname == txtAuthor.Text).
                                            FirstOrDefault();
                                AuthorBook.Nickname = txtAuthor.Text;
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
                                    EF.Book newBook = new EF.Book();
                                    newBook.NameBook = txtNameBook.Text;
                                    var Publishing = AppData.Context.Publishing.ToList().
                                                Where(i => i.NamePublishing == txtPublishing.Text).
                                                FirstOrDefault();
                                    newBook.IDPublishing = Publishing.ID;
                                    newBook.YearOfPublishing = Convert.ToInt32(txtYearOfPublishing.Text);
                                    var GenreBook = AppData.Context.Genre.ToList().
                                                Where(i => i.NameGenre == txtGenre.Text).
                                                FirstOrDefault();
                                    newBook.IDGenreBook = GenreBook.ID;
                                    var AuthorBook = AppData.Context.Author.ToList().
                                                Where(i => i.Nickname == txtAuthor.Text).
                                                FirstOrDefault();
                                    newBook.IDAuthorBook = AuthorBook.ID;
                                    newBook.NumberOfPages = Convert.ToInt32(txtNumberOfPages.Text);
                                    newBook.Cost = Convert.ToInt32(txtCost.Text);
                                    newBook.IsDeleted = false;


                                    if (pathPhoto != null)
                                    {
                                        newBook.Preview = File.ReadAllBytes(pathPhoto);
                                    }

                                    AppData.Context.Book.Add(newBook);

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

        private void btEnd_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}


