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
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<DoctorListView>();
            containerRegistry.RegisterForNavigation<PatientsListView>();
            containerRegistry.RegisterForNavigation<DiagnosesListView>();
            containerRegistry.RegisterForNavigation<VisitsListView>();
            containerRegistry.RegisterForNavigation<UsersListView>();
            containerRegistry.RegisterForNavigation<DoctorInfoView>();
        }
    }
}