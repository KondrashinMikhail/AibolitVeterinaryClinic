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
    /// Логика взаимодействия для WindowVisits.xaml
    /// </summary>
    public partial class WindowVisits : Window
    {
        public int clientId { get; set; }
        private readonly IVisitLogic _visitLogic;
        public WindowVisits( IVisitLogic visitLogic)
        {
            InitializeComponent();
            _visitLogic = visitLogic;
        }
        private void LoadData() 
        {
            try
            {
                var list = _visitLogic.Read(new VisitBindingModel { ClientId = clientId });
                if (list != null)
                {
                    DataGrid.ItemsSource = list;
                    DataGrid.Columns[0].Visibility = Visibility.Hidden;
                    DataGrid.Columns[1].Visibility = Visibility.Hidden;
                    DataGrid.Columns[2].Visibility = Visibility.Hidden;
                    DataGrid.Columns[3].Header = "Имя врача";
                    DataGrid.Columns[4].Header = "Название услуги";
                    DataGrid.Columns[5].Visibility = Visibility.Hidden;
                    DataGrid.Columns[6].Visibility = Visibility.Hidden;
                    DataGrid.Columns[7].Visibility = Visibility.Hidden;
                    DataGrid.Columns[8].Header = "Дата визита";
                    (DataGrid.Columns[8] as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<WindowVisit>();
            form.clientId = clientId;
            if (form.ShowDialog() == true) LoadData();
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<WindowVisit>();
            form.clientId = clientId;
            form.visitId = ((VisitViewModel)DataGrid.SelectedItems[0]).Id;
            form.Service.IsEnabled = false;
            if (form.ShowDialog() == true) LoadData();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count == 1)
            {
                if (MessageBox.Show("Удалить визит?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _visitLogic.Delete(new VisitBindingModel { Id = ((VisitViewModel)DataGrid.SelectedItems[0]).Id });
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
        private void Window_Loaded(object sender, RoutedEventArgs e) => LoadData();
        private void BindVisitAnimal_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<WindowVisitAnimal>();
            form.clientId = clientId;
            form.ShowDialog();
        }
    }
}
