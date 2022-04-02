using AibolitVeterinaryClinicContracts.BusinessLogicsContracts;
using AibolitVeterinaryClinicContracts.StoragesContracts;
using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.ViewModels;

namespace AibolitVeterinaryClinicBusinessLogic.BusinessLogics
{
    public class ServiceLogic : IServiceLogic
    {
        private readonly IServiceStorage _serviceStorage;
        public ServiceLogic(IServiceStorage serviceStorage) => _serviceStorage = serviceStorage;
        public void CreateOrUpdate(ServiceBindingModel model)
        {
            if (model.Id.HasValue) _serviceStorage.Update(model);
            else _serviceStorage.Insert(model);
        }
        public void Delete(ServiceBindingModel model)
        {
            var element = _serviceStorage.GetElement(new ServiceBindingModel { Id = model.Id });
            if (element == null) throw new Exception("Услуга не найдена");
        }
        public List<ServiceViewModel> Read(ServiceBindingModel model)
        {
            if (model == null) return _serviceStorage.GetFullList();
            if (model.Id.HasValue) return new List<ServiceViewModel> { _serviceStorage.GetElement(model) };
            return _serviceStorage.GetFilteredList(model);
        }
    }
}
