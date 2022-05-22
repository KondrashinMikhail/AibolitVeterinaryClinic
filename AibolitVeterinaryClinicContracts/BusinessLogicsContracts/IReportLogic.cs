using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AibolitVeterinaryClinicContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {
        void SaveVisitsToWordFile(ReportBindingModel model, int clientId);
        void SaveVisitsToExcelFile(ReportBindingModel model, int clientId);
        void SaveVisitsToPdfFile(ReportBindingModel model, int clientId);
        List<ReportVisitsViewModel> GetVisitsReportInfo(ReportBindingModel model, int clientId);
    }
}
