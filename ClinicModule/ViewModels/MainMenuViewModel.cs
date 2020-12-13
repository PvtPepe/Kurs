using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ClinicModule.ViewModels
{
    public class MainMenuViewModel : BindableBase
    {
        public MainMenuViewModel()
        {
            if (Thread.CurrentPrincipal.IsInRole("User")) UsersButtonIsEnabled = false;
            else UsersButtonIsEnabled = true;
        }

        private bool _usersButtonIsEnabled;
        public bool UsersButtonIsEnabled
        {
            get { return _usersButtonIsEnabled; }
            set { SetProperty(ref _usersButtonIsEnabled, value); }
        }
    }
}
