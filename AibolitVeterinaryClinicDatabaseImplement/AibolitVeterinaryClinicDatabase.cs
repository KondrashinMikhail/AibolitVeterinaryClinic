using AibolitVeterinaryClinicDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace AibolitVeterinaryClinicDatabaseImplement
{
    public class AibolitVeterinaryClinicDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            if (optionsBuilder.IsConfigured == false)
                optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-87RD5TMK\SQLEXPRESSCOURCE;Initial Catalog=AibolitVeterinaryClinicDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Vaccination> Vaccinations { get; set; }
        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
        public virtual DbSet<AnimalVaccinationRecord> AnimalVaccinationRecords { get; set; }
        public virtual DbSet<ServiceMedicine> ServiceMedicines { get; set; }
        public virtual DbSet<ServiceVisit> ServiceVisits { get; set; }
        public virtual DbSet<VisitAnimal> VisitAnimals { get; set; }
        public virtual DbSet<VisitMedicine> VisitMedicines { get; set; }
    }
}
