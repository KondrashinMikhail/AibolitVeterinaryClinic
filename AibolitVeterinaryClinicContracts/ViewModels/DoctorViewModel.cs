using System.ComponentModel;

namespace AibolitVeterinaryClinicContracts.ViewModels
{
    public class DoctorViewModel
    {
        public int Id { get; set; }
        [DisplayName ("Имя врача")] public string DoctorName { get; set; }
        [DisplayName ("Спецификация врача")] public string DoctorSpecification { get; set; }
    }
}
