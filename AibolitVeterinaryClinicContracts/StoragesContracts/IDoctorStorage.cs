﻿using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.ViewModels;

namespace AibolitVeterinaryClinicContracts.StoragesContracts
{
    public interface IDoctorStorage
    {
        List<DoctorViewModel> GetFullList();
        List<DoctorViewModel> GetFilteredList(DoctorBindingModel model);
        DoctorViewModel GetElement(DoctorBindingModel model);
        void Insert(DoctorBindingModel model);
        void Update(DoctorBindingModel model);
        void Delete(DoctorBindingModel model);
    }
}
