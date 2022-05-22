using AibolitVeterinaryClinicBusinessLogic.OfficePackage.HelperEnums;
using AibolitVeterinaryClinicBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AibolitVeterinaryClinicBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToWord
    {
        public void CreateDoc(WordInfo info)
        {
            CreateWord(info);
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24", }) },
                TextProperties = new WordTextProperties { Size = "24", JustificationType = WordJustificationType.Center }
            });
            foreach (var visit in info.Visits)
            {
                string str = "";
                foreach (var medicine in visit.Medicines)  foreach (var item in info.Medicines)  if (item.Id == medicine) str += item.MedicineName + "; ";
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> { ("Визит от " + visit.DateVisit.ToShortDateString() + ": ", new WordTextProperties { Size = "24", Bold = true }),
                    (str, new WordTextProperties { Size = "24" })}, TextProperties = new WordTextProperties { Size = "24", JustificationType = WordJustificationType.Both }
                });
            }
            SaveWord(info);
        }
        protected abstract void CreateWord(WordInfo info);
        protected abstract void CreateParagraph(WordParagraph paragraph);
        protected abstract void CreateTable(List<string> tableHeaderInfo);
        protected abstract void AddRow(List<string> tableRowInfo);
        protected abstract void SaveWord(WordInfo info);
    }
}
