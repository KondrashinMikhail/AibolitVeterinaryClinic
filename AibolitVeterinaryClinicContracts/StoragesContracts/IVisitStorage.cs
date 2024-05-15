using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.ViewModels;

namespace AibolitVeterinaryClinicContracts.StoragesContracts
{
    public interface IVisitStorage
    {
        List<VisitViewModel> GetFullList();
        List<VisitViewModel> GetFilteredList(VisitBindingModel model);
        VisitViewModel GetElement(VisitBindingModel model);
        void Insert(VisitBindingModel model);
        void Update(VisitBindingModel model);
        void Delete(VisitBindingModel model);
    }
}
