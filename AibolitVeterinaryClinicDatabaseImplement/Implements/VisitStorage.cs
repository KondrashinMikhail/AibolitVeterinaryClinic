using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.StoragesContracts;
using AibolitVeterinaryClinicContracts.ViewModels;
using AibolitVeterinaryClinicDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace AibolitVeterinaryClinicDatabaseImplement.Implements
{
    public class VisitStorage : IVisitStorage
    {
        public List<VisitViewModel> GetFullList()
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            return context.Visits
                .Include(rec => rec.VisitServices)
                .ThenInclude(rec => rec.Service)
                .Include(rec => rec.VisitMedicines)
                .ThenInclude(rec => rec.Medicine)
                .Include(rec => rec.VisitAnimals)
                .ThenInclude(rec => rec.Animal)
                .ToList()
                .Select(CreateModel).ToList();
        }
        public List<VisitViewModel> GetFilteredList(VisitBindingModel model)
        {
            if (model == null) return null;
            using var context = new AibolitVeterinaryClinicDatabase();
            return context.Visits
                .Include(rec => rec.VisitServices)
                .ThenInclude(rec => rec.Service)
                .Include(rec => rec.VisitMedicines)
                .ThenInclude(rec => rec.Medicine)
                .Include(rec => rec.VisitAnimals)
                .ThenInclude(rec => rec.Animal)
                 .Where(rec => (rec.ClientId == model.ClientId)
                || (model.DateFrom.HasValue && model.DateTo.HasValue && model.DateFrom <= rec.DateVisit && model.DateTo >= rec.DateVisit))
                .ToList()
                .Select(CreateModel)
                .ToList();
        }
        public VisitViewModel GetElement(VisitBindingModel model)
        {
            if (model == null) return null;
            using var context = new AibolitVeterinaryClinicDatabase();
            var visit = context.Visits.FirstOrDefault(rec => rec.DateVisit == model.DateVisit || rec.Id == model.Id);
            return visit != null ? CreateModel(visit) : null;
        }
        public void Insert(VisitBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            var visit = new Visit
            {
                DateVisit = model.DateVisit,
                ClientId = model.ClientId,
                DoctorId = model.DoctorId
            };
            context.Visits.Add(visit);
            context.SaveChanges();
            CreateModel(model, visit, context);
            context.SaveChanges();
        }
        public void Update(VisitBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            var element = context.Visits.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null) throw new Exception("Визит не найден");
            CreateModel(model, element, context);
            context.SaveChanges();
        }
        public void Delete(VisitBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            var element = context.Visits.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null) throw new Exception("Визит не найден");
            context.ServiceVisits.Remove(context.ServiceVisits.FirstOrDefault(rec => rec.VisitId == model.Id));
            context.Visits.Remove(element);
            context.SaveChanges();
        }
        private static Visit CreateModel(VisitBindingModel model, Visit visit, AibolitVeterinaryClinicDatabase context) 
        {
            visit.DateVisit = model.DateVisit;
            visit.ClientId = model.ClientId;
            visit.DoctorId = model.DoctorId;
            visit.ServiceId = model.ServiceId;
            if (model.Id.HasValue)
            {
                if (model.Medicines != null)
                {
                    var visitMedicine = context.VisitMedicines.Where(rec => rec.VisitId == model.Id.Value).ToList();
                    context.VisitMedicines.RemoveRange(visitMedicine.Where(rec => !model.Medicines.Contains(rec.MedicineId)).ToList());
                    context.SaveChanges();
                    foreach (var update in visitMedicine)
                        if (model.Medicines.Contains(update.MedicineId))
                            model.Medicines.Remove(update.MedicineId);
                    context.SaveChanges();
                }
                var visitAnimal = context.VisitAnimals.Where(rec => rec.VisitId == model.Id.Value).ToList();
                context.VisitAnimals.RemoveRange(visitAnimal.Where(rec => !model.Animals.Contains((int)rec.AnimalId)).ToList());
                context.SaveChanges();
                foreach (var update in visitAnimal)
                    if (model.Animals.Contains((int)update.AnimalId))
                        model.Animals.Remove((int)update.AnimalId);
                context.SaveChanges();
                   
            }
            foreach (var item in model.Medicines)
            {
                context.VisitMedicines.Add(new VisitMedicine
                {
                    VisitId = (int)visit.Id,
                    MedicineId = (int)context.Medicines.FirstOrDefault(rec => rec.DoctorId == model.DoctorId).Id
                });
                context.SaveChanges();
            }
            if (model.Animals != null)
                foreach (var item in model.Animals)
                {
                    context.VisitAnimals.Add(new VisitAnimal
                    {
                        VisitId = visit.Id,
                        AnimalId = item
                    });
                    context.SaveChanges();
                }
            context.ServiceVisits.Add(new VisitService
            {
                VisitId = visit.Id,
                ServiceId = model.ServiceId
            });
            context.SaveChanges();
            return visit;
        }
        private static VisitViewModel CreateModel(Visit visit) 
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            List<int> listMedicines = new();
            if (visit.VisitMedicines != null)
                foreach (var item in context.VisitMedicines.Where(rec => rec.VisitId == visit.Id).ToList()) 
                    listMedicines.Add(item.MedicineId);
            List<int> listAnimals = new();
            if (visit.VisitAnimals != null)
                foreach (var item in visit.VisitAnimals.Where(rec => rec.VisitId == visit.Id).ToList())
                    listAnimals.Add((int)item.AnimalId);
            return new VisitViewModel 
            {
                Id = (int)visit.Id,
                DateVisit = visit.DateVisit,
                ClientId = visit.ClientId,
                DoctorId = visit.DoctorId,
                ServiceId = visit.ServiceId,
                Animals = listAnimals,
                Medicines = listMedicines,
                ServiceName = context.Services.FirstOrDefault(rec => rec.Id == visit.ServiceId).ServiceName,
                DoctorName = context.Doctors.FirstOrDefault(rec => rec.Id == visit.DoctorId).DoctorName
            };
        }
    }
}
