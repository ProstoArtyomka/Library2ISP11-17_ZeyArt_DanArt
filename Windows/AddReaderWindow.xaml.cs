
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
    /// Логика взаимодействия для AddReaderWindow.xaml
    /// </summary>
    public partial class AddReaderWindow : Window
    {
        EF.Client editReader = new EF.Client();
        bool isEdit = true;
        string pathPhoto = null;

        public AddReaderWindow()
        {
            InitializeComponent();
            cmbGender.ItemsSource = AppData.Context.Gender.ToList();
            cmbGender.DisplayMemberPath = "NameGender";
            cmbGender.SelectedItem = 0;

            isEdit = false;
        }

        public AddReaderWindow(EF.Client reader)
        {
            InitializeComponent();


            if (reader.Photo != null)
            {
                using (MemoryStream stream = new MemoryStream(reader.Photo))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    imgUser.Source = bitmapImage;
                }
            }

            cmbGender.ItemsSource = AppData.Context.Gender.ToList();
            cmbGender.DisplayMemberPath = "NameGender";

            tbTitle.Text = "Изменения данных читателя";
            btAdd.Content = "Изменить читателя";

            editReader = reader;

            txtLastName.Text = editReader.LastName;
            txtFirstName.Text = editReader.FirstName;
            txtPatronymic.Text = editReader.Patronymic;
            txtPhone.Text = editReader.Phone;
            txtEmail.Text = editReader.Email;
            txtRating.Text = Convert.ToString(editReader.Rating);
            cmbGender.SelectedIndex = editReader.IDGender - 1;
            txtAddress.Text = editReader.Address;

            isEdit = true;
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            //проверка на пустоту
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Поле Фамилия не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Поле Имя не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPatronymic.Text))
            {
                MessageBox.Show("Поле Отчество не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Поле Телефон не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Поле Почта не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtRating.Text))
            {
                MessageBox.Show("Поле Рейтинг не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Поле Адрес не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //проверка на кол-во символов
            if ((txtLastName.Text.Length > 50) || (txtLastName.Text.Length < 1))
            {
                MessageBox.Show("Недопустимое количество символов для поля Фамилия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if ((txtFirstName.Text.Length > 50) || (txtFirstName.Text.Length < 1))
            {
                MessageBox.Show("Недопустимое количество символов для поля Имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if ((txtPatronymic.Text.Length > 50) || (txtPatronymic.Text.Length < 1))
            {
                MessageBox.Show("Недопустимое количество символов для поля Отчество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtPhone.Text.Length > 20)
            {
                MessageBox.Show("Недопустимое количество символов для поля Телефон", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if ((txtEmail.Text.Length > 50) || (txtEmail.Text.Length < 2))
            {
                MessageBox.Show("Недопустимое количество символов для поля Почта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtRating.Text.Length > 3)
            {
                MessageBox.Show("Недопустимое количество символов для поля Рейтинг", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txtAddress.Text.Length > 100)
            {
                MessageBox.Show("Недопустимое количество символов для поля Адрес", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Валидация значения рейтинга
            if ((Convert.ToDouble(txtRating.Text) > 9.9) || (Convert.ToDouble(txtRating.Text) < 0.0))
            {
                MessageBox.Show("Рейтинг не может быть больше 9,9 или меньше 0,0", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string Rating = txtRating.Text;
            if (Rating.Any(Char.IsUpper) || (Rating.Any(Char.IsLower) || (Rating.Any(Char.IsWhiteSpace))))
            {
                MessageBox.Show("Поле Рейтинг может содержать только ПОЛОЖИТЕЛЬНЫЕ цифры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Валидация номера телефона
            string Phone = txtPhone.Text;
            if (Phone.Any(Char.IsUpper) || (Phone.Any(Char.IsLower)  || (Phone.Any(Char.IsWhiteSpace))))
            {
                MessageBox.Show("Поле Телефон может содержать только цифры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Валидация почты
            string Email = txtEmail.Text;
            if (Email.Contains("@@"))
            {
                MessageBox.Show("Поле Почта должна содержать только одну собаку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Email.Contains("@"))
            {
                MessageBox.Show("Поле почта не содержит собаку!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (isEdit)
            {
               try
                {
                    //Изменение данных Клиента
                    editReader.LastName = txtLastName.Text;
                    editReader.FirstName = txtFirstName.Text;
                    editReader.Patronymic = txtPatronymic.Text;
                    editReader.Phone = txtPhone.Text;
                    editReader.Email = txtEmail.Text;
                    editReader.Rating = Convert.ToDecimal(txtRating.Text);
                    editReader.IDGender = cmbGender.SelectedIndex + 1;
                    editReader.Address = txtAddress.Text;

                    if (pathPhoto != null)
                    {
                        editReader.Photo = File.ReadAllBytes(pathPhoto);
                    }

                    AppData.Context.SaveChanges();
                    MessageBox.Show("Успех", "Данные Клиента изменены", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите добавление читателя", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultClick == MessageBoxResult.Yes)
                    {
                        //Добавление нового читателя
                        EF.Client newReader = new EF.Client();
                        newReader.LastName = txtLastName.Text;
                        newReader.FirstName = txtFirstName.Text;
                        newReader.Patronymic = txtPatronymic.Text;
                        newReader.Phone = txtPhone.Text;
                        newReader.Email = txtEmail.Text;
                        newReader.Rating = Convert.ToDecimal(txtRating.Text);
                        newReader.IDGender = cmbGender.SelectedIndex + 1;
                        newReader.Address = txtAddress.Text;
                        newReader.IsDeleted = false;

                        if (pathPhoto != null)
                        {
                            newReader.Photo = File.ReadAllBytes(pathPhoto);
                        }

                        AppData.Context.Client.Add(newReader);
                        AppData.Context.SaveChanges();

                        MessageBox.Show("Успех", "Пользователь успешно добавлен", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
            }
                catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


        }

        private void cmbIsDeleted_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnChoosePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                imgUser.Source = new BitmapImage(new Uri(openFileDialog.FileName));

               pathPhoto = openFileDialog.FileName;
            }

        }

        private void btEnd_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
