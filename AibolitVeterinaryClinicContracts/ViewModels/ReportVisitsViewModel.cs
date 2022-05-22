using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AibolitVeterinaryClinicContracts.ViewModels
{
    public class ReportVisitsViewModel
    {
        public string ServiceName { get; set; }
        public DateTime DateVisit { get; set; }
        public List<string> Medicines { get; set; }
        public List<string> AnimalVaccinations { get; set; }
    }
}
