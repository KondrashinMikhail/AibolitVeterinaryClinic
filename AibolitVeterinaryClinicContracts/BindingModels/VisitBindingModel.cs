namespace AibolitVeterinaryClinicContracts.BindingModels
{
    public class VisitBindingModel
    {
        public int? Id { get; set; }
        public int ClientId { get; set; }
        public int ServiceId { get; set; }
        public int DoctorId { get; set; }
        public List<int>? Animals { get; set; }
        public List<int> Medicines { get; set; }
        public DateTime DateVisit { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
