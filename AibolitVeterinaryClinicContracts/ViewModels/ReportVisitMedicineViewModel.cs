using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AibolitVeterinaryClinicContracts.ViewModels
{
    public class ReportVisitMedicineViewModel
    {
        public DateTime VisitDate { get; set; }
        public List<string> Medicines { get; set; }
    }
}
