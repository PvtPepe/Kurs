using ClinicAppDAL.Models.ClinicModel;
using ClinicAppDAL.Repos.ClinicRepo;
using ClinicAppDAL.Repos;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Regions;


namespace ClinicModule.ViewModels
{
    public class VisitInfoViewModel : BindableBase, INavigationAware
    {
        private readonly BaseRepo<Patient> patRepo;
        private readonly BaseRepo<Diagnosis> diagRepo;
        private readonly DoctorRepo docRepo;

        public VisitInfoViewModel()
        {
            patRepo = new BaseRepo<Patient>(new ClinicAppDAL.EF.ClinicAppClinicContext());
            diagRepo = new BaseRepo<Diagnosis>(new ClinicAppDAL.EF.ClinicAppClinicContext());
            docRepo = new DoctorRepo(new ClinicAppDAL.EF.ClinicAppClinicContext());
        }

        private Doctor _doc;
        public Doctor SelectedDoctor
        {
            get { return _doc; }
            set { SetProperty(ref _doc, value); }
        }

        private Patient _pat;
        public Patient SelectedPatient
        {
            get { return _pat; }
            set { SetProperty(ref _pat, value); }
        }

        private DoctorVisit _vis;
        public DoctorVisit SelectedVisit
        {
            get { return _vis; }
            set { SetProperty(ref _vis, value); }
        }

        private Diagnosis _diag;
        public Diagnosis SelectedDiagnosis
        {
            get { return _diag; }
            set { SetProperty(ref _diag, value); }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("visit"))
            {
                SelectedVisit = navigationContext.Parameters.GetValue<DoctorVisit>("visit");
                if (SelectedVisit != null)
                {
                    SelectedDiagnosis = diagRepo.GetOne(SelectedVisit.DiagnosisID);
                    SelectedPatient = patRepo.GetOne(SelectedVisit.PatientID);
                    SelectedDoctor = docRepo.GetOne(SelectedVisit.DoctorID);
                }
            }
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
