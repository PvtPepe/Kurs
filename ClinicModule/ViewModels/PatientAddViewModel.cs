using ClinicAppDAL.Models.ClinicModel;
using ClinicAppDAL.Repos;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicModule.ViewModels
{
    public class PatientAddViewModel : BindableBase, IDialogAware
    {
        public DelegateCommand ButtonClick { get; private set; }
        private readonly BaseRepo<Patient> repo;

        public PatientAddViewModel()
        {
            ButtonClick = new DelegateCommand(Add);
            repo = new BaseRepo<Patient>(new ClinicAppDAL.EF.ClinicAppClinicContext());
            SelectedPatient = new Patient();
            SelectedPatient.PatientBirthdate = DateTime.Now;
        }

        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set { SetProperty(ref _selectedPatient, value); }
        }

        public string Title => "Add patient";

        public event Action<IDialogResult> RequestClose;

        private void Add()
        {
            if (SelectedPatient.FirstName != null && SelectedPatient.PatientAdress != null
                && SelectedPatient.LastName != null && SelectedPatient.PatientNumber != null && SelectedPatient.PatientNumber!=null)
            {
                if (SelectedPatient.MidName == null) SelectedPatient.MidName = " ";

                ButtonResult result = ButtonResult.OK;
                repo.Add(SelectedPatient);
                RequestClose(new DialogResult(result));
            }
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}
