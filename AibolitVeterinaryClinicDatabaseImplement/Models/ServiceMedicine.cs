namespace AibolitVeterinaryClinicDatabaseImplement.Models
{
    public class ServiceMedicine
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int MedicineId { get; set; }
        public virtual Service Service { get; set; }
        public virtual Medicine Medicine { get; set; }
    }
}
