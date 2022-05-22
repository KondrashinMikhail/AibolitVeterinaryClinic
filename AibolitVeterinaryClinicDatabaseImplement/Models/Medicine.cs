using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AibolitVeterinaryClinicDatabaseImplement.Models
{
    public class Medicine
    {
        public int? Id { get; set; }
        public int? DoctorId { get; set; }
        [Required] public string MedicineName { get; set; }
        [ForeignKey ("MedicineId")] public virtual List<ServiceMedicine>? ServiceMedicines { get; set; }
        [ForeignKey ("MedicineId")] public virtual List<VisitMedicine>? VisitMedicines { get; set; }
        public virtual Doctor? Doctor { get; set; }
    }
}
