using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.StoragesContracts;
using AibolitVeterinaryClinicContracts.ViewModels;
using AibolitVeterinaryClinicDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace AibolitVeterinaryClinicDatabaseImplement.Implements
{
    public class AnimalStorage : IAnimalStorage
    {
        public List<AnimalViewModel> GetFullList()
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            return context.Animals
                .Include(rec => rec.AnimalVaccinationRecords)
                .ThenInclude(rec => rec.Vaccination)
                .ToList()
                .Select(CreateModel)
                .ToList();
        }
        public List<AnimalViewModel> GetFilteredList(AnimalBindingModel model)
        {
            if (model == null) return null;
            using var context = new AibolitVeterinaryClinicDatabase();
            return context.Animals
                .Include(rec => rec.AnimalVaccinationRecords)
                .ThenInclude(rec => rec.Vaccination)
                .Where(rec => rec.AnimalName.Contains(model.AnimalName) || rec.ClientId == model.ClientId || rec.Id == model.Id)
                .ToList()
                .Select(CreateModel).ToList();
        }
        public AnimalViewModel GetElement(AnimalBindingModel model)
        {
            if (model == null) return null;
            using var context = new AibolitVeterinaryClinicDatabase();
            var animal = context.Animals
            .Include(rec => rec.AnimalVaccinationRecords)
            .ThenInclude(rec => rec.Vaccination)
            .FirstOrDefault(rec => rec.AnimalName == model.AnimalName || rec.Id == model.Id);
            return animal != null ? CreateModel(animal) : null;
        }
        public void Insert(AnimalBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            var animal = new Animal
            {
                ClientId = model.ClientId,
                AnimalName = model.AnimalName,
                AnimalBreed = model.AnimalBreed
            };
            context.Animals.Add(animal);
            context.SaveChanges();
        }

        public void Update(AnimalBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            var element = context.Animals.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null) throw new Exception("Животное не найдено");
            CreateModel(model, element, context);
            context.SaveChanges();
        }
        public void Delete(AnimalBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            var element = context.Animals.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null) throw new Exception("Животное не найдено");
            context.Animals.Remove(element);
            context.SaveChanges();
        }
        private static Animal CreateModel(AnimalBindingModel model, Animal animal, AibolitVeterinaryClinicDatabase context) 
        {
            animal.ClientId = model.ClientId;
            animal.AnimalName = model.AnimalName;
            animal.AnimalBreed = model.AnimalBreed;
            if (model.Id.HasValue && model.AnimalVaccinationRecord != null)
            {
                var animalVaccinationRecords = context.AnimalVaccinationRecords.Where(rec => rec.AnimalId == model.Id.Value).ToList();
                context.AnimalVaccinationRecords.RemoveRange(animalVaccinationRecords.Where(rec => !model.AnimalVaccinationRecord.ContainsKey(rec.VaccinationId)).ToList());
                context.SaveChanges();
                foreach (var updateVaccination in animalVaccinationRecords)
                {
                    if (model.AnimalVaccinationRecord.ContainsKey(updateVaccination.VaccinationId))
                    {
                        updateVaccination.Date = model.AnimalVaccinationRecord[updateVaccination.VaccinationId].Item2;
                        model.AnimalVaccinationRecord.Remove(updateVaccination.VaccinationId);
                    }
                }
                context.SaveChanges();
                foreach (var pc in model.AnimalVaccinationRecord)
                {
                    context.AnimalVaccinationRecords.Add(new AnimalVaccinationRecord
                    {
                        AnimalId = animal.Id,
                        VaccinationId = pc.Key,
                        Date = pc.Value.Item2
                    });
                    context.SaveChanges();
                }
            }
            return animal;
        }
        private static AnimalViewModel CreateModel(Animal animal) 
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            return new AnimalViewModel
            {
                Id = animal.Id,
                ClientId = animal.ClientId,
                //ClientName = context.Clients.FirstOrDefault(rec => rec.Id == animal.ClientId)?.ClientName,
                AnimalName = animal.AnimalName,
                AnimalBreed = animal.AnimalBreed,
                AnimalVaccinationRecord = animal.AnimalVaccinationRecords.ToDictionary(rec => rec.VaccinationId, rec => (rec.Vaccination?.VaccinationName, rec.Date))
            };
        }
    }
}
