namespace AibolitVeterinaryClinicDatabaseImplement.Models
{
    public class VisitAnimal
    {
        public int Id { get; set; }
        public int? VisitId { get; set; }
        public int? AnimalId { get; set; }
        public virtual Visit? Visit { get; set; }
        public virtual Animal? Animal { get; set; }
    }
}
