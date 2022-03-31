using System.ComponentModel;

namespace AibolitVeterinaryClinicContracts.ViewModels
{
    public class MedicineViewModel
    {
        public int Id { get; set; }
        [DisplayName ("Название лекарства")] public string MedicineName { get; set; }
    }
}
