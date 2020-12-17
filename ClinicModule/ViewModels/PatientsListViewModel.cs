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
    public class PatientsListViewModel : BindableBase
    {
        private BaseRepo<Patient> patRepo;
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        public DelegateCommand<Patient> PatientSelectedCommand { get; private set; }
        public DelegateCommand PatientAddCommand { get; private set; }
        public DelegateCommand PatientDeleteCommand { get; private set; }
        public DelegateCommand SearchCommand { get; private set; }

        public PatientsListViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
            patRepo = new BaseRepo<Patient>(new ClinicAppDAL.EF.ClinicAppClinicContext());
            Patients = new ObservableCollection<Patient>(patRepo.GetAll());
            PatientSelectedCommand = new DelegateCommand<Patient>(PatientSelected);
            PatientDeleteCommand = new DelegateCommand(PatientDelete, canDelete).ObservesProperty(() => SelectedPatient);
            PatientAddCommand = new DelegateCommand(PatientAdd);
            SearchCommand = new DelegateCommand(Search);
        }

        private ObservableCollection<Patient> patients;
        public ObservableCollection<Patient> Patients
        {
            get { return patients; }
            set { SetProperty(ref patients, value); }
        }

        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set { SetProperty(ref _selectedPatient, value); }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        private string _searchCombo;
        public string SearchCombo
        {
            get { return _searchCombo; }
            set { SetProperty(ref _searchCombo, value); }
        }

        private void PatientDelete()
        {

            _dialogService.ShowDialog("ClinicNotificationView", r =>
            {
                if (r.Result == ButtonResult.Yes)
                {
                    patRepo.Delete(SelectedPatient);
                    Patients = new ObservableCollection<Patient>(patRepo.GetAll());
                }
            });
            
        }

        private bool canDelete()
        {
            return SelectedPatient != null;
        }

        private void PatientSelected(Patient obj)
        {
            var p = new NavigationParameters();
            p.Add("patient", obj);
            _regionManager.RequestNavigate("PatientMainRegion", "PatientInfoView", p);
        }

        private void Search()
        {
            switch (SearchCombo)
            {
                case "First Name":
                    Patients = new ObservableCollection<Patient>(patRepo.GetSome(x => x.FirstName.StartsWith(SearchText)));
                    break;
                case "Middle Name":
                    Patients = new ObservableCollection<Patient>(patRepo.GetSome(x => x.MidName.StartsWith(SearchText)));
                    break;
                case "Last Name":
                    Patients = new ObservableCollection<Patient>(patRepo.GetSome(x => x.LastName.StartsWith(SearchText)));
                    break;
            }
        }

        private void PatientAdd()
        {
            _dialogService.ShowDialog("PatientAddView", r =>
            {
                if (r.Result == ButtonResult.OK) Patients = new ObservableCollection<Patient>(patRepo.GetAll());
            });
        }
    }
}
