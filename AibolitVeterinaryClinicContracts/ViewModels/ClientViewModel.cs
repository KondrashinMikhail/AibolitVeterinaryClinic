using System.ComponentModel;

namespace AibolitVeterinaryClinicContracts.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        [DisplayName ("Логин")] public string ClientLogin { get; set; }
        [DisplayName ("Имя клиента")] public string ClientName { get; set; }
        [DisplayName ("Телефонный номер клиента")] public string ClientPhoneNumber { get; set; }
        //public Dictionary<int, (string, int)> ClientAnimals;
        //public List<AnimalViewModel> ClientAnimals;
        public List<int> ClientAnimals { get; set; }
    }
}
