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
    public class ClinicNotificationViewModel : BindableBase, IDialogAware
    {
        public DelegateCommand YesCommand { get; private set; }
        public DelegateCommand NoCommand { get; private set; }

        public ClinicNotificationViewModel()
        {
            YesCommand = new DelegateCommand(Yes);
            NoCommand = new DelegateCommand(No);
        }

        public string Title => "Notification";

        public event Action<IDialogResult> RequestClose;

        public void Yes()
        {
            ButtonResult result = ButtonResult.Yes;
            RequestClose(new DialogResult(result));
        }

        public void No()
        {
            ButtonResult result = ButtonResult.No;
            RequestClose(new DialogResult(result));
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
