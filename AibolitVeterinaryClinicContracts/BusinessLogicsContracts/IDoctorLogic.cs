using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.ViewModels;

namespace AibolitVeterinaryClinicContracts.BusinessLogicsContracts
{
    public interface IDoctorLogic
    {
        List<DoctorViewModel> Read(DoctorBindingModel model); 
        void CreateOrUpdate(DoctorBindingModel model);
        void Delete(DoctorBindingModel model);
    }
}
