using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.ViewModels;

namespace AibolitVeterinaryClinicContracts.StoragesContracts
{
    public interface IVaccinationStorage
    {
        List<VaccinationViewModel> GetFullList();
        List<VaccinationViewModel> GetFilteredList(VaccinationBindingModel model);
        VaccinationViewModel GetElement(VaccinationBindingModel model);
        void Insert(VaccinationBindingModel model);
        void Update(VaccinationBindingModel model);
        void Delete(VaccinationBindingModel model);
    }
}
