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

            cmbNameBook.ItemsSource = AppData.Context.Book.ToList();
            cmbNameBook.DisplayMemberPath = "NameBook";

            cmbClient.ItemsSource = AppData.Context.Client.ToList();
            cmbClient.DisplayMemberPath = "FIO";

            cmbEmployee.ItemsSource = AppData.Context.Employee.ToList();
            cmbEmployee.DisplayMemberPath = "FIO";

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

            cmbNameBook.ItemsSource = AppData.Context.Book.ToList();
            cmbNameBook.DisplayMemberPath = "NameBook";

            cmbClient.ItemsSource = AppData.Context.Client.ToList(); 
            cmbClient.DisplayMemberPath = "FIO";

            cmbEmployee.ItemsSource = AppData.Context.Employee.ToList();
            cmbEmployee.DisplayMemberPath = "FIO";

            txtDateExtradition.Text = Convert.ToString(editExtradition.DateExtradition);
            txtDateReturn.Text = Convert.ToString(editExtradition.DateReturn);
            cmbNameBook.SelectedIndex = editExtradition.IDBook - 1;
            cmbClient.SelectedIndex = editExtradition.IDClient - 1;
            txtPhoneClient.Text = editExtradition.Client.Phone;
            txtAddressClient.Text = editExtradition.Client.Address;
            cmbEmployee.SelectedIndex = editExtradition.IDEmployee - 1;
            txtCostDebt.Text = Convert.ToString(CalcDebt.Debt(Convert.ToDateTime(editExtradition.DateExtradition), Convert.ToDateTime(editExtradition.DateReturn),Convert.ToDouble(editExtradition.Book.Cost)));

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

            //if (string.IsNullOrWhiteSpace(txtDateReturn.Text))
            //{
            //    MessageBox.Show("Поле Дата возврата не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            if (string.IsNullOrWhiteSpace(cmbNameBook.Text))
            {
                MessageBox.Show("Поле Название книги не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbClient.Text))
            {
                MessageBox.Show("Поле Фамилия Клиента не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

            if (string.IsNullOrWhiteSpace(cmbEmployee.Text))
            {
                MessageBox.Show("Поле Фамилия Сотрудника не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            //Проверка на кол-во символов
            if (txtDateExtradition.Text.Length > 50)
            {
                MessageBox.Show("Недопустимое количество символов для поля Дата выдачи", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtDateReturn.Text.Length > 50)
            {
                MessageBox.Show("Недопустимое количество символов для поля Дата возврата", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

            //if (txtLastNameEmployee.Text.Length > 50)
            //{
            //    MessageBox.Show("Недопустимое количество символов для поля Фамилия Сотрудника", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //Валидация Книги
            //if (AppData.Context.Book.Where(i => i.NameBook == txtNameBook.Text).FirstOrDefault() == null)
            //{
            //    MessageBox.Show("Такой книги не существует в базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //Валидация Клиента
            //if (AppData.Context.Client.Where(i => i.LastName == txtLastNameClient.Text).FirstOrDefault() == null)
            //{
            //    MessageBox.Show("Такой Фамилии Клиента не существует в базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //if (AppData.Context.Client.Where(i => i.FirstName == txtFirstNameClient.Text).FirstOrDefault() == null)
            //{
            //    MessageBox.Show("Такого Имени Клиента не существует в базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //if (AppData.Context.Client.Where(i => i.Phone == txtPhoneClient.Text).FirstOrDefault() == null)
            //{
            //    MessageBox.Show("Такого Номера телефона Клиента не существует в базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //if (AppData.Context.Client.Where(i => i.Address == txtAddressClient.Text).FirstOrDefault() == null)
            //{
            //    MessageBox.Show("Такого Адресса Клиента не существует в базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //Валидация Сотрудника
            //if (AppData.Context.Employee.Where(i => i.LastName == txtLastNameEmployee.Text).FirstOrDefault() == null)
            //{
            //    MessageBox.Show("Такой Фамилии Сотрудника не существует в базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //Валидация Долга
            if (txtCostDebt.Text.Length > 8)
            {
                MessageBox.Show("Недопустимое количество символов для поля Долг", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //if (Convert.ToDouble(txtCostDebt.Text) < 0.00)
            //{
            //    MessageBox.Show("Недопустимое значение для поля Долг", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            string Cost = txtCostDebt.Text;

            if (Cost.Any(Char.IsUpper) || (Cost.Any(Char.IsLower) || (Cost.Any(Char.IsWhiteSpace))))
            {
                MessageBox.Show("Поле Цена долга может содержать только ПОЛОЖИТЕЛЬНЫЕ цифры");
                return;
            }

            //Валидация Дат
            //if (txtDateReturn.Text != null)
            //{
            //    if (txtDateReturn.Text.Length < txtDateExtradition.Text.Length)
            //    {
            //        MessageBox.Show("Дата возврата не может быть меньше даты выдачи", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //        return;
            //    }

            //    if (txtDateExtradition.Text.Length > txtDateReturn.Text.Length)
            //    {
            //        MessageBox.Show("Дата выдачи не может быть больше даты возврата", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //        return;
            //    }
            //    return;
            //}
            //else
            //{
            //    txtDateReturn.Text = Convert.ToString(DateTime.Now);
            //}
            ////Валидация телефона
            string Phone = txtPhoneClient.Text;
            if (Phone.Any(Char.IsUpper) || (Phone.Any(Char.IsLower) || (Phone.Any(Char.IsWhiteSpace))))
            {
                MessageBox.Show("Поле Телефон может содержать только цифры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (isEdit)
            {
                try
                {
                    //Изменение данных Заказа
                    editExtradition.DateExtradition = Convert.ToDateTime(txtDateExtradition.Text);
                    editExtradition.DateReturn = Convert.ToDateTime(txtDateReturn.Text);
                    editExtradition.IDBook = cmbNameBook.SelectedIndex + 1;
                    editExtradition.IDClient = cmbClient.SelectedIndex + 1;
                    editExtradition.Client.Phone = txtPhoneClient.Text;
                    editExtradition.Client.Address = txtAddressClient.Text;
                    editExtradition.IDEmployee = cmbEmployee.SelectedIndex + 1;
                    editExtradition.ClientDebt = Convert.ToDecimal(CalcDebt.Debt(Convert.ToDateTime(txtDateExtradition.Text), Convert.ToDateTime(txtDateReturn.Text), Convert.ToDouble(editExtradition.Book.Cost)));

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

                        newExtradition.IDBook = cmbNameBook.SelectedIndex + 1;

                        newExtradition.IDClient = cmbClient.SelectedIndex + 1;

                        newExtradition.IDEmployee = cmbEmployee.SelectedIndex + 1;

                        newExtradition.ClientDebt = null;

                        if (txtDateReturn.Text != null)
                        {
                            newExtradition.IsСompleted = true;
                        }
                        else
                        {
                            newExtradition.IsСompleted = false;
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
