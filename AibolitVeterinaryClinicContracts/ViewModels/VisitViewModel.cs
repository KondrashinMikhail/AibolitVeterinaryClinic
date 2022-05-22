using System.ComponentModel;

namespace AibolitVeterinaryClinicContracts.ViewModels
{
    public class VisitViewModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string ServiceName { get; set; }
        public int ServiceId { get; set; }
        public List<int>? Animals { get; set; }
        public List<int>? Medicines { get; set; }
        public DateTime DateVisit { get; set; }
    }
}
