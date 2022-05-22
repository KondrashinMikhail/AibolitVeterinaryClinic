using AibolitVeterinaryClinicBusinessLogic.OfficePackage;
using AibolitVeterinaryClinicBusinessLogic.OfficePackage.HelperModels;
using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.BusinessLogicsContracts;
using AibolitVeterinaryClinicContracts.StoragesContracts;
using AibolitVeterinaryClinicContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AibolitVeterinaryClinicBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IVisitStorage _visitStorage;
        private readonly IMedicineStorage _medicineStorage;
        private readonly IAnimalStorage _animalStorage;
        private readonly IVaccinationStorage _vaccinationStorage;
        private readonly IServiceStorage _serviceStorage;

        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToPdf _saveToPdf;
        public ReportLogic(IVisitStorage visitStorage, IMedicineStorage medicineStorage,  AbstractSaveToWord saveToWord, AbstractSaveToExcel saveToExcel, AbstractSaveToPdf saveToPdf, IVaccinationStorage vaccinationStorage, IServiceStorage serviceStorage, IAnimalStorage animalStorage)
        {
            _visitStorage = visitStorage;
            _medicineStorage = medicineStorage;
            _saveToWord = saveToWord;
            _saveToExcel = saveToExcel;
            _saveToPdf = saveToPdf;
            _vaccinationStorage = vaccinationStorage;
            _serviceStorage = serviceStorage;
            _animalStorage = animalStorage;
        }
        public List<ReportVisitMedicineViewModel> GetVisitMedicine(int clientId) 
        {
            var visits = _visitStorage.GetFilteredList(new VisitBindingModel { ClientId = clientId });
            var list = new List<ReportVisitMedicineViewModel>();
            foreach (var visit in visits) 
            {
                var record = new ReportVisitMedicineViewModel
                {
                    VisitDate = visit.DateVisit,
                    Medicines = new List<string>()
                };
                foreach (var medicine in visit.Medicines) 
                    record.Medicines.Add(_medicineStorage.GetElement(new MedicineBindingModel { Id = medicine }).MedicineName);
                list.Add(record);
            }
            return list;
        }
        public void SaveVisitsToWordFile(ReportBindingModel model, int clientId)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список медикаментов по визитам",
                Visits = _visitStorage.GetFilteredList(new VisitBindingModel { ClientId = clientId }),
                Medicines = _medicineStorage.GetFullList()
            });
        }
        public void SaveVisitsToExcelFile(ReportBindingModel model, int clientId) 
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список медикаментов по визитам",
                VisitMedicines = GetVisitMedicine(clientId)
            });
        }
        public void SaveVisitsToPdfFile(ReportBindingModel model, int clientId)
        {
            
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Сведения по визитам за период",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Visits = GetVisitsReportInfo(model, clientId)
            });
        }
        public List<ReportVisitsViewModel> GetVisitsReportInfo(ReportBindingModel model, int clientId)
        {
            var list = new List<ReportVisitsViewModel>();
            var visits = _visitStorage.GetFilteredList(new VisitBindingModel
            {
                ClientId = clientId,
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });
           
            foreach (var visit in visits) 
            {
                var medicines = new List<string>();
                var animalVaccinations = new List<string>();
                foreach (var medicine in visit.Medicines) 
                    medicines.Add(_medicineStorage.GetElement(new MedicineBindingModel { Id = medicine }).MedicineName);
                foreach (var animal in visit.Animals)
                    foreach (var vaccination in _animalStorage.GetElement(new AnimalBindingModel { Id = animal }).AnimalVaccinationRecord)
                        animalVaccinations.Add(vaccination.Value.Item1);
                list.Add(new ReportVisitsViewModel
                {
                    ServiceName = visit.ServiceName,
                    DateVisit = visit.DateVisit,
                    Medicines = medicines,
                    AnimalVaccinations = animalVaccinations
                });
            }
            return list;
        }
    }
}
