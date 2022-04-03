using AibolitVeterinaryClinicContracts.BusinessLogicsContracts;
using AibolitVeterinaryClinicContracts.StoragesContracts;
using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.ViewModels;

namespace AibolitVeterinaryClinicBusinessLogic.BusinessLogics
{
    public class MedicineLogic : IMedicineLogic
    {
        private readonly IMedicineStorage _medicineStorage;
        public MedicineLogic(IMedicineStorage medicineStorage) => _medicineStorage = medicineStorage;
        public void CreateOrUpdate(MedicineBindingModel model)
        {
            if (model.Id.HasValue) _medicineStorage.Update(model);
            else _medicineStorage.Insert(model);
        }
        public void Delete(MedicineBindingModel model)
        {
            var element = _medicineStorage.GetElement(new MedicineBindingModel { Id = model.Id });
            if (element == null) throw new Exception("Лекарство не найдено");
            _medicineStorage.Delete(model);
        }
        public List<MedicineViewModel> Read(MedicineBindingModel model)
        {
            if (model == null) return _medicineStorage.GetFullList();
            if (model.Id.HasValue) return new List<MedicineViewModel> { _medicineStorage.GetElement(model) };
            return _medicineStorage.GetFilteredList(model);
        }
    }
}
