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

            cmbPublishing.ItemsSource = AppData.Context.Publishing.ToList();
            cmbPublishing.DisplayMemberPath = "NamePublishing";

            cmbPublishing.SelectedIndex = editBook.IDPublishing - 1;

            txtNameBook.Text = editBook.NameBook;
            txtYearOfPublishing.Text = Convert.ToString(editBook.YearOfPublishing);

            cmbGenre.ItemsSource = AppData.Context.Genre.ToList();
            cmbGenre.DisplayMemberPath = "NameGenre";

            cmbGenre.SelectedIndex = editBook.IDGenreBook - 1;

            cmbAuthor.ItemsSource = AppData.Context.Author.ToList();
            cmbAuthor.DisplayMemberPath = "Nickname";

            cmbAuthor.SelectedIndex = editBook.IDAuthorBook - 1;

            txtNumberOfPages.Text = Convert.ToString(editBook.NumberOfPages);
            txtCost.Text = Convert.ToString(editBook.Cost);

            isEdit = true;
        }

            public BookAddWindow()
            {
                InitializeComponent();

                cmbPublishing.ItemsSource = AppData.Context.Publishing.ToList();
                cmbPublishing.DisplayMemberPath = "NamePublishing";

                cmbGenre.ItemsSource = AppData.Context.Genre.ToList();
                cmbGenre.DisplayMemberPath = "NameGenre";

                cmbAuthor.ItemsSource = AppData.Context.Author.ToList();
                cmbAuthor.DisplayMemberPath = "Nickname";

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

            if (string.IsNullOrWhiteSpace(cmbPublishing.Text))
            {
                MessageBox.Show("Поле Издатель не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtYearOfPublishing.Text))
            {
                MessageBox.Show("Поле Год публикации не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(cmbGenre.Text))
            {
                MessageBox.Show("Поле Жанр не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbAuthor.Text))
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

            if (txtYearOfPublishing.Text.Length > 4)
            {
                MessageBox.Show("Недопустимое количество символов для поля Год публикации", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            //if (AppData.Context.Publishing.Where(i => i.NamePublishing == txtPublishing.Text).FirstOrDefault() == null)
            //{
            //    MessageBox.Show("Такого Издателя не существует в базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //Валидация Жанра
            //if (AppData.Context.Genre.Where(i => i.NameGenre == txtGenre.Text).FirstOrDefault() == null)
            //{
            //    MessageBox.Show("Такого Жанра не существует в базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

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

            if (Convert.ToInt32(NumberOfPages) < 0)
            {
                MessageBox.Show("Поле Кол-во страниц в книге не может быть отрицательным значением", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Convert.ToInt32(NumberOfPages) < 20)
            {
                MessageBox.Show("Книга не может иметь так мало страниц", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

            if (Convert.ToInt32(YearOfPublishing) > Convert.ToInt32(DateTime.Now.Year))
            {
                MessageBox.Show("Поле Год публикации не может быть больше текущего времени", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Валидация Долга
            if ((txtCost.Text.Length > 10) || (txtCost.Text.Length < 1))
                {
                    MessageBox.Show("Недопустимое количество символов для поля Цена книги", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (Convert.ToDouble(txtCost.Text) < 0.00)
                {
                    MessageBox.Show("Поле Цена книги не может быть ОТРИЦАТЕЛЬНЫМ числом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (isEdit)
                        {
                            try
                            {
                                //Изменение данных Книги
                                editBook.NameBook = txtNameBook.Text;

                                editBook.IDPublishing = cmbPublishing.SelectedIndex + 1;
                                editBook.YearOfPublishing = Convert.ToInt32(txtYearOfPublishing.Text);
                                editBook.IDGenreBook = cmbGenre.SelectedIndex + 1;
                                editBook.IDAuthorBook = cmbAuthor.SelectedIndex + 1;
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
                                    newBook.IDPublishing = cmbPublishing.SelectedIndex + 1;
                                    newBook.YearOfPublishing = Convert.ToInt32(txtYearOfPublishing.Text);
                                    newBook.IDGenreBook = cmbGenre.SelectedIndex + 1;
                                    newBook.IDAuthorBook = cmbAuthor.SelectedIndex + 1;
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


