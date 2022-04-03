using AibolitVeterinaryClinicContracts.BusinessLogicsContracts;
using AibolitVeterinaryClinicContracts.StoragesContracts;
using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.ViewModels;

namespace AibolitVeterinaryClinicBusinessLogic.BusinessLogics
{
    public class DoctorLogic : IDoctorLogic
    {
        private readonly IDoctorStorage _doctorStorage;
        public DoctorLogic(IDoctorStorage doctorStorage) => _doctorStorage = doctorStorage;
        public void CreateOrUpdate(DoctorBindingModel model)
        {
            if (model.Id.HasValue) _doctorStorage.Update(model);
            else _doctorStorage.Insert(model);
        }
        public void Delete(DoctorBindingModel model)
        {
            var element = _doctorStorage.GetElement(new DoctorBindingModel { Id = model.Id });
            if (element == null) throw new Exception("Врач не найден");
            _doctorStorage.Delete(model);
        }
        public List<DoctorViewModel> Read(DoctorBindingModel model)
        {
            if (model == null) return _doctorStorage.GetFullList();
            if (model.Id.HasValue) return new List<DoctorViewModel> { _doctorStorage.GetElement(model) };
            return _doctorStorage.GetFilteredList(model);
        }
    }
}
