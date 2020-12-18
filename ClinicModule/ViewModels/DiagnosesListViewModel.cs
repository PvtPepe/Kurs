using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ClinicAppDAL.Models.ClinicModel;
using ClinicAppDAL.Repos;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace ClinicModule.ViewModels
{
    public class DiagnosesListViewModel : BindableBase
    {
        private BaseRepo<Diagnosis> diagRepo;
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        public DelegateCommand<Diagnosis> DiagnosisSelectedCommand { get; private set; }
        public DelegateCommand DiagnosisAddCommand { get; private set; }
        public DelegateCommand DiagnosisDeleteCommand { get; private set; }
        public DelegateCommand SearchCommand { get; private set; }
        public DelegateCommand ChangeCommand { get; private set; }

        public DiagnosesListViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
            diagRepo = new BaseRepo<Diagnosis>(new ClinicAppDAL.EF.ClinicAppClinicContext());
            Diagnoses = new ObservableCollection<Diagnosis>(diagRepo.GetAll());
            DiagnosisSelectedCommand = new DelegateCommand<Diagnosis>(DiagnosisSelected);
            DiagnosisDeleteCommand = new DelegateCommand(DiagnosisDelete,canDelete).ObservesProperty(()=>SelectedDiagnosis);
            DiagnosisAddCommand = new DelegateCommand(DiagnosisAdd);
            SearchCommand = new DelegateCommand(Search);
            ChangeCommand = new DelegateCommand(Change,canDelete).ObservesProperty(() => SelectedDiagnosis);
        }

        private ObservableCollection<Diagnosis> diagnoses;
        public ObservableCollection<Diagnosis> Diagnoses
        {
            get { return diagnoses; }
            set { SetProperty(ref diagnoses, value); }
        }

        private Diagnosis _selectedDiagnosis;
        public Diagnosis SelectedDiagnosis
        {
            get { return _selectedDiagnosis; }
            set { SetProperty(ref _selectedDiagnosis, value); }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        private void DiagnosisDelete()
        {
            _dialogService.ShowDialog("ClinicNotificationView", r =>
            {
                if(r.Result == ButtonResult.Yes)
                {
                    diagRepo.Delete(SelectedDiagnosis);
                    Diagnoses = new ObservableCollection<Diagnosis>(diagRepo.GetAll());
                }
            });
        }

        private bool canDelete()
        {
            return SelectedDiagnosis != null;
        }

        private void DiagnosisSelected(Diagnosis obj)
        {
            var p = new NavigationParameters();
            p.Add("diagnose", obj);
            _regionManager.RequestNavigate("DiagnosesMainRegion", "DiagnoseInfoView", p);
        }

        private void Search()
        {
            Diagnoses = new ObservableCollection<Diagnosis>(diagRepo.GetSome(x => x.DiagnosisName.StartsWith(SearchText)));
        }

        private void DiagnosisAdd()
        {
            _dialogService.ShowDialog("DiagnosisAddView",r=>
            {
                 if (r.Result == ButtonResult.OK) Diagnoses = new ObservableCollection<Diagnosis>(diagRepo.GetAll());
            });
        }

        private void Change()
        {
            var p = new DialogParameters();
            p.Add("key", SelectedDiagnosis);
            _dialogService.ShowDialog("DiagnosisAddView", p,r =>
            {
                if (r.Result == ButtonResult.OK) Diagnoses = new ObservableCollection<Diagnosis>(diagRepo.GetAll());
            });
        }
    }
}
