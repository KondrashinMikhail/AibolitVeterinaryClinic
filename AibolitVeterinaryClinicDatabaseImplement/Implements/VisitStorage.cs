using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.StoragesContracts;
using AibolitVeterinaryClinicContracts.ViewModels;
using AibolitVeterinaryClinicDatabaseImplement.Models;

namespace AibolitVeterinaryClinicDatabaseImplement.Implements
{
    public class VisitStorage : IVisitStorage
    {
        public List<VisitViewModel> GetFullList()
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            return context.Visits.Select(CreateModel).ToList();
        }
        public List<VisitViewModel> GetFilteredList(VisitBindingModel model)
        {
            if (model == null) return null;
            using var context = new AibolitVeterinaryClinicDatabase();
            return context.Visits.Where(rec => rec.DateVisit.Equals(model.DateVisit)).Select(CreateModel).ToList();
        }
        public VisitViewModel GetElement(VisitBindingModel model)
        {
            if (model == null) return null;
            using var context = new AibolitVeterinaryClinicDatabase();
            var visit = context.Visits.FirstOrDefault(rec => rec.DateVisit == model.DateVisit || rec.Id == model.Id);
            return visit != null ? CreateModel(visit) : null;
        }
        public void Insert(VisitBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            context.Visits.Add(CreateModel(model, new Visit()));
            context.SaveChanges();
        }
        public void Update(VisitBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            var element = context.Visits.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null) throw new Exception("Визит не найден");
            CreateModel(model, element);
            context.SaveChanges();
        }
        public void Delete(VisitBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            var element = context.Visits.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null) throw new Exception("Визит не найден");
            context.Visits.Remove(element);
            context.SaveChanges();
        }
        private static Visit CreateModel(VisitBindingModel model, Visit visit) 
        {
            visit.DateVisit = model.DateVisit;
            return visit;
        }
        private static VisitViewModel CreateModel(Visit visit) 
        {
            return new VisitViewModel 
            {
                Id = visit.Id,
                DateVisit = visit.DateVisit
            };
        }
    }
}
