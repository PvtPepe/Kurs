using ClinicAppDAL.Models.ClinicModel;
using ClinicAppDAL.Repos.ClinicRepo;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace ClinicModule.ViewModels
{
    public class DoctorInfoViewModel : BindableBase, INavigationAware
    {
        public DelegateCommand DShowList { get; private set; }

        public DoctorInfoViewModel()
        {
            repo = new DoctorRepo(new ClinicAppDAL.EF.ClinicAppClinicContext());
            DShowList = new DelegateCommand(ShowList);
            LeftBorderDate = DateTime.Now;
            RightBorderDate = DateTime.Now;
        }
        DoctorRepo repo;

        private ObservableCollection<Patient> _visits;
        public ObservableCollection<Patient> Visits
        {
            get { return _visits; }
            set { SetProperty(ref _visits, value); }
        }

        private DateTime _leftBorderDate;
        public DateTime LeftBorderDate
        {
            get { return _leftBorderDate; }
            set { SetProperty(ref _leftBorderDate, value); }
        }

        private DateTime _rightBorderDate;
        public DateTime RightBorderDate
        {
            get { return _rightBorderDate; }
            set { SetProperty(ref _rightBorderDate, value); }
        }

        private Doctor _selectedDoctor;
        public Doctor SelectedDoctor
        {
            get { return _selectedDoctor; }
            set { SetProperty(ref _selectedDoctor, value); }
        }

        private void ShowList()
        {
            if (LeftBorderDate != null && RightBorderDate != null)
                Visits = new ObservableCollection<Patient>(repo.GetPatientsByTime(SelectedDoctor.Id, LeftBorderDate, RightBorderDate));
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("doctor"))
                SelectedDoctor = navigationContext.Parameters.GetValue<Doctor>("doctor");
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
