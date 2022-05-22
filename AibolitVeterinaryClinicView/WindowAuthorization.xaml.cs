using AibolitVeterinaryClinicContracts.BusinessLogicsContracts;
using System;
using System.Linq;
using System.Windows;

namespace AibolitVeterinaryClinicView
{
    /// <summary>
    /// Логика взаимодействия для WindowAuthorization.xaml
    /// </summary>
    public partial class WindowAuthorization : Window
    {
        public int clientId { get; set; }
        private readonly IClientLogic _logic;
        public WindowAuthorization(IClientLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e) 
        {
            DialogResult = false;
            Close();
        }
        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            var list = _logic.Read(null);
            Boolean flag = false;
            try
            {
                foreach (var item in list) 
                    if (TextBoxLogin.Text == item.ClientLogin && TextBoxPhoneNumder.Text == item.ClientPhoneNumber)
                        flag = true;
                if (flag == true)
                {
                    clientId = list.FirstOrDefault(rec => rec.ClientLogin == TextBoxLogin.Text).Id;
                    DialogResult = true;
                    Close();
                }
                else MessageBox.Show("Пользователя с введенными данными нет в системе.\nВведите данные заново, или зарегистрируйтесь.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
