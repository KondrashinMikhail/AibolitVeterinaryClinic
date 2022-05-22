using System.ComponentModel;

namespace AibolitVeterinaryClinicContracts.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        public string ClientLogin { get; set; }
        public string ClientMail { get; set; }
        public string ClientName { get; set; }
        public string ClientPhoneNumber { get; set; }
        public List<int> ClientAnimals { get; set; }
    }
}
