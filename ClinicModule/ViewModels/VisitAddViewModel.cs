using ClinicAppDAL.Models.ClinicModel;
using ClinicAppDAL.Repos;
using ClinicAppDAL.Repos.ClinicRepo;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;

namespace ClinicModule.ViewModels
{
    public class VisitAddViewModel : BindableBase, IDialogAware
    {
        public DelegateCommand ButtonClick { get; private set; }
        private readonly BaseRepo<Patient> patRepo;
        private readonly BaseRepo<Diagnosis> diagRepo;
        private readonly DoctorRepo docRepo;
        private readonly DoctorVisitRepo visRepo;


        public VisitAddViewModel()
        {
            ButtonClick = new DelegateCommand(Add);
            patRepo = new BaseRepo<Patient>(new ClinicAppDAL.EF.ClinicAppClinicContext());
            diagRepo = new BaseRepo<Diagnosis>(new ClinicAppDAL.EF.ClinicAppClinicContext());
            docRepo = new DoctorRepo(new ClinicAppDAL.EF.ClinicAppClinicContext());
            visRepo = new DoctorVisitRepo(new ClinicAppDAL.EF.ClinicAppClinicContext());
            VisitDate = DateTime.Now;

            Doctors = new ObservableCollection<Doctor>(docRepo.GetAll());
            Patients = new ObservableCollection<Patient>(patRepo.GetAll());
            Diagnoses = new ObservableCollection<Diagnosis>(diagRepo.GetAll());
        }

        private Patient _selectedPat;
        public Patient SelectedPatient
        {
            get { return _selectedPat; }
            set { SetProperty(ref _selectedPat, value); }
        }

        private Diagnosis _selectedDiag;
        public Diagnosis SelectedDiagnosis
        {
            get { return _selectedDiag; }
            set { SetProperty(ref _selectedDiag, value); }
        }

        private Doctor _selectedDoc;
        public Doctor SelectedDoctor
        {
            get { return _selectedDoc; }
            set { SetProperty(ref _selectedDoc, value); }
        }

        private DateTime _visDate;
        public DateTime VisitDate
        {
            get { return _visDate; }
            set { SetProperty(ref _visDate, value); }
        }

        private DoctorVisit DoctorVisit { get; set; }

        private ObservableCollection<Doctor> _docs;
        public ObservableCollection<Doctor> Doctors
        {
            get { return _docs; }
            set { SetProperty(ref _docs, value); }
        }

        private ObservableCollection<Patient> _pats;
        public ObservableCollection<Patient> Patients
        {
            get { return _pats; }
            set { SetProperty(ref _pats, value); }
        }

        private ObservableCollection<Diagnosis> _diag;

        public event Action<IDialogResult> RequestClose;

        public ObservableCollection<Diagnosis> Diagnoses
        {
            get { return _diag; }
            set { SetProperty(ref _diag, value); }
        }

        private void Add()
        {
            if (SelectedDoctor != null && SelectedDiagnosis != null && SelectedPatient != null)
            {
                DoctorVisit p = new DoctorVisit();
                p.DiagnosisID = SelectedDiagnosis.Id;
                p.DoctorID = SelectedDoctor.Id;
                p.PatientID = SelectedPatient.Id;
                p.VisitDate = VisitDate;

                ButtonResult result = ButtonResult.OK;
                if (ButtonName == "add") visRepo.Add(p);
                else 
                {
                    DoctorVisit.DiagnosisID = SelectedDiagnosis.Id;
                    DoctorVisit.DoctorID = SelectedDoctor.Id;
                    DoctorVisit.PatientID = SelectedPatient.Id;
                    DoctorVisit.VisitDate = VisitDate;
                    visRepo.Update(DoctorVisit); 
                }
                
                RequestClose(new DialogResult(result));
            }
        }

        private string buttonName;
        public string ButtonName
        {
            get { return buttonName; }
            set { SetProperty(ref buttonName, value); }
        }

        public string Title => "Add visit";

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("key"))
            {
                ButtonName = "change";
                DoctorVisit = parameters.GetValue<DoctorVisit>("key");
            }
            else
                ButtonName = "add";
        }
    }
}
