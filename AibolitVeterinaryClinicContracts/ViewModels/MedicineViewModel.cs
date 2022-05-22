using System.ComponentModel;

namespace AibolitVeterinaryClinicContracts.ViewModels
{
    public class MedicineViewModel
    {
        public int? Id { get; set; }
        public int? DoctorId { get; set; }
        public string MedicineName { get; set; }
    }
}
