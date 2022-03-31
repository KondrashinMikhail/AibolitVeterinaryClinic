namespace AibolitVeterinaryClinicContracts.BindingModels
{
    public class AnimalBindingModel
    {
        public int Id { get; set; }
        public string AnimalBreed { get; set; }
        public string AnimalName { get; set; }
        public Dictionary<int, (string, int)> AnimalVaccinationRecord;
    }
}
