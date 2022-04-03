using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.StoragesContracts;
using AibolitVeterinaryClinicContracts.ViewModels;
using AibolitVeterinaryClinicDatabaseImplement.Models;

namespace AibolitVeterinaryClinicDatabaseImplement.Implements
{
    public class MedicineStorage : IMedicineStorage
    {
        public List<MedicineViewModel> GetFullList()
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            return context.Medicines.Select(CreateModel).ToList();
        }
        public List<MedicineViewModel> GetFilteredList(MedicineBindingModel model)
        {
            if (model == null) return null;
            using var context = new AibolitVeterinaryClinicDatabase();
            return context.Medicines.Where(rec => rec.MedicineName.Contains(model.MedicineName)).Select(CreateModel).ToList();
        }
        public MedicineViewModel GetElement(MedicineBindingModel model)
        {
            if (model == null) return null;
            using var context = new AibolitVeterinaryClinicDatabase();
            var medicine = context.Medicines.FirstOrDefault(rec => rec.MedicineName == model.MedicineName || rec.Id == model.Id);
            return medicine != null ? CreateModel(medicine) : null;
        }
        public void Insert(MedicineBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            context.Medicines.Add(CreateModel(model, new Medicine()));
            context.SaveChanges();
        }
        public void Update(MedicineBindingModel model)
        {
            using var context =  new AibolitVeterinaryClinicDatabase();
            var element = context.Medicines.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null) throw new Exception("Медикамент не найден");
            CreateModel(model, element);
            context.SaveChanges();
        }
        public void Delete(MedicineBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            var element = context.Medicines.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null) throw new Exception("Медикамент не найден");
            context.Medicines.Remove(element);
            context.SaveChanges();
        }
        private Medicine CreateModel(MedicineBindingModel model, Medicine medicine) 
        {
            medicine.MedicineName = model.MedicineName;
            return medicine;
        }
        private MedicineViewModel CreateModel(Medicine medicine)
        {
            return new MedicineViewModel
            {
                Id = medicine.Id,
                MedicineName = medicine.MedicineName
            };
        }
    }
}
