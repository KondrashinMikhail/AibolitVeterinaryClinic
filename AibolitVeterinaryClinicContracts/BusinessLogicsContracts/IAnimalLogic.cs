using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.ViewModels;

namespace AibolitVeterinaryClinicContracts.BusinessLogicsContracts
{
    public interface IAnimalLogic
    {
        List<AnimalViewModel> Read(AnimalBindingModel model);
        void CreateOrUpdate(AnimalBindingModel model);
        void Delete(AnimalBindingModel model);
    }
}
