using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AibolitVeterinaryClinicDatabaseImplement.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required] public string ClientLogin { get; set; }
        [Required] public string ClientMail { get; set; }
        [Required] public string ClientName { get; set; }
        [Required] public string ClientPhoneNumber { get; set; }
        [ForeignKey ("ClientId")] public virtual List<Visit> Visits { get; set; }
        [ForeignKey ("ClientId")] public virtual List<Animal> Animals { get; set; }
    }
}
