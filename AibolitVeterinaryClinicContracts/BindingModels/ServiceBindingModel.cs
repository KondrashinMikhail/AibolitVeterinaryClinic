namespace AibolitVeterinaryClinicContracts.BindingModels
{
    public class ServiceBindingModel
    {
        public int? Id { get; set; }
        public int? DoctorId { get; set; }
        public string ServiceName { get; set; }
        public Dictionary<int, string> ServiceMedicine { get; set; }
    }
}
