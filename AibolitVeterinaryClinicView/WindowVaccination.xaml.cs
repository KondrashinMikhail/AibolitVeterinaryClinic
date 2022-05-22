using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.BusinessLogicsContracts;
using System;
using System.Linq;
using System.Windows;

namespace AibolitVeterinaryClinicView
{
    /// <summary>
    /// Логика взаимодействия для WindowVaccination.xaml
    /// </summary>
    public partial class WindowVaccination : Window
    {
        public int animalId { get; set; }

        public int Id
        {
            get { return Convert.ToInt32(VaccinationName.SelectedValue); }
            set { VaccinationName.SelectedValue = value; }
        }
        public string Name { get { return VaccinationName.Text; } }
        public DateTime Date
        {
            get { return Convert.ToDateTime(VaccinationDate.Text); }
            set { VaccinationDate.Text = value.ToShortDateString(); }
        }

        private readonly IAnimalLogic _logicAnimal;
        public WindowVaccination(IVaccinationLogic logicVaccination, IAnimalLogic logicAnimal)
        {
            InitializeComponent();
            var list = logicVaccination.Read(null);
            if (list != null)
            {
                VaccinationName.DisplayMemberPath = "VaccinationName";
                VaccinationName.SelectedValuePath = "Id";
                VaccinationName.ItemsSource = list;
                VaccinationName.SelectedItem = null;
            }
            _logicAnimal = logicAnimal;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(VaccinationName.Text) || string.IsNullOrEmpty(VaccinationDate.Text))
            {
                MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DialogResult = true;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var view = _logicAnimal.Read(new AnimalBindingModel { Id = animalId })?[0];
            if (view != null) AnimalName.Text = view.AnimalName;
        }
    }
}
