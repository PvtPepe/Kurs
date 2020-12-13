using ClinicModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ClinicModule
{
    public class ClinicModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ClinicModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegion MainRegion = _regionManager.Regions["MainRegion"];
            IRegion MenuRegion = _regionManager.Regions["MenuRegion"];

            MainMenuView view2 = containerProvider.Resolve<MainMenuView>();
            MenuRegion.Add(view2);
            DoctorListView view3 = containerProvider.Resolve<DoctorListView>();
            MainRegion.Add(view3);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}