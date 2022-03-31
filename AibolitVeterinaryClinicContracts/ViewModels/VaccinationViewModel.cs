using System.ComponentModel;

namespace AibolitVeterinaryClinicContracts.ViewModels
{
    public class VaccinationViewModel
    {
        public int Id { get; set; }
        [DisplayName ("Название прививки")] public string VaccinationName { get; set; }
    }
}
