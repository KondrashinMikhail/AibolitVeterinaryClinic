using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.BusinessLogicsContracts;
using AibolitVeterinaryClinicContracts.ViewModels;
using AibolitVeterinaryClinicDatabaseImplement;
using AibolitVeterinaryClinicDatabaseImplement.Models;
using System.Windows;
using System.Xml.Linq;

namespace AibolitVeterinaryClinicBusinessLogic
{
    public class WorkModeling
    {
        private readonly IMedicineLogic _medicineLogic;
        private readonly IVaccinationLogic _vaccinationLogic;
        private readonly IServiceLogic _serviceLogic;
        private readonly IAnimalLogic _animalLogic;
        private readonly IDoctorLogic _doctorLogic;
        private readonly IVisitLogic _visitLogic;
        private readonly int count = 5;
        private readonly List<string> Vacctinations = new() { "Нобивак", "Квадрикат", "Пуревакс", "Вакдерм", "Рабизин", "Мультикан", "Биовак", "Гексадок", "Вангард", "Эурикан" };
        private readonly List<string> Medicines = new() {"Амоксициллин", "Азитронит", "Альвет", "Дезоклин", "Гелерон", "Деквикокс", "Доксилокс", "Ивермек", "Клозатрем", "Левамизол" };
        private readonly List<string> Services = new() { "Эвантазия", "ЭКГ", "Общий осмотр", "Чипирование", "Груминг", "Стоматология", "Дерматология", "Хирургия", "Забор анализов", "Офтальмология" };
        private readonly List<DoctorBindingModel> Doctors = new()
        {
            new DoctorBindingModel {DoctorName = "Иванов А.И.", DoctorSpecification = "Хирургия" },
            new DoctorBindingModel {DoctorName = "Андреев В.И.", DoctorSpecification = "Хирургия" },
            new DoctorBindingModel {DoctorName = "Васильев И.В.", DoctorSpecification = "Хирургия" },
            new DoctorBindingModel {DoctorName = "Алексеев И.А.", DoctorSpecification = "Офтольмология" },
            new DoctorBindingModel {DoctorName = "Александров В.В.", DoctorSpecification = "Офтольмология" },
            new DoctorBindingModel {DoctorName = "Корнеев А.С.", DoctorSpecification = "Офтольмология" },
            new DoctorBindingModel {DoctorName = "Павлов П.Д.", DoctorSpecification = "Стоматология" },
            new DoctorBindingModel {DoctorName = "Петренко Д.П.", DoctorSpecification = "Стоматология" },
            new DoctorBindingModel {DoctorName = "Игнатов К.К.", DoctorSpecification = "Дерматология" },
            new DoctorBindingModel {DoctorName = "Игнатьев К.Г.", DoctorSpecification = "Дерматология" },
        };
        public WorkModeling(IMedicineLogic medicineLogic, IVaccinationLogic vaccinationLogic, IServiceLogic serviceLogic, IAnimalLogic animalLogic, IDoctorLogic doctorLogic, IVisitLogic visitLogic)
        {
            _medicineLogic = medicineLogic;
            _vaccinationLogic = vaccinationLogic;
            _serviceLogic = serviceLogic;
            _animalLogic = animalLogic;
            _doctorLogic = doctorLogic;
            _visitLogic = visitLogic;
        }
        public void CreateMedicines() 
        {
            var list = _medicineLogic.Read(null);
            List<string> bucketList = new();
            List<string> bucketMedicines = Medicines;
            bool flag = false;
            for (int i = 0; i < count - list.Count; i++)
            {
                int index = new Random().Next(bucketMedicines.Count);
                string randomName = bucketMedicines.ElementAt(index);
                foreach (var item in list)
                    if (item.MedicineName == randomName)
                        flag = true;
                if (!bucketList.Contains(randomName) && !flag)
                    bucketList.Add(randomName);
                flag = false;
                bucketMedicines.RemoveAt(index);
            }
            foreach (var name in bucketList)
                _medicineLogic.CreateOrUpdate(new MedicineBindingModel { MedicineName = name });
        }
        public void CreateVaccinations() 
        {
            var list = _vaccinationLogic.Read(null);
            List<string> bucketList = new ();
            List<string> bucketVaccinations = Vacctinations;
            bool flag = false;
            for (int i = 0; i < count - list.Count; i++) 
            {
                int index = new Random().Next(bucketVaccinations.Count);
                string randomName = Vacctinations.ElementAt(index);
                foreach (var item in list)
                    if (item.VaccinationName == randomName)
                        flag = true;
                if (!bucketList.Contains(randomName) && !flag)
                    bucketList.Add(randomName);
                flag = false;
                bucketVaccinations.RemoveAt(index);
            }
            foreach (var name in bucketList)
                _vaccinationLogic.CreateOrUpdate(new VaccinationBindingModel { VaccinationName = name });
        }
        public void CreateServices() 
        {
            var list = _serviceLogic.Read(null);
            List<string> bucketList = new();
            List<string> bucketServices = Services;
            bool flag = false;
            for (int i = 0; i < count - list.Count; i++)
            {
                int index = new Random().Next(bucketServices.Count);
                string randomName = Services.ElementAt(index);
                foreach (var item in list)
                    if (item.ServiceName == randomName)
                        flag = true;
                if (!bucketList.Contains(randomName) && !flag)
                    bucketList.Add(randomName);
                flag = false;
                bucketServices.RemoveAt(index);
            }
            foreach (var name in bucketList)
                _serviceLogic.CreateOrUpdate(new ServiceBindingModel { ServiceName = name});
        }
        public void CreateDoctors() 
        {
            var list = _doctorLogic.Read(null);
            List<DoctorBindingModel> bucketList = new();
            List<DoctorBindingModel> bucketDoctors = Doctors;
            bool flag = false;
            for (int i = 0; i < count - list.Count; i++)
            {
                int index = new Random().Next(bucketDoctors.Count);
                var randomDoctor = Doctors.ElementAt(index);
                foreach (var item in list)
                    if (item.DoctorName == randomDoctor.DoctorName)
                        flag = true;
                if (!bucketList.Contains(randomDoctor) && !flag)
                    bucketList.Add(randomDoctor);
                flag = false;
                bucketDoctors.RemoveAt(index);
            }
            foreach (var doctor in bucketList)
                _doctorLogic.CreateOrUpdate(new DoctorBindingModel { DoctorName = doctor.DoctorName, DoctorSpecification = doctor.DoctorSpecification });
        }
        public void Delete()
        {
            bool flag = false;
            foreach (var vaccination in _vaccinationLogic.Read(null))
            {
                foreach (var animal in _animalLogic.Read(null))
                    foreach (var record in animal.AnimalVaccinationRecord) 
                        if (vaccination.VaccinationName == record.Value.Item1) flag = true;
                if (!flag)
                    _vaccinationLogic.Delete(new VaccinationBindingModel { Id = vaccination.Id, VaccinationName = vaccination.VaccinationName });
                flag = false;
            }
            flag = false;

            foreach (var medicine in _medicineLogic.Read(null)) 
            {
                foreach (var visit in _visitLogic.Read(null))
                    foreach (var item in visit.Medicines) 
                        if (medicine.Id == item) flag = true;
                if (!flag)
                    _medicineLogic.Delete(new MedicineBindingModel { Id = medicine.Id });
                flag = false;
            }
            flag = false;

            foreach (var service in _serviceLogic.Read(null)) 
            {
                foreach (var visit in _visitLogic.Read(null))
                    foreach (var id in visit.Services)
                        if (service.Id == id) flag = true;
                if (!flag)
                    _serviceLogic.Delete(new ServiceBindingModel { Id = service.Id });
                flag = false;
            }
            flag = false;

            foreach (var doctor in _doctorLogic.Read(null))
            {
                foreach (var service in _serviceLogic.Read(null))
                    if (doctor.Id == service.DoctorId) flag = true;
                if (!flag)
                    _doctorLogic.Delete(new DoctorBindingModel { Id = doctor.Id });
                flag = false;
            }
        }
        public void MedicinesServiceBinding() 
        {
            List<ServiceViewModel> services = _serviceLogic.Read(null);
            List<MedicineViewModel> medicines = new();
            foreach (var service in services)
            {
                medicines = _medicineLogic.Read(null);
                var index = new Random().Next(medicines.Count);
                var medicine = medicines.ElementAt(index);
                List<MedicineBindingModel> list = new() { new MedicineBindingModel
                {
                    Id = medicine.Id,
                    MedicineName = medicine.MedicineName
                }};
                if (service.ServiceMedicine.Count == 0)
                    _serviceLogic.CreateOrUpdate(new ServiceBindingModel
                    {
                        Id = service.Id,
                        DoctorId = service.DoctorId,
                        ServiceName = service.ServiceName,
                        ServiceMedicine = list.ToDictionary(rec => (int)rec.Id, rec => rec.MedicineName)
                    });
                medicines.RemoveAt(index);
            }
        }
        public void DoctorsBinding() 
        {
            List<ServiceViewModel> services = _serviceLogic.Read(null);
            List<DoctorViewModel> doctors = new();
            foreach (var service in services) 
            {
                doctors = _doctorLogic.Read(null);
                var index = new Random().Next(doctors.Count);
                var doctor = doctors.ElementAt(index);
                foreach (var medicine in service.ServiceMedicine)
                    if (_medicineLogic.Read(new MedicineBindingModel { Id = medicine.Key })?[0].DoctorId == null)
                        _medicineLogic.CreateOrUpdate (new MedicineBindingModel
                        {
                            Id = medicine.Key,
                            DoctorId = doctor.Id,
                            MedicineName = medicine.Value
                        });
                if (service.DoctorId == null)
                    _serviceLogic.CreateOrUpdate(new ServiceBindingModel
                    {
                        Id = service.Id,
                        DoctorId = doctor.Id,
                        ServiceName= service.ServiceName,
                        ServiceMedicine = service.ServiceMedicine
                    });
                doctors.RemoveAt(index);
            }
        }
    }
}
