namespace AibolitVeterinaryClinicDatabaseImplement.Models
{
    public class VisitService
    {
        public int Id { get; set; }
        public int? ServiceId { get; set; }
        public int? VisitId { get; set; }
        public virtual Service? Service { get; set; }
        public virtual Visit? Visit { get; set; }
    }
}
