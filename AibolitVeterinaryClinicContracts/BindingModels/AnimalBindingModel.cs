namespace AibolitVeterinaryClinicContracts.BindingModels
{
    public class AnimalBindingModel
    {
        public int? Id { get; set; }
        public int ClientId { get; set; }
        public string AnimalBreed { get; set; }
        public string AnimalName { get; set; }
        public Dictionary<int, string> AnimalVaccinationRecord { get; set; }
    }
}
