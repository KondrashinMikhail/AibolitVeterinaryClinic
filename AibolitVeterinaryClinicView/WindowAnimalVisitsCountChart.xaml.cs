using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.BusinessLogicsContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AibolitVeterinaryClinicView
{
    /// <summary>
    /// Логика взаимодействия для WindowAnimalVisitsCountChart.xaml
    /// </summary>
    public partial class WindowAnimalVisitsCountChart : Window
    {
        public int clientId { get; set; }
        private readonly IAnimalLogic _animalLogic;
        private readonly IVisitLogic _visitLogic;
        public WindowAnimalVisitsCountChart(IAnimalLogic animalLogic, IVisitLogic visitLogic)
        {
            InitializeComponent();
            _animalLogic = animalLogic;
            _visitLogic = visitLogic;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var visits = _visitLogic.Read(new VisitBindingModel {ClientId = clientId });
            var animals = _animalLogic.Read(new AnimalBindingModel { ClientId = clientId });
            List<Tuple<string, int>> ChartInfo = new();
            foreach (var animal in animals)
            {
                int count = 0;
                foreach (var visit in visits)
                {
                    if (visit.Animals.Contains(animal.Id))
                        count++;
                }
                ChartInfo.Add(Tuple.Create(animal.AnimalName, count));
            }
            ((ColumnSeries)AnimalsVisitsCount.Series[0]).ItemsSource = ChartInfo;

        }
    }
}
