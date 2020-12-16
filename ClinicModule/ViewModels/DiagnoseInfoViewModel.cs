using ClinicAppDAL.Models.ClinicModel;
using ClinicAppDAL.Repos.ClinicRepo;
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
        DoctorVisitRepo visitRepo;
        
        private Diagnosis _selectedDiagnosis;
        public Diagnosis SelectedDiagnosis
        {
            get { return _selectedDiagnosis; }
            set { SetProperty(ref _selectedDiagnosis, value); }
        }

        private string _decNumb;
        public string DecNumb
        {
            get { return _decNumb; }
            set { SetProperty(ref _decNumb, value); }
        }

        public DiagnoseInfoViewModel()
        {
            visitRepo = new DoctorVisitRepo(new ClinicAppDAL.EF.ClinicAppClinicContext());
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("diagnose"))
                SelectedDiagnosis = navigationContext.Parameters.GetValue<Diagnosis>("diagnose");
            if(SelectedDiagnosis != null)
                DecNumb = visitRepo.GetDoctorVisitCountByDiagnosis(SelectedDiagnosis.Id).ToString();
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
