using AibolitVeterinaryClinicContracts.BusinessLogicsContracts;
using AibolitVeterinaryClinicContracts.StoragesContracts;
using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.ViewModels;

namespace AibolitVeterinaryClinicBusinessLogic.BusinessLogics
{
    public class VaccinationLogic : IVaccinationLogic
    {
        private readonly IVaccinationStorage _vaccinationStorage;
        public VaccinationLogic(IVaccinationStorage vaccinationStorage) => _vaccinationStorage = vaccinationStorage;
        public void CreateOrUpdate(VaccinationBindingModel model)
        {
            if (model.Id.HasValue) _vaccinationStorage.Update(model);
            else _vaccinationStorage.Insert(model);
        }
        public void Delete(VaccinationBindingModel model)
        {
            var element = _vaccinationStorage.GetElement(new VaccinationBindingModel { Id = model.Id });
            if (element == null) throw new Exception("Прививка не найдена");
            _vaccinationStorage.Delete(model);
        }
        public List<VaccinationViewModel> Read(VaccinationBindingModel model)
        {
            if (model == null) return _vaccinationStorage.GetFullList();
            if (model.Id.HasValue) return new List<VaccinationViewModel> { _vaccinationStorage.GetElement(model) };
            return _vaccinationStorage.GetFilteredList(model);
        }
    }
}
