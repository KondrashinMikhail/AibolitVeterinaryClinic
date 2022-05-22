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

namespace AibolitVeterinaryClinicView
{
    /// <summary>
    /// Логика взаимодействия для WindowVisitAnimal.xaml
    /// </summary>
    public partial class WindowVisitAnimal : Window
    {
        public int clientId { get; set; }
        private readonly IVisitLogic _visitLogic;
        private readonly IAnimalLogic _animalLogic;
        public WindowVisitAnimal(IAnimalLogic animalLogic, IVisitLogic visitLogic)
        {
            InitializeComponent();
            _animalLogic = animalLogic;
            _visitLogic = visitLogic;
        }
        private void ButtonBind_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridVisits.SelectedItems.Count == 1) {
                int visitId = ((VisitViewModel) DataGridVisits.SelectedItems[0]).Id;
                var animalId = ((AnimalViewModel)DataGridAnimals.SelectedItems[0]).Id;
                List<int> animalsId = new();
                animalsId.Add(animalId);
                var list = _visitLogic.Read(null);
                var element = list.FirstOrDefault(rec => rec.Id == visitId);
                if (element != null)
                    _visitLogic.CreateOrUpdate(new VisitBindingModel 
                    {
                        Id = visitId,
                        DateVisit = element.DateVisit,
                        ClientId = clientId,
                        ServiceId = element.ServiceId,
                        DoctorId = element.DoctorId,
                        Medicines = element.Medicines,
                        Animals = animalsId
                    });
                MessageBox.Show("Визит и животное связаны.", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information); 
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var listAnimals = _animalLogic.Read(new AnimalBindingModel { ClientId = clientId });
            var listVisits = _visitLogic.Read(new VisitBindingModel { ClientId = clientId });
            if (listAnimals != null)
            {
                DataGridAnimals.ItemsSource = listAnimals;
                DataGridAnimals.Columns[0].Visibility = Visibility.Hidden;
                DataGridAnimals.Columns[1].Visibility = Visibility.Hidden;
                DataGridAnimals.Columns[2].Visibility = Visibility.Hidden;
                DataGridAnimals.Columns[3].Header = "Порода";
                DataGridAnimals.Columns[4].Header = "Кличка";
                DataGridAnimals.Columns[5].Visibility = Visibility.Hidden;
            }
            if (listVisits != null)
            {
                DataGridVisits.ItemsSource = listVisits;
                DataGridVisits.Columns[0].Visibility = Visibility.Hidden;
                DataGridVisits.Columns[1].Visibility = Visibility.Hidden;
                DataGridVisits.Columns[2].Visibility = Visibility.Hidden;
                DataGridVisits.Columns[3].Header = "Название услуги";
                DataGridVisits.Columns[4].Header = "Имя врача";
                DataGridVisits.Columns[5].Visibility = Visibility.Hidden;
                DataGridVisits.Columns[6].Visibility = Visibility.Hidden;
                DataGridVisits.Columns[7].Visibility = Visibility.Hidden;
                DataGridVisits.Columns[8].Header = "Дата визита";
                (DataGridVisits.Columns[8] as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy";
            }
        }
    }
}
