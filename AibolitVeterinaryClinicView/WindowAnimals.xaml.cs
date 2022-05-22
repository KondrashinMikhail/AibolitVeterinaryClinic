using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.BusinessLogicsContracts;
using AibolitVeterinaryClinicContracts.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using Unity;

namespace AibolitVeterinaryClinicView
{
    /// <summary>
    /// Логика взаимодействия для WindowAnimals.xaml
    /// </summary>
    public partial class WindowAnimals : Window
    {
        public int clientId { get; set; }
        private readonly IAnimalLogic _logicAnimals;
        public WindowAnimals(IAnimalLogic logicA)
        {
            InitializeComponent();
            _logicAnimals = logicA;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e) => LoadData();
        public void LoadData() 
        {
            try
            {
                var list = _logicAnimals.Read(new AnimalBindingModel { ClientId = clientId });
                if (list != null)
                {
                    DataGrid.ItemsSource = list;
                    DataGrid.Columns[0].Visibility = Visibility.Hidden;
                    DataGrid.Columns[1].Visibility = Visibility.Hidden;
                    DataGrid.Columns[2].Visibility = Visibility.Hidden;
                    DataGrid.Columns[3].Header = "Порода";
                    DataGrid.Columns[4].Header = "Кличка";
                    DataGrid.Columns[5].Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<WindowAnimal>();
            form.clientId = clientId;
            if (form.ShowDialog() == true) LoadData();
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count == 1) 
            {
                var form = App.Container.Resolve<WindowAnimal>();
                form.clientId = clientId;
                form.animalId = ((AnimalViewModel)DataGrid.SelectedItems[0]).Id;
                if (form.ShowDialog() == true) LoadData();
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count == 1)
            {
                if (MessageBox.Show("Удалить животное?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _logicAnimals.Delete(new AnimalBindingModel { Id = ((AnimalViewModel)DataGrid.SelectedItems[0]).Id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }
        private void Refresh_Click(object sender, RoutedEventArgs e) => LoadData();
        private void VaccinationRecord_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count == 1)
            {
                var form = App.Container.Resolve<WindowAnimalVaccinationRecord>();
                form.animalId = ((AnimalViewModel)DataGrid.SelectedItems[0]).Id; ;
                if (form.ShowDialog() == true) LoadData();
            }
        }
    }
}
