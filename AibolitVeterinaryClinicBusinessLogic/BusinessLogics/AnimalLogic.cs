using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.BusinessLogicsContracts;
using AibolitVeterinaryClinicContracts.StoragesContracts;
using AibolitVeterinaryClinicContracts.ViewModels;

namespace AibolitVeterinaryClinicBusinessLogic.BusinessLogics
{
    public class AnimalLogic : IAnimalLogic
    {
        private readonly IAnimalStorage _animalStorage;
        public AnimalLogic(IAnimalStorage animalStorage) => _animalStorage = animalStorage;
        public void CreateOrUpdate(AnimalBindingModel model)
        {
            if (model.Id.HasValue) _animalStorage.Update(model);
            else _animalStorage.Insert(model);
        }
        public void Delete(AnimalBindingModel model)
        {
            var element = _animalStorage.GetElement(new AnimalBindingModel { Id = model.Id });
            if (element == null) throw new Exception("Животное не найдено");
            _animalStorage.Delete(model);
        }
        public List<AnimalViewModel> Read(AnimalBindingModel model)
        {
            if (model == null) return _animalStorage.GetFullList();
            if (model.Id.HasValue) return new List<AnimalViewModel> { _animalStorage.GetElement(model) };
            return _animalStorage.GetFilteredList(model);
        }
    }
}
