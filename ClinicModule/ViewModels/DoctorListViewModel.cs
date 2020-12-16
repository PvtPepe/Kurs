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
    
    public class DoctorListViewModel : BindableBase
    {
        private DoctorRepo docRepo;
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        public DelegateCommand<Doctor> DoctorSelectedCommand { get; private set; }
        public DelegateCommand DoctorAddCommand { get; private set; }
        public DelegateCommand DoctorDeleteCommand { get; private set; }
        public DelegateCommand SearchCommand { get; private set; }

        public DoctorListViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
            docRepo = new DoctorRepo(new ClinicAppDAL.EF.ClinicAppClinicContext());
            Doctors = new ObservableCollection<Doctor>(docRepo.GetAll());
            DoctorSelectedCommand = new DelegateCommand<Doctor>(DoctorSelected);
            DoctorDeleteCommand = new DelegateCommand(DoctorDelete, canDelete).ObservesProperty(() => SelectedDoctor);
            DoctorAddCommand = new DelegateCommand(DoctorAdd);
            SearchCommand = new DelegateCommand(Search);
        }

        private ObservableCollection<Doctor> doctors;
        public ObservableCollection<Doctor> Doctors
        {
            get { return doctors; }
            set { SetProperty(ref doctors, value); }
        }

        private Doctor _selectedDoctor;
        public Doctor SelectedDoctor
        {
            get { return _selectedDoctor; }
            set { SetProperty(ref _selectedDoctor, value); }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        private void DoctorDelete()
        {
            docRepo.Delete(SelectedDoctor);
            Doctors = new ObservableCollection<Doctor>(docRepo.GetAll());
        }

        private bool canDelete()
        {
            return SelectedDoctor != null;
        }

        private void DoctorSelected(Doctor obj)
        {
            var p = new NavigationParameters();
            p.Add("doctor", obj);
            _regionManager.RequestNavigate("DoctorsMainRegion", "DoctorInfoView", p);
        }

        private void Search()
        {
            //Doctors = new ObservableCollection<Doctor>(docRepo.GetSome(x => x.DiagnosisName.StartsWith(SearchText)));
        }

        private void DoctorAdd()
        {
            _dialogService.ShowDialog("DiagnosisAddView", r =>
            {
                if (r.Result == ButtonResult.OK) Doctors = new ObservableCollection<Doctor>(docRepo.GetAll());
            });
        }

    }
}
