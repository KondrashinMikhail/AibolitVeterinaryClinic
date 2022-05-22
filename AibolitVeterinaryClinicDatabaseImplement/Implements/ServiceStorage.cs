using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.StoragesContracts;
using AibolitVeterinaryClinicContracts.ViewModels;
using AibolitVeterinaryClinicDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace AibolitVeterinaryClinicDatabaseImplement.Implements
{
    public class ServiceStorage : IServiceStorage
    {
        public List<ServiceViewModel> GetFullList()
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            return context.Services
                .Include(rec => rec.ServiceMedicines)
                .ThenInclude(rec => rec.Medicine)
                .ToList()
                .Select(CreateModel)
                .ToList();
        }
        public List<ServiceViewModel> GetFilteredList(ServiceBindingModel model)
        {
            if (model == null) return null;
            using var context = new AibolitVeterinaryClinicDatabase();
            return context.Services
                .Include(rec => rec.ServiceMedicines)
                .ThenInclude(rec => rec.Medicine)
                .Where(rec => rec.ServiceName.Contains(model.ServiceName) || rec.DoctorId == model.DoctorId)
                .ToList()
                .Select(CreateModel)
                .ToList();
        }
        public ServiceViewModel GetElement(ServiceBindingModel model)
        {
            if (model == null) return null;
            using var context = new AibolitVeterinaryClinicDatabase();
            var service = context.Services
                .Include(rec => rec.ServiceMedicines)
                .ThenInclude(rec => rec.Medicine)
                .FirstOrDefault(rec => rec.Id == model.Id );
            return service != null ? CreateModel(service) : null;
        }
        public void Insert(ServiceBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            var service = new Service
            {
                DoctorId = model.DoctorId,
                ServiceName = model.ServiceName,
                ServiceMedicines = new List<ServiceMedicine>()
            };
            context.Services.Add(service);
            context.SaveChanges();
        }
        public void Update(ServiceBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            var element = context.Services.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null) throw new Exception("Услуга не найдена");
            CreateModel(model, element, context);
            context.SaveChanges();
        }
        public void Delete(ServiceBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            var element = context.Services.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null) throw new Exception("Услуга не найдена");
            context.Services.Remove(element);
            context.SaveChanges();
        }
        private static Service CreateModel(ServiceBindingModel model, Service service, AibolitVeterinaryClinicDatabase context) 
        {
            service.DoctorId = model.DoctorId;
            service.ServiceName = model.ServiceName;
            if (model.Id.HasValue && model.ServiceMedicine != null)
            {
                var serviceMedicine = context.ServiceMedicines.Where(rec => rec.ServiceId == model.Id.Value).ToList();
                context.ServiceMedicines.RemoveRange(serviceMedicine.Where(rec => !model.ServiceMedicine.ContainsKey(rec.MedicineId)).ToList());
                context.SaveChanges();
                foreach (var updateMedicine in serviceMedicine)
                    if (model.ServiceMedicine.ContainsKey(updateMedicine.MedicineId))
                        model.ServiceMedicine.Remove(updateMedicine.MedicineId);
                context.SaveChanges();
                foreach (var pc in model.ServiceMedicine)
                {
                    context.ServiceMedicines.Add(new ServiceMedicine
                    {
                        ServiceId = service.Id,
                        MedicineId = pc.Key,
                    });
                    context.SaveChanges();
                }
            }
            return service;
        }
        private static ServiceViewModel CreateModel(Service service) 
        {
            return new ServiceViewModel
            {
                Id = service.Id,
                DoctorId = service.DoctorId,
                ServiceName = service.ServiceName,
                ServiceMedicine = service.ServiceMedicines.ToDictionary(rec => rec.MedicineId, rec => rec.Medicine.MedicineName),
            };
        }
    }
}
