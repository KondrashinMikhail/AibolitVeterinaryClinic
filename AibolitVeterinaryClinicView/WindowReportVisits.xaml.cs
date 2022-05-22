using AibolitVeterinaryClinicBusinessLogic.BusinessLogics;
using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.BusinessLogicsContracts;
using AibolitVeterinaryClinicContracts.ViewModels;
using Microsoft.Win32;
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

namespace AibolitVeterinaryClinicView
{
    /// <summary>
    /// Логика взаимодействия для WindowReportVisits.xaml
    /// </summary>
    public partial class WindowReportVisits : Window
    {
        public int clientId { get; set; }
        private IReportLogic _logicR;
        private readonly IClientLogic _clientLogic;
        private readonly MailLogic _logicM;
        public WindowReportVisits(IReportLogic logicR, MailLogic logicM , IClientLogic clientLogic)
        {
            InitializeComponent();
            _logicR = logicR;
            _logicM = logicM;
            _clientLogic = clientLogic;
        }
        private void ButtonFormReport_Click(object sender, RoutedEventArgs e)
        {
            if (DatePikerTo.SelectedDate == null || DatePikerFrom.SelectedDate == null)
            {
                MessageBox.Show("Выберите даты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (DatePikerFrom.SelectedDate >= DatePikerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                List<ReportVisitsViewModel> visits = _logicR.GetVisitsReportInfo(new ReportBindingModel
                {
                    DateFrom = DatePikerFrom.SelectedDate,
                    DateTo = DatePikerTo.SelectedDate,
                    ClientId = clientId
                }, clientId);
                var itemSourceList = new List<ReportVisitsDataGridViewModel>();
                string medicines = "";
                string animalVaccinations = "";
                foreach (var visit in visits)
                {
                    foreach (var medicine in visit.Medicines)
                        medicines += (medicine + "; ");
                    foreach (var animalVaccination in visit.AnimalVaccinations)
                        animalVaccinations += (animalVaccination + "; ");
                    itemSourceList.Add(new ReportVisitsDataGridViewModel
                    {
                        ServiceName = visit.ServiceName,
                        DateVisit = visit.DateVisit.ToShortDateString(),
                        Medicines = medicines,
                        AnimalVaccinations = animalVaccinations
                    });
                }
                DataGridVisits.ItemsSource = itemSourceList;
                DataGridVisits.Columns[0].Header = "Услуга";
                DataGridVisits.Columns[1].Header = "Дата";
                DataGridVisits.Columns[2].Header = "Медикаменты";
                DataGridVisits.Columns[3].Header = "Прививки";
                textBoxDateFrom.Content = DatePikerFrom.SelectedDate.Value.ToLongDateString();
                textBoxDateTo.Content = DatePikerTo.SelectedDate.Value.ToLongDateString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ButtonFormReportPdf_Click(object sender, RoutedEventArgs e)
        {
            if (DatePikerTo.SelectedDate == null || DatePikerFrom.SelectedDate == null)
            {
                MessageBox.Show("Выберите даты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (DatePikerFrom.SelectedDate >= DatePikerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" };
            {
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        _logicR.SaveVisitsToPdfFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName,
                            DateFrom = DatePikerFrom.SelectedDate,
                            DateTo = DatePikerTo.SelectedDate,
                            ClientId = clientId
                        }, clientId);
                        MessageBox.Show("Отчёт сформирован", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        private void ButtonSendReportPdfToEmail_Click(object sender, RoutedEventArgs e)
        {
            if (DatePikerTo.SelectedDate == null || DatePikerFrom.SelectedDate == null)
            {
                MessageBox.Show("Выберите даты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (DatePikerFrom.SelectedDate >= DatePikerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try 
            {
                _logicR.SaveVisitsToPdfFile(new ReportBindingModel
                {
                    FileName = "D:/Университет/2 курс/Четвертый семестр/ТП/Курсовая/Отчеты/Отчет mail.pdf",
                    DateFrom = DatePikerFrom.SelectedDate,
                    DateTo = DatePikerTo.SelectedDate,
                    ClientId = clientId
                }, clientId);
                _logicM.MailSendAsync(new MailSendInfoBindingModel
                {
                    MailAddress = _clientLogic.Read(new ClientBindingModel { Id = clientId })?[0].ClientMail,
                    Subject = "Отчет по визитам",
                    Text = "Сведения по визитам с " + DatePikerFrom.SelectedDate.Value.ToShortDateString() + " по " + DatePikerTo.SelectedDate.Value.ToShortDateString(),
                    FileName = "D:/Университет/2 курс/Четвертый семестр/ТП/Курсовая/Отчеты/Отчет mail.pdf"
                });
                MessageBox.Show("Отчёт отправлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
