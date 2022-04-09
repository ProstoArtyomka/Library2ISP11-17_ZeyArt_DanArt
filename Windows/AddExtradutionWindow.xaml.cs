using Library2ISP11_17_ZeyArt_DanArt.ClassHelper;
using Microsoft.Win32;
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

namespace Library2ISP11_17_ZeyArt_DanArt.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddExtradutionWindow.xaml
    /// </summary>
    public partial class AddExtradutionWindow : Window
    {
        EF.Extradition editExtradition = new EF.Extradition();
        bool isEdit = true;
        string pathPhoto = null;

        public AddExtradutionWindow()
        {
            InitializeComponent();

            isEdit = false;
        }

        public AddExtradutionWindow(EF.Extradition extradition)
        {
            InitializeComponent();


            if (extradition.Photo != null)
            {
                using (MemoryStream stream = new MemoryStream(extradition.Photo))
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


            tbTitle.Text = "Изменения записи о выдаче книги";
            btAdd.Content = "Изменить запись";

            editExtradition = extradition;

            txtDateExtradition.Text = Convert.ToString(editExtradition.DateExtradition);
            txtDateReturn.Text = Convert.ToString(editExtradition.DateReturn);
            txtNameBook.Text = editExtradition.Book.NameBook;
            txtLastNameClient.Text = editExtradition.Client.LastName;
            txtFirstNameClient.Text = editExtradition.Client.FirstName;
            txtPhoneClient.Text = editExtradition.Client.Phone;
            txtAddressClient.Text = editExtradition.Client.Address;
            txtLastNameEmployee.Text = editExtradition.Employee.LastName;
            txtCostDebt.Text = Convert.ToString(editExtradition.ClientDebt);

            isEdit = true;

        }


        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            //проверка на пустоту
            if (string.IsNullOrWhiteSpace(txtDateExtradition.Text))
            {
                MessageBox.Show("Поле Дата выдачи не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNameBook.Text))
            {
                MessageBox.Show("Поле Название книги не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLastNameClient.Text))
            {
                MessageBox.Show("Поле Фамилия Клиента не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFirstNameClient.Text))
            {
                MessageBox.Show("Поле Имя Клиента не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhoneClient.Text))
            {
                MessageBox.Show("Поле Телефон Клиента не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAddressClient.Text))
            {
                MessageBox.Show("Поле Адрес Клиента не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLastNameEmployee.Text))
            {
                MessageBox.Show("Поле Фамилия Сотрудника не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            //проверка на кол-во символов

            if (txtDateExtradition.Text.Length > 200)
            {
                MessageBox.Show("Недопустимое количество символов для поля Дата выдачи", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtDateReturn.Text.Length > 200)
            {
                MessageBox.Show("Недопустимое количество символов для поля Дата возврата", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtDateReturn.Text.Length < txtDateExtradition.Text.Length)
            {
                MessageBox.Show("Дата возврата не может быть меньше даты выдачи", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtDateExtradition.Text.Length > txtDateReturn.Text.Length)
            {
                MessageBox.Show("Дата выдачи не может быть больше даты возврата", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtNameBook.Text.Length > 100)
            {
                MessageBox.Show("Недопустимое количество символов для поля Название книги", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtLastNameClient.Text.Length > 50)
            {
                MessageBox.Show("Недопустимое количество символов для поля Фамилия Клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtFirstNameClient.Text.Length > 50)
            {
                MessageBox.Show("Недопустимое количество символов для поля Имя Клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtPhoneClient.Text.Length > 14)
            {
                MessageBox.Show("Недопустимое количество символов для поля Телефон Клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtAddressClient.Text.Length > 100)
            {
                MessageBox.Show("Недопустимое количество символов для поля Адрес Клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtLastNameEmployee.Text.Length > 50)
            {
                MessageBox.Show("Недопустимое количество символов для поля Фамилия Сотрудника", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtCostDebt.Text.Length > 8)
            {
                MessageBox.Show("Недопустимое количество символов для поля Долг", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Convert.ToDouble(txtCostDebt.Text) < 0.00)
            {
                MessageBox.Show("Недопустимое значение для поля Долг", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (isEdit)
            {
                try
                {
                    //Изменение данных Заказа
                    editExtradition.DateExtradition = Convert.ToDateTime(txtDateExtradition.Text);
                    editExtradition.DateReturn = Convert.ToDateTime(txtDateReturn.Text);
                    editExtradition.Book.NameBook = txtNameBook.Text;
                    editExtradition.Client.LastName = txtLastNameClient.Text;
                    editExtradition.Client.FirstName = txtFirstNameClient.Text;
                    editExtradition.Client.Phone = txtPhoneClient.Text;
                    editExtradition.Client.Address = txtAddressClient.Text;
                    editExtradition.Employee.LastName = txtLastNameEmployee.Text;
                    editExtradition.ClientDebt = Convert.ToDecimal(txtCostDebt.Text);

                    if (txtDateReturn.Text != null)
                    {
                        editExtradition.IsСompleted = true;
                    }

                    if (pathPhoto != null)
                    {
                        editExtradition.Photo = File.ReadAllBytes(pathPhoto);
                    }

                    AppData.Context.SaveChanges();
                    MessageBox.Show("Успех", "Данные записи о выдаче заказа изменены", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите добавление записи о выдаче заказа", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultClick == MessageBoxResult.Yes)
                    {
                        //Добавление Заказа
                        EF.Extradition newExtradition = new EF.Extradition();
                        newExtradition.DateExtradition = Convert.ToDateTime(txtDateExtradition.Text);
                        newExtradition.DateReturn = Convert.ToDateTime(txtDateReturn.Text);
                        newExtradition.Book.NameBook = txtNameBook.Text;
                        newExtradition.Client.LastName = txtLastNameClient.Text;
                        newExtradition.Client.FirstName = txtFirstNameClient.Text;
                        newExtradition.Client.Phone = txtPhoneClient.Text;
                        newExtradition.Client.Address = txtAddressClient.Text;
                        newExtradition.Employee.LastName = txtLastNameEmployee.Text;
                        newExtradition.ClientDebt = Convert.ToDecimal(txtCostDebt.Text);

                        if (txtDateReturn.Text != null)
                        {
                            editExtradition.IsСompleted = true;
                        }

                        if (pathPhoto != null)
                        {
                            newExtradition.Photo = File.ReadAllBytes(pathPhoto);
                        }

                        AppData.Context.Extradition.Add(newExtradition);
                        AppData.Context.SaveChanges();

                        MessageBox.Show("Успех", "Запись о выдаче заказа успешно добавлена", MessageBoxButton.OK, MessageBoxImage.Information);
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
