using System.ComponentModel;

namespace AibolitVeterinaryClinicContracts.ViewModels
{
    public class AnimalViewModel
    {
        public int Id { get; set; }
        [DisplayName ("Порода животного")] public string AnimalBreed { get; set; }
        [DisplayName ("Кличка животного")]public string AnimalName { get; set; }
        public Dictionary<int, (string, int)> AnimalVaccinationRecord;
    }
}
