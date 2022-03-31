using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AibolitVeterinaryClinicDatabaseImplement.Models
{
    public class Vaccination
    {
        public int Id { get; set; }
        [Required] public string VaccinationName { get; set; }
        [ForeignKey ("VaccinationId")] public virtual List<AnimalVaccinationRecord> AnimalVaccinationRecords { get; set; }
    }
}
