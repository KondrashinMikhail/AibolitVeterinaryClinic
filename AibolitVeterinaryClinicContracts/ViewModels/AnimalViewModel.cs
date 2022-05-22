using System.ComponentModel;

namespace AibolitVeterinaryClinicContracts.ViewModels
{
    public class AnimalViewModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string AnimalBreed { get; set; }
        public string AnimalName { get; set; }
        public Dictionary<int, (string, DateTime)> AnimalVaccinationRecord { get; set; }
    }
}
