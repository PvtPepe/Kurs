using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ClinicAppDAL.Models.AuthModel;
using ClinicAppDAL.Repos.AuthRepo;
using ClinicAppDAL.EF;
using System.Windows.Documents;
using System.Collections.ObjectModel;
using System.Security;
using System.Windows;
using Prism.Modularity;
using Prism.Regions;
using System.Security.Principal;
using System.Threading;

namespace AuthorizationModule.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private IModuleManager _moduleManager;
        private IRegionManager _regionManager;
        public IList<User> Users { get; }
        public UserRepo UserRepo { get; }
        public DelegateCommand LoginButtonClick { get; private set; }
        public String Password { private get; set; }

        public LoginViewModel(IModuleManager moduleManager, IRegionManager regionManager)
        {
            UserRepo = new UserRepo(new ClinicAppAuthContext());
            Users = new ObservableCollection<User>(UserRepo.GetAll());
            LoginButtonClick = new DelegateCommand(LoginExecute, LoginCanExecute);
            _moduleManager = moduleManager;
            _regionManager = regionManager;

            StatusBarVisibility = Visibility.Hidden;
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }

        private Visibility _statusBarVisibility;
        public Visibility StatusBarVisibility
        {
            get { return _statusBarVisibility; }
            set { SetProperty(ref _statusBarVisibility, value); }
        }

        private bool LoginCanExecute()
        {
            return true; 
        }

        private void LoginExecute()
        {

            if (UserRepo.CheckPassword(Password) && UserRepo.CheckLogin(Login))
            {
                StatusBarVisibility = Visibility.Hidden;
                _moduleManager.LoadModule("ClinicModule");
            }
            else
            {
                StatusBarVisibility = Visibility.Visible;
                GenericIdentity identity = new GenericIdentity("Admin");
                string[] userRoles = new string[]{ "Admin" };
                GenericPrincipal genericPrincipal = new GenericPrincipal(identity, userRoles);
                Thread.CurrentPrincipal = genericPrincipal;
                IRegion MenuRegion = _regionManager.Regions["MenuRegion"];
                IRegion AuthRegion = _regionManager.Regions["AuthRegion"];
                MenuRegion.RemoveAll();
                AuthRegion.RemoveAll();
                _moduleManager.LoadModule("ClinicModule");
            }
        }

    }
}
