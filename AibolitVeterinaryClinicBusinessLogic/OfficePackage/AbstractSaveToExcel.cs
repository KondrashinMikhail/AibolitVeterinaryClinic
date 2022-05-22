using AibolitVeterinaryClinicBusinessLogic.OfficePackage.HelperEnums;
using AibolitVeterinaryClinicBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AibolitVeterinaryClinicBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToExcel
    {
        public void CreateReport(ExcelInfo info)
        {
            CreateExcel(info);
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });
            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });
            uint rowIndex = 2;
            foreach (var pc in info.VisitMedicines)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = "Визит от " + pc.VisitDate.ToShortDateString() + ": ",
                    StyleInfo = ExcelStyleInfoType.Text
                });
                rowIndex++;
                foreach (var medicine in pc.Medicines)
                {
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "B",
                        RowIndex = rowIndex,
                        Text = medicine,
                        StyleInfo = ExcelStyleInfoType.Text
                    });
                }
                rowIndex++;
            }
            SaveExcel(info);
        }
        protected abstract void CreateExcel(ExcelInfo info);
        protected abstract void InsertCellInWorksheet(ExcelCellParameters excelParams);
        protected abstract void MergeCells(ExcelMergeParameters excelParams);
        protected abstract void SaveExcel(ExcelInfo info);
    }
}
