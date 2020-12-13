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
            UsersButtonIsEnabled = !Thread.CurrentPrincipal.IsInRole("User");
        }

        private bool _usersButtonIsEnabled;
        public bool UsersButtonIsEnabled
        {
            get { return _usersButtonIsEnabled; }
            set { SetProperty(ref _usersButtonIsEnabled, value); }
        }
    }
}
