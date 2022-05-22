using AibolitVeterinaryClinicDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace AibolitVeterinaryClinicDatabaseImplement
{
    public class AibolitVeterinaryClinicDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            if (optionsBuilder.IsConfigured == false)
                optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-87RD5TMK\SQLEXPRESS1;Initial Catalog=AibolitVeterinaryClinicDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Service> Services { set; get; }
        public virtual DbSet<Doctor> Doctors { set; get; }
        public virtual DbSet<Vaccination> Vaccinations { set; get; }
        public virtual DbSet<Animal> Animals { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
        public virtual DbSet<Medicine> Medicines { set; get; }
        public virtual DbSet<Visit> Visits { set; get; }
        public virtual DbSet<AnimalVaccinationRecord> AnimalVaccinationRecords { set; get; }
        public virtual DbSet<ServiceMedicine> ServiceMedicines { set; get; }
        public virtual DbSet<VisitService> ServiceVisits { set; get; }
        public virtual DbSet<VisitAnimal> VisitAnimals { set; get; }
        public virtual DbSet<VisitMedicine> VisitMedicines { set; get; }
    }
}
