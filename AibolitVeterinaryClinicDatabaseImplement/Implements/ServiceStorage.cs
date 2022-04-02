using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.StoragesContracts;
using AibolitVeterinaryClinicContracts.ViewModels;
using AibolitVeterinaryClinicDatabaseImplement.Models;

namespace AibolitVeterinaryClinicDatabaseImplement.Implements
{
    public class ServiceStorage : IServiceStorage
    {
        public List<ServiceViewModel> GetFullList()
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            return context.Services.Select(CreateModel).ToList();
        }
        public List<ServiceViewModel> GetFilteredList(ServiceBindingModel model)
        {
            if (model == null) return null;
            using var context = new AibolitVeterinaryClinicDatabase();
            return context.Services.Where(rec => rec.ServiceName.Contains(model.ServiceName)).Select(CreateModel).ToList();
        }
        public ServiceViewModel GetElement(ServiceBindingModel model)
        {
            if (model == null) return null;
            using var context = new AibolitVeterinaryClinicDatabase();
            var service = context.Services.FirstOrDefault(rec => rec.ServiceName == model.ServiceName || rec.Id == model.Id);
            return service != null ? CreateModel(service) : null;
        }
        public void Insert(ServiceBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            context.Services.Add(CreateModel(model, new Service()));
            context.SaveChanges();
        }
        public void Update(ServiceBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            var element = context.Services.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null) throw new Exception("Услуга не найдена");
            CreateModel(model, element);
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
        private static Service CreateModel(ServiceBindingModel model, Service service) 
        {
            service.ServiceName = model.ServiceName;
            return service;
        }
        private static ServiceViewModel CreateModel(Service service) 
        {
            return new ServiceViewModel
            {
                Id = service.Id,
                ServiceName = service.ServiceName
            };
        }
    }
}
