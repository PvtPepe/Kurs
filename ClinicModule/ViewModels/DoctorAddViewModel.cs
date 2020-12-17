using ClinicAppDAL.Models.ClinicModel;
using ClinicAppDAL.Repos.ClinicRepo;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicModule.ViewModels
{
    public class DoctorAddViewModel : BindableBase, IDialogAware
    {
        public DelegateCommand ButtonClick { get; private set; }
        private readonly DoctorRepo repo;

        public DoctorAddViewModel()
        {
            ButtonClick = new DelegateCommand(Add);
            repo = new DoctorRepo(new ClinicAppDAL.EF.ClinicAppClinicContext());
            SelectedDoctor = new Doctor();
        }

        private Doctor _selectedDoctor;
        public Doctor SelectedDoctor
        {
            get { return _selectedDoctor; }
            set { SetProperty(ref _selectedDoctor, value); }
        }

        public string Title => "Add doctor";

        public event Action<IDialogResult> RequestClose;

        private void Add()
        {
            if (SelectedDoctor.FirstName != null && SelectedDoctor.Speciality != null 
                && SelectedDoctor.LastName != null && SelectedDoctor.LengthOfService != null && SelectedDoctor.DocNumber!=null)
            {
                if (SelectedDoctor.MidName == null) SelectedDoctor.MidName = " ";

                ButtonResult result = ButtonResult.OK;
                repo.Add(SelectedDoctor);
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
