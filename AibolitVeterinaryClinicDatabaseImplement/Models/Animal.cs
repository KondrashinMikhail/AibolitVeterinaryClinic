using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AibolitVeterinaryClinicDatabaseImplement.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        [Required] public string AnimalBreed { get; set; }
        [Required] public string AnimalName { get; set; }
        [ForeignKey ("AnimalId")] public virtual List<AnimalVaccinationRecord>? AnimalVaccinationRecords { get; set; }
        [ForeignKey ("AnimalId")] public virtual List<VisitAnimal> VisitAnimals { get; set; }
        public virtual Client Clients { get; set; }
    }
}
