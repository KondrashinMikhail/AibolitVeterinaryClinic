using AibolitVeterinaryClinicContracts.BusinessLogicsContracts;
using AibolitVeterinaryClinicContracts.StoragesContracts;
using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.ViewModels;

namespace AibolitVeterinaryClinicBusinessLogic.BusinessLogics
{
    public class VisitLogic : IVisitLogic
    {
        private readonly IVisitStorage _visitStorage;
        public VisitLogic (IVisitStorage visitStorage) => _visitStorage = visitStorage;
        public void CreateOrUpdate(VisitBindingModel model)
        {
            if (model.Id.HasValue) _visitStorage.Update(model);
            else _visitStorage.Insert(model);
        }

        public void Delete(VisitBindingModel model)
        {
            var element = _visitStorage.GetElement(new VisitBindingModel { Id = model.Id });
            if (element == null) throw new Exception("Визит не найден");
            _visitStorage.Delete(model);
        }

        public List<VisitViewModel> Read(VisitBindingModel model)
        {
            if (model == null) return _visitStorage.GetFullList();
            if (model.Id.HasValue) return new List<VisitViewModel> { _visitStorage.GetElement(model) };
            return _visitStorage.GetFilteredList(model);
        }
    }
}
