using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AibolitVeterinaryClinicContracts.ViewModels
{
    public class ReportVisitsDataGridViewModel
    {
        public string ServiceName { get; set; }
        public string DateVisit { get; set; }
        public string Medicines { get; set; }
        public string AnimalVaccinations { get; set; }
    }
}
