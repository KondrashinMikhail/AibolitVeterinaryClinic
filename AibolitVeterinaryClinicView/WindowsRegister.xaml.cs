using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.BusinessLogicsContracts;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace AibolitVeterinaryClinicView
{
    /// <summary>
    /// Логика взаимодействия для WindowRegister.xaml
    /// </summary>
    public partial class WindowRegister : Window
    {
        public string clientLogin { get; set; }
        private readonly IClientLogic _logic;
        public WindowRegister(IClientLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            var list = _logic.Read(null);
            try
            {
                if (TextBoxPhoneNumder.Text.Length != 11)
                {
                    MessageBox.Show("Неверно введен номер телефона.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (TextBoxLogin.Text == null || TextBoxName.Text == null || TextBoxPhoneNumder.Text == null || TextBoxMail.Text == null)
                {
                    MessageBox.Show("Вы заполнили не все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                foreach (var item in list) 
                    if (TextBoxLogin.Text == item.ClientLogin)
                    {
                        MessageBox.Show("Данный логин занят.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                if (!Regex.IsMatch(TextBoxMail.Text, @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                            + "@"
                                            + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$"))
                    throw new Exception("Введенная почта имела неверный формат");
                _logic.CreateOrUpdate(new ClientBindingModel
                {
                    ClientLogin = TextBoxLogin.Text,
                    ClientMail = TextBoxMail.Text.ToString(),
                    ClientName = TextBoxName.Text,
                    ClientPhoneNumber = TextBoxPhoneNumder.Text
                });
                clientLogin = TextBoxLogin.Text;
                DialogResult = true;
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}