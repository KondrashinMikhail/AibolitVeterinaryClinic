using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.ViewModels;

namespace AibolitVeterinaryClinicContracts.BusinessLogicsContracts
{
    public interface IVaccinationLogic
    {
        List<VaccinationViewModel> Read(VaccinationBindingModel model);
        void CreateOrUpdate(VaccinationBindingModel model);
        void Delete(VaccinationBindingModel model);
    }
}
