using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AibolitVeterinaryClinicDatabaseImplement.Models
{
    public class Service
    {
        public int Id { get; set; }
        public int? DoctorId { get; set; }
        [Required] public string ServiceName { get; set; }
        [ForeignKey ("ServiceId")] public virtual List<ServiceMedicine>? ServiceMedicines { get; set; }
        [ForeignKey ("ServiceId")] public virtual List<VisitService>? ServiceVisits { get; set; }
        public virtual Doctor? Doctor { get; set; }
    }
}
