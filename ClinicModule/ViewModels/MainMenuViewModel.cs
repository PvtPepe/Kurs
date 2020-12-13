using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Prism.Regions;

namespace ClinicModule.ViewModels
{
    public class MainMenuViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        public DelegateCommand<string> NavigateCommand { get; set; }

        public MainMenuViewModel(IRegionManager regionManager)
        {
            UsersButtonIsEnabled = !Thread.CurrentPrincipal.IsInRole("User");
            NavigateCommand = new DelegateCommand<string>(Navigate);
            _regionManager = regionManager;
        }

        private bool _usersButtonIsEnabled;
        public bool UsersButtonIsEnabled
        {
            get { return _usersButtonIsEnabled; }
            set { SetProperty(ref _usersButtonIsEnabled, value); }
        }

        private void Navigate(string uri)
        {
            _regionManager.RequestNavigate("MainRegion", uri);
        }
    }
}
