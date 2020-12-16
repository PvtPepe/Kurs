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
    public class DiagnosisAddViewModel : BindableBase, IDialogAware
    {
        public DelegateCommand ButtonClick { get; private set; }
        private readonly BaseRepo<Diagnosis> repo;

        private Diagnosis tdiagnosis;
        public Diagnosis TDiagnosis
        {
            get { return tdiagnosis; }
            set { SetProperty(ref tdiagnosis, value); }
        }

        public DiagnosisAddViewModel()
        {
            ButtonClick = new DelegateCommand(Add);
            repo = new BaseRepo<Diagnosis>(new ClinicAppDAL.EF.ClinicAppClinicContext());
            TDiagnosis = new Diagnosis();
        }

        private string buttonName;
        public string ButtonName
        {
            get { return buttonName; }
            set { SetProperty(ref buttonName, value); }
        }

        public string Title => "";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        private void Add()
        {
            if (TDiagnosis.DiagnosisName != null && TDiagnosis.DiagnosisTreatment!=null)
            {
                ButtonResult result = ButtonResult.OK;
                repo.Add(TDiagnosis);
                RequestClose(new DialogResult(result));
            }
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            ButtonName = "Add";
        }
    }
}
