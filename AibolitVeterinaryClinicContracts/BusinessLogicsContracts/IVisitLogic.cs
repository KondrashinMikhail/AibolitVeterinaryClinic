using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.ViewModels;

namespace AibolitVeterinaryClinicContracts.BusinessLogicsContracts
{
    public interface IVisitLogic
    {
        List<VisitViewModel> Read(VisitBindingModel model);
        void CreateOrUpdate(VisitBindingModel model);
        void Delete(VisitBindingModel model);
    }
}
