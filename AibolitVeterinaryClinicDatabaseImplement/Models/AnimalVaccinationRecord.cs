using System.ComponentModel.DataAnnotations;

namespace AibolitVeterinaryClinicDatabaseImplement.Models
{
    public class AnimalVaccinationRecord
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public int VaccinationId { get; set; }
        public DateTime Date { get; set; }
        public virtual Animal Animal { get; set; }
        public virtual Vaccination Vaccination { get; set; }
    }
}
