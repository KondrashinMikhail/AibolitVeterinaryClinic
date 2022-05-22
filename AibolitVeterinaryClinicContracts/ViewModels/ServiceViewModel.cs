using System.ComponentModel;

namespace AibolitVeterinaryClinicContracts.ViewModels
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        public int? DoctorId { get; set; }
        public string ServiceName { get; set; }
        public Dictionary<int, string>? ServiceMedicine { get; set; }
    }
}
