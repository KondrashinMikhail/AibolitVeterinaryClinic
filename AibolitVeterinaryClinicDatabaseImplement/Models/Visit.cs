using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AibolitVeterinaryClinicDatabaseImplement.Models
{
    public class Visit
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        [Required] public DateTime DateVisit { get; set; }
        [ForeignKey ("VisitId")] public virtual List<ServiceVisit> ServiceVisits { get; set; }
        [ForeignKey ("VisitId")] public virtual List<VisitAnimal> VisitAnimals { get; set; }
        [ForeignKey ("VisitId")] public virtual List<VisitMedicine> VisitMedicines { get; set; }
        public virtual Client Client { get; set; }
    }
}
