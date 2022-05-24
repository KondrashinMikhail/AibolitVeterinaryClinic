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
                .Include(rec => rec.Client)
                 .Where(rec => rec.ClientId.Equals(model.ClientId) || rec.Id == model.Id
                   || (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.DateVisit.Date == model.DateVisit.Date)
                   || (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateVisit.Date >= model.DateFrom.Value.Date && rec.DateVisit.Date <= model.DateTo.Value.Date))
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
            bool flag = true;

            if (model.Medicines!= null)
                foreach (var item in model.Medicines)
                {
                    foreach (var medicinesVisits in context.VisitMedicines.Where(rec => rec.VisitId == visit.Id.Value).ToList()) if (medicinesVisits.MedicineId == item) flag = false;                    
                    if (flag) 
                        context.VisitMedicines.Add(new VisitMedicine
                        {
                            VisitId = (int)visit.Id,
                            MedicineId = item
                        });
                    context.SaveChanges();
                    flag = true;
                }
            if (model.Animals != null)
                {
                    foreach (var item in model.Animals)
                    {
                        foreach (var animalVisit in context.VisitAnimals.Where(rec => rec.VisitId == visit.Id.Value).ToList()) if (animalVisit.AnimalId == item) flag = false;
                        if (flag)
                            context.VisitAnimals.Add(new VisitAnimal
                            {
                                VisitId = visit.Id,
                                AnimalId = item
                            });
                        context.SaveChanges();
                        flag = true;
                    }
                }
                if (model.Services != null)
                {
                    foreach (var item in model.Services)
                    {
                        if (context.ServiceVisits.Count() != 0)
                        {
                            foreach (var service in context.ServiceVisits.Where(rec => rec.VisitId == visit.Id.Value).ToList()) if (service.ServiceId == item) flag = false;
                            if (flag)
                                context.ServiceVisits.Add(new VisitService
                                {
                                    VisitId = visit.Id,
                                    ServiceId = item
                                });
                            context.SaveChanges();
                            flag = true;
                        }
                        else
                        {
                            context.ServiceVisits.Add(new VisitService
                            {
                                VisitId = visit.Id,
                                ServiceId = item
                            });
                            context.SaveChanges();
                        }
                       
                    }
                }
            context.SaveChanges();
            return visit;
        }
        private static VisitViewModel CreateModel(Visit visit) 
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            List<int> listMedicines = new();
            if (context.VisitMedicines != null) foreach (var item in context.VisitMedicines.Where(rec => rec.VisitId == visit.Id).ToList()) listMedicines.Add(item.MedicineId);
            List<int> listAnimals = new();
            if (context.VisitAnimals != null) foreach (var item in context.VisitAnimals.Where(rec => rec.VisitId == visit.Id).ToList()) listAnimals.Add((int)item.AnimalId);
            List<int> listServices = new();
            List<string> listServiceNames = new();
            if (context.ServiceVisits != null) foreach (var item in context.ServiceVisits.Where(rec => rec.VisitId == visit.Id).ToList()) 
                {
                    listServices.Add((int)item.ServiceId);
                    listServiceNames.Add(context.Services.FirstOrDefault(rec => rec.Id == item.ServiceId).ServiceName);
                }
            return new VisitViewModel 
            {
                Id = (int)visit.Id,
                DateVisit = visit.DateVisit,
                ClientId = visit.ClientId,
                Services = listServices,
                Animals = listAnimals,
                Medicines = listMedicines,
                ServiceNames = listServiceNames,
            };
        }
    }
}
