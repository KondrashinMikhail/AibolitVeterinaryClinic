using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AibolitVeterinaryClinicDatabaseImplement.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        [Required] public string DoctorName { get; set; }
        [Required] public string DoctorSpecification { get; set; }
        [ForeignKey("DoctorId")] public virtual List<Medicine> Medicines { get; set; }
        [ForeignKey("DoctorId")] public virtual List<Service>? Services { get; set; }
        [ForeignKey("DoctorId")] public virtual List<Visit> Visits { get; set; }
    }
}
