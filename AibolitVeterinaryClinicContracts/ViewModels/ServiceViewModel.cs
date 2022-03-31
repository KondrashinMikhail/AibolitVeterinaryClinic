using System.ComponentModel;

namespace AibolitVeterinaryClinicContracts.ViewModels
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        [DisplayName ("Название услуги")] public string ServiceName { get; set; }
    }
}
