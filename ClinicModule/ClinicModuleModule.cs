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
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}