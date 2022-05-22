using AibolitVeterinaryClinicBusinessLogic;
using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.BusinessLogicsContracts;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using Unity;

namespace AibolitVeterinaryClinicView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int clientId { get; set; }
        private readonly IClientLogic _clientLogic;
        private readonly IVaccinationLogic _vaccinationLogic;
        private readonly IMedicineLogic _medicineLogic;
        private readonly IServiceLogic _serviceLogic;
        private readonly IDoctorLogic _doctorLogic;
        private readonly WorkModeling _workModeling;

        private readonly IReportLogic _reportLogic;
        public MainWindow(IClientLogic clientLogic, IVaccinationLogic vaccinationLogic, IMedicineLogic medicineLogic, IServiceLogic serviceLogic, IDoctorLogic doctorLogic, WorkModeling workModeling, IReportLogic reportLogic)
        {
            InitializeComponent();
            _clientLogic = clientLogic;
            _vaccinationLogic = vaccinationLogic;
            _medicineLogic = medicineLogic;
            _serviceLogic = serviceLogic;
            _doctorLogic = doctorLogic;
            _workModeling = workModeling;
            _reportLogic = reportLogic;
        }
        private void LoadData() 
        {
            try
            {
                var listVaccinations = _vaccinationLogic.Read(null);
                var listMedicines = _medicineLogic.Read(null);
                var listServices = _serviceLogic.Read(null);
                var listDoctors = _doctorLogic.Read(null);
                if (listVaccinations != null && listMedicines != null)
                {
                    DataGridVaccinations.ItemsSource = listVaccinations;
                    DataGridVaccinations.Columns[0].Visibility = Visibility.Hidden;
                    DataGridVaccinations.Columns[1].Header = "Название прививки";

                }
                if (listMedicines != null)
                {
                    DataGridMedicines.ItemsSource = listMedicines;
                    DataGridMedicines.Columns[0].Visibility = Visibility.Hidden;
                    DataGridMedicines.Columns[1].Visibility = Visibility.Hidden;
                    DataGridMedicines.Columns[2].Header = "Название препарата";
                }
                if (listServices != null)
                {
                    DataGridServices.ItemsSource = listServices;
                    DataGridServices.Columns[0].Visibility = Visibility.Hidden;
                    DataGridServices.Columns[1].Visibility = Visibility.Hidden;
                    DataGridServices.Columns[2].Header = "Название услуги";
                    DataGridServices.Columns[3].Visibility = Visibility.Hidden;
                }
                if (listDoctors != null)
                {
                    DataGridDoctors.ItemsSource = listDoctors;
                    DataGridDoctors.Columns[0].Visibility = Visibility.Hidden;
                    DataGridDoctors.Columns[1].Header = "Имя врача";
                    DataGridDoctors.Columns[2].Header = "Спецификация";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
            _workModeling.CreateMedicines();
            _workModeling.CreateDoctors();
            _workModeling.CreateVaccinations();
            _workModeling.CreateServices();
            _workModeling.CreateVaccinations();
            _workModeling.MedicinesServiceBinding();
            _workModeling.DoctorsBinding();
        }
        private void Update_Click(object sender, RoutedEventArgs e) => LoadData();
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<WindowAuthorization>();
            form.ShowDialog();
            if (form.DialogResult == true)
            {
                var list = _clientLogic.Read(null);
                clientId = form.clientId;
                Enter.Header = "Войти в другой аккаунт";
                Register.IsEnabled = false;
                Authorization.Header = "Вы зашли как: " + list.FirstOrDefault(rec => rec.Id == clientId).ClientLogin;
            }
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<WindowRegister>();
            form.ShowDialog();
            if (form.DialogResult == true) 
            {
                var list = _clientLogic.Read(null);
                clientId = (int)_clientLogic.Read(new ClientBindingModel { ClientLogin = form.clientLogin })?[0].Id;
                Enter.Header = "Войти в другой аккаунт";
                Register.IsEnabled = false;
                Authorization.Header = "Вы зашли как: " + list.FirstOrDefault(rec => rec.Id == clientId).ClientLogin;
                MessageBox.Show("Регистрация завершена.", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }
        private void Visits_Click(object sender, RoutedEventArgs e)
        {
            if (clientId != 0)
            {
                var form = App.Container.Resolve<WindowVisits>();
                form.clientId = clientId;
                form.ShowDialog();
            }
            else MessageBox.Show("Вы не авторизованы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void Animals_Click(object sender, RoutedEventArgs e)
        {
            if (clientId != 0) 
            {
                var form = App.Container.Resolve<WindowAnimals>();
                form.clientId = clientId;
                form.ShowDialog();
            }
            else MessageBox.Show("Вы не авторизованы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void docxFile_Click(object sender, RoutedEventArgs e)
        {
            if (clientId != 0)
            {
                var dialog = new SaveFileDialog { Filter = "docx|*.docx" };
                if (dialog.ShowDialog() == true) _reportLogic.SaveVisitsToWordFile(new ReportBindingModel { FileName = dialog.FileName }, clientId);
            }
            else MessageBox.Show("Вы не авторизованы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void xlsFile_Click(object sender, RoutedEventArgs e)
        {
            if (clientId != 0)
            {
                var dialog = new SaveFileDialog { Filter = "xlsx | *.xlsx" };
                if (dialog.ShowDialog() == true) _reportLogic.SaveVisitsToExcelFile(new ReportBindingModel { FileName = dialog.FileName }, clientId);
            }
            else MessageBox.Show("Вы не авторизованы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void ReportVisits_Click(object sender, RoutedEventArgs e)
        {
            if (clientId != 0)
            {
                var form = App.Container.Resolve<WindowReportVisits>();
                form.clientId = clientId;
                form.ShowDialog();
            }
            else MessageBox.Show("Вы не авторизованы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void Window_Closed(object sender, System.EventArgs e) => _workModeling.Delete();
        private void VisitsCount_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<WindowVisitsCountChart>();
            form.clientId = clientId;
            form.ShowDialog();
        }
        private void AnimalVisitsCount_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<WindowAnimalVisitsCountChart>();
            form.clientId = clientId;
            form.ShowDialog();
        }
    }
}
