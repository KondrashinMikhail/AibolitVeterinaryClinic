using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.ViewModels;

namespace AibolitVeterinaryClinicContracts.StoragesContracts
{
    public interface IMedicineStorage
    {
        List<MedicineViewModel> GetFullList();
        List<MedicineViewModel> GetFilteredList(MedicineBindingModel model);
        MedicineViewModel GetElement(MedicineBindingModel model);
        void Insert(MedicineBindingModel model);
        void Update(MedicineBindingModel model);
        void Delete(MedicineBindingModel model);
    }
}
