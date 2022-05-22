using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.BusinessLogicsContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Unity;

namespace AibolitVeterinaryClinicView
{
    /// <summary>
    /// Логика взаимодействия для WindowAnimal.xaml
    /// </summary>
    public partial class WindowAnimal : Window
    {
        public int clientId { get; set; }
        public int? animalId { get; set; }
        private readonly IClientLogic _logicClient;
        private readonly IAnimalLogic _logicAnimal;
        public WindowAnimal(IClientLogic logicC, IAnimalLogic logicA)
        {
            InitializeComponent();
            _logicClient = logicC;
            _logicAnimal = logicA;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClientName.Text = _logicClient.Read(new ClientBindingModel { Id = clientId })?[0].ClientName.ToString();
            if (animalId.HasValue)
            {
                try
                {
                    var view = _logicAnimal.Read(new AnimalBindingModel { Id = animalId })?[0];
                    if (view != null)
                    {
                        AnimalBreed.Text = view.AnimalBreed;
                        AnimalName.Text = view.AnimalName;
                        ClientName.Text = view.ClientName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(AnimalBreed.Text) || string.IsNullOrEmpty(AnimalName.Text))
            {
                MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                _logicAnimal.CreateOrUpdate(new AnimalBindingModel
                {
                    Id = animalId,
                    ClientId = clientId,
                    AnimalBreed = AnimalBreed.Text,
                    AnimalName = AnimalName.Text,
                    //AnimalVaccinationRecord = _logicAnimal.Read(new AnimalBindingModel { Id = animalId })?[0].AnimalVaccinationRecord
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