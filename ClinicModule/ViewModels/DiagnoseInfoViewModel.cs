using ClinicAppDAL.Models.ClinicModel;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Regions;

namespace ClinicModule.ViewModels
{
    public class DiagnoseInfoViewModel : BindableBase, INavigationAware
    {
        private Diagnosis _selectedDiagnosis;
        public Diagnosis SelectedDiagnosis
        {
            get { return _selectedDiagnosis; }
            set { SetProperty(ref _selectedDiagnosis, value); }
        }

        public DiagnoseInfoViewModel()
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("diagnose"))
                SelectedDiagnosis = navigationContext.Parameters.GetValue<Diagnosis>("diagnose");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
