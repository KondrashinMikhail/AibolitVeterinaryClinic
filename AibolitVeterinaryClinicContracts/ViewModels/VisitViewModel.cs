using System.ComponentModel;

namespace AibolitVeterinaryClinicContracts.ViewModels
{
    public class VisitViewModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public List<string> ServiceNames { get; set; }
        public List<int> Services { get; set; }
        public List<int>? Animals { get; set; }
        public List<int>? Medicines { get; set; }
        public DateTime DateVisit { get; set; }
    }
}
