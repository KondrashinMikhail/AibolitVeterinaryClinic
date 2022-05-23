using AibolitVeterinaryClinicBusinessLogic.OfficePackage.HelperEnums;
using AibolitVeterinaryClinicBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AibolitVeterinaryClinicBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdf
    {
        public void CreateDoc(PdfInfo info)
        {
            CreatePdf(info);
            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });
            CreateParagraph(new PdfParagraph
            {
                Text = $"с {info.DateFrom.ToShortDateString()} по {info.DateTo.ToShortDateString()}",
                Style = "Normal"
            });
            CreateTable(new List<string> { "4cm", "3cm", "5cm", "5cm" });
            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "Название услуги", "Дата визита", "Медикаменты", "Прививки животных" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });
            foreach (var visit in info.Visits)
            {
                string medicines = "";
                string animalVaccinations = "";
                string services = "";
                foreach (var medicine in visit.Medicines)
                    medicines += (medicine + "; ");
                foreach (var animalVaccination in visit.AnimalVaccinations)
                    animalVaccinations += (animalVaccination + "; ");
                foreach (var service in visit.ServiceNames)
                    services += (service + "; ");
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { services, visit.DateVisit.ToShortDateString(), medicines, animalVaccinations },
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }
            SavePdf(info);
        }
        protected abstract void CreatePdf(PdfInfo info);
        protected abstract void CreateParagraph(PdfParagraph paragraph);
        protected abstract void CreateTable(List<string> columns);
        protected abstract void CreateRow(PdfRowParameters rowParameters);
        protected abstract void SavePdf(PdfInfo info);
    }
}
