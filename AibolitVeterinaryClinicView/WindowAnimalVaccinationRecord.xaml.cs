using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.BusinessLogicsContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Unity;

namespace AibolitVeterinaryClinicView
{
    /// <summary>
    /// Логика взаимодействия для WindowAnimalVaccinationRecord.xaml
    /// </summary>
    public partial class WindowAnimalVaccinationRecord : Window
    {
        public int? animalId { get; set; }
        public Dictionary<int, (string, DateTime)>? animalVaccinationRecord;
        private readonly IAnimalLogic _logic;
        public WindowAnimalVaccinationRecord(IAnimalLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }
        public void LoadData() 
        {
            try
            {
                if (animalVaccinationRecord != null)
                {
                    var list = new List<AnimalVaccinationRecordBindingModel>();
                    foreach (var pc in animalVaccinationRecord)
                        list.Add(new AnimalVaccinationRecordBindingModel { Id = pc.Key, Name = pc.Value.Item1, Date = pc.Value.Item2 });
                    DataGrid.ItemsSource = list;
                    DataGrid.Columns[0].Visibility = Visibility.Hidden;
                    DataGrid.Columns[1].Header = "Название прививки";
                    DataGrid.Columns[2].Header = "Дата прививания";
                    (DataGrid.Columns[2] as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<WindowVaccination>();
            form.animalId = (int)animalId;
            if (form.ShowDialog() == true)
            {
                if (animalVaccinationRecord.ContainsKey(form.Id)) animalVaccinationRecord[form.Id] = (form.Name, form.Date);
                else animalVaccinationRecord.Add(form.Id, (form.Name, form.Date));
                LoadData();
            }
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count == 1)
            {
                var form = App.Container.Resolve<WindowVaccination>();
                int id = Convert.ToInt32(((AnimalVaccinationRecordBindingModel)DataGrid.SelectedItems[0]).Id);
                form.Id = id;
                form.animalId = (int)animalId;
                form.Date = animalVaccinationRecord[id].Item2;
                form.VaccinationName.IsEnabled = false;
                if (form.ShowDialog() == true)
                {
                    animalVaccinationRecord[form.Id] = (form.Name, form.Date);
                    LoadData();
                }
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count == 1)
            {
                if (MessageBox.Show("Удалить прививку?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        animalVaccinationRecord.Remove(Convert.ToInt32(((AnimalVaccinationRecordBindingModel)DataGrid.SelectedItems[0]).Id));
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (animalId.HasValue)
            {
                try
                {
                    var view = _logic.Read(new AnimalBindingModel { Id = animalId })?[0];
                    if (view != null)
                    {
                        animalVaccinationRecord = view.AnimalVaccinationRecord;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else animalVaccinationRecord = new Dictionary<int, (string, DateTime)>();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _logic.CreateOrUpdate(new AnimalBindingModel
                {
                    Id = animalId,
                    ClientId = (int)(_logic.Read(new AnimalBindingModel { Id = animalId })?[0].ClientId),
                    AnimalName = _logic.Read(new AnimalBindingModel { Id = animalId })?[0].AnimalName,
                    AnimalBreed = _logic.Read(new AnimalBindingModel { Id = animalId })?[0].AnimalBreed,
                    AnimalVaccinationRecord = animalVaccinationRecord
                });
                MessageBox.Show("Сохранение прошло успешно.", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
