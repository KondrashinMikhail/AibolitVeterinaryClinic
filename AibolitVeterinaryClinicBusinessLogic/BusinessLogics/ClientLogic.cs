using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.BusinessLogicsContracts;
using AibolitVeterinaryClinicContracts.StoragesContracts;
using AibolitVeterinaryClinicContracts.ViewModels;

namespace AibolitVeterinaryClinicBusinessLogic.BusinessLogics
{
    public class ClientLogic : IClientLogic
    {
        private readonly IClientStorage _clientStorage;
        public ClientLogic(IClientStorage clientStorage) => _clientStorage = clientStorage;
        public void CreateOrUpdate(ClientBindingModel model)
        {
            if (model.Id.HasValue) _clientStorage.Update(model);
            else _clientStorage.Insert(model);
        }
        public void Delete(ClientBindingModel model)
        {
            var element = _clientStorage.GetElement(new ClientBindingModel { Id = model.Id });
            if (element == null) throw new Exception("Клиент не найден");
            _clientStorage.Delete(model);
        }
        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            if (model == null) return _clientStorage.GetFullList();
            if (model.Id.HasValue) return new List<ClientViewModel> { _clientStorage.GetElement(model) };
            return _clientStorage.GetFilteredList(model);
        }
    }
}
