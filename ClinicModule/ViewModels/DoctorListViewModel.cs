using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Regions;

namespace ClinicModule.ViewModels
{
    public class DoctorListViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        public DelegateCommand<string> NavigateCommand { get; set; }

        public DoctorListViewModel(IRegionManager regionManager)
        {
            NavigateCommand = new DelegateCommand<string>(Navigate);
            _regionManager = regionManager;
        }

        private void Navigate(string uri)
        {
            _regionManager.RequestNavigate("DoctorMainRegion", uri);
        }
    }
}
