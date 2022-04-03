using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.ViewModels;

namespace AibolitVeterinaryClinicContracts.BusinessLogicsContracts
{
    public interface IMedicineLogic
    {
        List<MedicineViewModel> Read(MedicineBindingModel model); 
        void CreateOrUpdate(MedicineBindingModel model); 
        void Delete(MedicineBindingModel model);
    }
}
