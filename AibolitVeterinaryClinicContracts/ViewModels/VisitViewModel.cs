using System.ComponentModel;

namespace AibolitVeterinaryClinicContracts.ViewModels
{
    public class VisitViewModel
    {
        public int Id { get; set; }
        [DisplayName ("Дата визита")] public DateTime DateVisit { get; set; }
    }
}
