namespace AibolitVeterinaryClinicDatabaseImplement.Models
{
    public class VisitMedicine
    {
        public int Id { get; set; }
        public int VisitId { get; set; }
        public int MedicineId { get; set; }
        public virtual Visit Visit { get; set; }
        public virtual Medicine Medicine { get; set; }
    }
}
