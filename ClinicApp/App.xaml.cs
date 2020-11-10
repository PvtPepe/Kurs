using Prism.Ioc;
using ClinicApp.Views;
using System.Windows;
using Prism.Modularity;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;

namespace ClinicApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }
    }
}
