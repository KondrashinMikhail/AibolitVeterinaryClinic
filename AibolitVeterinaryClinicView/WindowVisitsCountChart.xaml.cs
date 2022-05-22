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
    /// Логика взаимодействия для WindowVisitsCountChart.xaml
    /// </summary>
    public partial class WindowVisitsCountChart : Window
    {
        public int clientId { get; set; }
        private readonly IVisitLogic _visitLogic;
        public WindowVisitsCountChart(IVisitLogic visitLogic)
        {
            InitializeComponent();
            _visitLogic = visitLogic;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var visits = _visitLogic.Read(new VisitBindingModel { ClientId = clientId });
            var ChartInfo = visits
                .OrderBy(rec => rec.DateVisit)
                .GroupBy(rec => (rec.DateVisit.Year, rec.DateVisit.Month))
                .Select(rec => new Tuple<string, int>(string.Format("{0}.{1}", rec.Key.Month, rec.Key.Year), rec.Count()))
                .ToList();
            ((PieSeries)VisitsDistribution.Series[0]).ItemsSource = ChartInfo;
        }
    }
}
