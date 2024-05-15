using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.ViewModels;

namespace AibolitVeterinaryClinicContracts.StoragesContracts
{
    public interface IServiceStorage
    {
        List<ServiceViewModel> GetFullList();
        List<ServiceViewModel> GetFilteredList(ServiceBindingModel model);
        ServiceViewModel GetElement(ServiceBindingModel model);
        void Insert(ServiceBindingModel model);
        void Update(ServiceBindingModel model);
        void Delete(ServiceBindingModel model);
    }
}
