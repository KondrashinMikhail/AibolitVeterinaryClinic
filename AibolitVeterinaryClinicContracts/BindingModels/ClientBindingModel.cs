namespace AibolitVeterinaryClinicContracts.BindingModels
{
    public class ClientBindingModel
    {
        public int Id { get; set; }
        public string ClientLogin { get; set; }
        public string ClientName { get; set; }
        public string ClientPhoneNumber { get; set; }
        public Dictionary<int, (string, int)> ClientAnimals;
    }
}
