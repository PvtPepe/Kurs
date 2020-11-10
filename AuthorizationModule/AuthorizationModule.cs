using AuthorizationModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace AuthorizationModule
{
    public class AuthorizationModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public AuthorizationModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegion region = _regionManager.Regions["AuthRegion"];
            IRegion MenuRegion = _regionManager.Regions["MenuRegion"];

            /*GenericIdentity identity = new GenericIdentity("User");
            string[] userRoles = new string[]{ "Admin", "User" };
            GenericPrincipal genericPrincipal = new GenericPrincipal(identity, userRoles);
            Thread.CurrentPrincipal = genericPrincipal;*/

            LoginView view1 = containerProvider.Resolve<LoginView>();
            region.Add(view1);

            AuthMenuView view2 = containerProvider.Resolve<AuthMenuView>();
            MenuRegion.Add(view2);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LoginView>();
            containerRegistry.RegisterForNavigation<SignupView>();
        }
    }
}