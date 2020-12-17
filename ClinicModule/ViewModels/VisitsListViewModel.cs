using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ClinicAppDAL.Models.ClinicModel;
using ClinicAppDAL.Repos.ClinicRepo;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace ClinicModule.ViewModels
{
    public class VisitsListViewModel : BindableBase
    {

        private DoctorVisitRepo repo;
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        public DelegateCommand<DoctorVisit> SelectedCommand { get; private set; }
        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }
        public DelegateCommand SearchCommand { get; private set; }

        public VisitsListViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
            repo = new DoctorVisitRepo(new ClinicAppDAL.EF.ClinicAppClinicContext());
            Visits = new ObservableCollection<DoctorVisit>(repo.GetAll());
            SelectedCommand = new DelegateCommand<DoctorVisit>(Selected);
            DeleteCommand = new DelegateCommand(Delete, canDelete).ObservesProperty(() => SelectedVisit);
            AddCommand = new DelegateCommand(Add);
            SearchCommand = new DelegateCommand(Search);
            VisitCount = repo.GetDoctorVisitCount().ToString();
            SearchText = DateTime.Now;
        }

        private ObservableCollection<DoctorVisit> diagnoses;
        public ObservableCollection<DoctorVisit> Visits
        {
            get { return diagnoses; }
            set { SetProperty(ref diagnoses, value); }
        }

        private DoctorVisit _selectedDiagnosis;
        public DoctorVisit SelectedVisit
        {
            get { return _selectedDiagnosis; }
            set { SetProperty(ref _selectedDiagnosis, value); }
        }

        private string _visitCount;
        public string VisitCount
        {
            get { return _visitCount; }
            set { SetProperty(ref _visitCount, value); }
        }

        private DateTime _searchText;
        public DateTime SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        private void Delete()
        {
            repo.Delete(SelectedVisit);
            Visits = new ObservableCollection<DoctorVisit>(repo.GetAll());
        }

        private bool canDelete()
        {
            return SelectedVisit != null;
        }

        private void Selected(DoctorVisit obj)
        {
            var p = new NavigationParameters();
            p.Add("visit", obj);
            _regionManager.RequestNavigate("VisitsMainRegion", "VisitInfoView", p);
        }

        private void Search()
        {
            Visits = new ObservableCollection<DoctorVisit>(repo.GetSome(x => x.VisitDate == SearchText));
        }

        private void Add()
        {
            _dialogService.ShowDialog("VisitAddView", r =>
            {
                if (r.Result == ButtonResult.OK) Visits = new ObservableCollection<DoctorVisit>(repo.GetAll());
            });
        }
    }
}
