using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.StoragesContracts;
using AibolitVeterinaryClinicContracts.ViewModels;
using AibolitVeterinaryClinicDatabaseImplement.Models;

namespace AibolitVeterinaryClinicDatabaseImplement.Implements
{
    public class VaccinationStorage : IVaccinationStorage
    {
        public List<VaccinationViewModel> GetFullList()
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            return context.Vaccinations.Select(CreateModel).ToList();
        }
        public List<VaccinationViewModel> GetFilteredList(VaccinationBindingModel model)
        {
            if (model == null) return null;
            using var context = new AibolitVeterinaryClinicDatabase();
            return context.Vaccinations.Where(rec => rec.VaccinationName.Contains(model.VaccinationName)).Select(CreateModel).ToList();
        }
        public VaccinationViewModel GetElement(VaccinationBindingModel model)
        {
            if (model == null) return null;
            using var context = new AibolitVeterinaryClinicDatabase();
            var vaccination = context.Vaccinations
            .FirstOrDefault(rec => rec.VaccinationName == model.VaccinationName || rec.Id == model.Id);
            return vaccination != null ? CreateModel(vaccination) : null;
        }
        public void Insert(VaccinationBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            context.Vaccinations.Add(CreateModel(model, new Vaccination()));
            context.SaveChanges();
        }
        public void Update(VaccinationBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            var element = context.Vaccinations.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null) throw new Exception("Прививка не найден");
            CreateModel(model, element);
            context.SaveChanges();
        }
        public void Delete(VaccinationBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            var element = context.Vaccinations.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null) throw new Exception("Прививка не найдена");
        }
        private static Vaccination CreateModel(VaccinationBindingModel model, Vaccination vaccination) 
        {
            vaccination.VaccinationName = model.VaccinationName;
            return vaccination;
        }
        private static VaccinationViewModel CreateModel(Vaccination vaccination) 
        {
            return new VaccinationViewModel 
            {
                Id = vaccination.Id,
                VaccinationName=vaccination.VaccinationName
            };
        }
    }
}
