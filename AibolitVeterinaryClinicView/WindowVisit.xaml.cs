using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.BusinessLogicsContracts;
using AibolitVeterinaryClinicDatabaseImplement;
using System;
using System.Collections.Generic;
using System.Windows;

namespace AibolitVeterinaryClinicView
{
    /// <summary>
    /// Логика взаимодействия для WindowVisit.xaml
    /// </summary>
    public partial class WindowVisit : Window
    {
        public int clientId { get; set; }
        public int visitId { get; set; }
        private readonly IServiceLogic _serviceLogic;
        private readonly IDoctorLogic _doctorLogic;
        private readonly IVisitLogic _visitLogic;
        public int ServiceId
        {
            get { return Convert.ToInt32(Service.SelectedValue); }
            set { Service.SelectedValue = value; }
        }
        public string ServiceName { get { return Service.Text; } }
        public DateTime Date
        {
            get { return Convert.ToDateTime(DateVisit.Text); }
            set { DateVisit.Text = value.ToShortDateString(); }
        }
        public WindowVisit(IServiceLogic serviceLogic, IDoctorLogic doctorLogic, IVisitLogic visitLogic)
        {
            InitializeComponent();
            var list = serviceLogic.Read(null);
            if (list != null)
            {
                Service.DisplayMemberPath = "ServiceName";
                Service.SelectedValuePath = "Id";
                Service.ItemsSource = list;
                Service.SelectedItem = null;
            }
            _serviceLogic = serviceLogic;
            _doctorLogic = doctorLogic;
            _visitLogic = visitLogic;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            var dict = _serviceLogic.Read(new ServiceBindingModel { ServiceName = ServiceName })?[0].ServiceMedicine;
            var medicines = new List<int>();
            foreach (var medicicne in _serviceLogic.Read(new ServiceBindingModel { ServiceName = ServiceName })?[0].ServiceMedicine)
                medicines.Add(medicicne.Key);
            _visitLogic.CreateOrUpdate(new VisitBindingModel
            {
                ClientId = clientId,
                DoctorId = (int)_serviceLogic.Read(new ServiceBindingModel { ServiceName = ServiceName })?[0].DoctorId,
                ServiceId = (int)_serviceLogic.Read(new ServiceBindingModel { ServiceName = ServiceName })?[0].Id,
                DateVisit = Date,
                Medicines = medicines
            });
            DialogResult = true;
            Close();
        }
        private void Service_DropDownClosed(object sender, EventArgs e) => DoctorName.Text = _doctorLogic.Read(new DoctorBindingModel { Id = _serviceLogic.Read(new ServiceBindingModel { Id = ServiceId })?[0].DoctorId })?[0].DoctorName;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var element = _visitLogic.Read(new VisitBindingModel { Id = visitId })?[0];
            if (element != null)
            {
                Service.Text = element.ServiceName;
                DoctorName.Text = element.DoctorName;
                Date = element.DateVisit;
            }
        }
    }
}
