using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ClinicAppDAL.Models.ClinicModel;
using ClinicAppDAL.Repos;
using Prism.Regions;

namespace ClinicModule.ViewModels
{
    public class DiagnosesListViewModel : BindableBase
    {
        private BaseRepo<Diagnosis> diagRepo;
        public DelegateCommand<Diagnosis> DiagnosisSelectedCommand { get; private set; }

        public DiagnosesListViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            diagRepo = new BaseRepo<Diagnosis>(new ClinicAppDAL.EF.ClinicAppClinicContext());
            Diagnoses = new ObservableCollection<Diagnosis>(diagRepo.GetSome(x => (x.Id < 100)));
            DiagnosisSelectedCommand = new DelegateCommand<Diagnosis>(DiagnosisSelected);
        }

        private readonly IRegionManager _regionManager;

        private ObservableCollection<Diagnosis> diagnoses;
        public ObservableCollection<Diagnosis> Diagnoses
        {
            get { return diagnoses; }
            set { SetProperty(ref diagnoses, value); }
        }

        private void DiagnosisSelected(Diagnosis obj)
        {
            if (obj == null) return;
            var p = new NavigationParameters();
            p.Add("diagnose", obj);
            _regionManager.RequestNavigate("DiagnosesMainRegion", "DiagnoseInfoView", p);
        }
    }
}
