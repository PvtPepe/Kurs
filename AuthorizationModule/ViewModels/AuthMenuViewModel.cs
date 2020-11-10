using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorizationModule.ViewModels
{
    public class AuthMenuViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        public DelegateCommand<string> NavigateCommand { get; set; }
        
        public AuthMenuViewModel(IRegionManager regionManager)
        {
            NavigateCommand = new DelegateCommand<string>(Navigate);
            _regionManager = regionManager;
        }

        private void Navigate(string uri)
        {
            _regionManager.RequestNavigate("AuthRegion", uri);
        }
    }
}
