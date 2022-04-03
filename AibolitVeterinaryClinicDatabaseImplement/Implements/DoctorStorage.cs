using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.StoragesContracts;
using AibolitVeterinaryClinicContracts.ViewModels;
using AibolitVeterinaryClinicDatabaseImplement.Models;

namespace AibolitVeterinaryClinicDatabaseImplement.Implements
{
    public class DoctorStorage : IDoctorStorage
    {
        public List<DoctorViewModel> GetFullList()
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            return context.Doctors.Select(CreateModel).ToList();
        }
        public List<DoctorViewModel> GetFilteredList(DoctorBindingModel model)
        {
            if (model == null) return null;
            using var context = new AibolitVeterinaryClinicDatabase();
            return context.Doctors.Where(rec => rec.DoctorName.Contains(model.DoctorName)).Select(CreateModel).ToList();
        }
        public DoctorViewModel GetElement(DoctorBindingModel model)
        {
            if (model == null) return null;
            using var context = new AibolitVeterinaryClinicDatabase();
            var doctor = context.Doctors.FirstOrDefault(rec => rec.DoctorName == model.DoctorName || rec.Id == model.Id || rec.DoctorSpecification == model.DoctorSpecification);
            return doctor != null ? CreateModel(doctor) : null;
        }
        public void Insert(DoctorBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            context.Doctors.Add(CreateModel(model, new Doctor()));
            context.SaveChanges();
        }
        public void Update(DoctorBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            var element = context.Doctors.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null) throw new Exception("Врач не найден");
            CreateModel(model, element);
            context.SaveChanges();
        }
        public void Delete(DoctorBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            var element = context.Doctors.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null) throw new Exception("Врач не найден");
            context.Doctors.Remove(element);
            context.SaveChanges();
        }
        private static Doctor CreateModel(DoctorBindingModel model, Doctor doctor) 
        {
            doctor.DoctorName = model.DoctorName;
            doctor.DoctorSpecification = model.DoctorSpecification;
            return doctor;
        }
        private static DoctorViewModel CreateModel(Doctor doctor) 
        {
            return new DoctorViewModel 
            {
                Id = doctor.Id,
                DoctorName = doctor.DoctorName,
                DoctorSpecification = doctor.DoctorSpecification
            };
        } 
    }
}
