using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.BusinessLogicsContracts;
using AibolitVeterinaryClinicContracts.ViewModels;
using System;
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
using Unity;

namespace AibolitVeterinaryClinicView
{
    /// <summary>
    /// Логика взаимодействия для WindowVisit.xaml
    /// </summary>
    public partial class WindowVisit : Window
    {
        public int visitId { get; set; }
        public int clientId { get; set; }
        private readonly IVisitLogic _visitLogic;
        private readonly IServiceLogic _serviceLogic;
        private List<string> listNames = new();
        private List<int> listIds = new ();
        public WindowVisit(IVisitLogic visitLogic, IServiceLogic serviceLogic)
        {
            InitializeComponent();
            _visitLogic = visitLogic;
            _serviceLogic = serviceLogic;
        }
        private void LoadData() 
        {
            var list = _serviceLogic.Read(null);
            DataGridAvailable.ItemsSource = list;
            DataGridAvailable.Columns[0].Visibility = Visibility.Hidden;
            DataGridAvailable.Columns[1].Visibility = Visibility.Hidden;
            DataGridAvailable.Columns[2].Header = "Услуга";
            DataGridAvailable.Columns[3].Visibility = Visibility.Hidden;

            ListSelected.ItemsSource = listNames;
            ListSelected.Items.Refresh();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
            if (visitId != 0) DateVisit.Text = _visitLogic.Read(new VisitBindingModel { Id = visitId })?[0].DateVisit.ToShortDateString();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridAvailable.SelectedItems.Count == 1)
            {
                listIds.Add(((ServiceViewModel)DataGridAvailable.SelectedItems[0]).Id);
                listNames.Add(((ServiceViewModel)DataGridAvailable.SelectedItems[0]).ServiceName);
            }
            LoadData();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (listIds.Count == 0 || listNames.Count == 0) 
            {
                MessageBox.Show("Не выбрано ни одной услуги.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(DateVisit.Text))
            {
                MessageBox.Show("Введите дату.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var medicines = new List<int>();
            foreach (var serviceName in listNames)
                foreach (var medicicne in _serviceLogic.Read(new ServiceBindingModel { ServiceName = serviceName })?[0].ServiceMedicine)
                    medicines.Add(medicicne.Key);
            _visitLogic.CreateOrUpdate(new VisitBindingModel
            {
                ClientId = clientId,
                Services = listIds,
                DateVisit = Convert.ToDateTime(DateVisit.Text),
                Medicines = medicines
            });
            DialogResult = true;
            Close();
        }
    }
}
