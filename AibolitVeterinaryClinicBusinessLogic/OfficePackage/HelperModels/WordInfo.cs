using AibolitVeterinaryClinicContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AibolitVeterinaryClinicBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<VisitViewModel> Visits { get; set; }
        public List<MedicineViewModel> Medicines { get; set; }
    }
}
