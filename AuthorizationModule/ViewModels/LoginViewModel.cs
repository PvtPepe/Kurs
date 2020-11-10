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

namespace AuthorizationModule.ViewModels
{
    public class LoginViewModel : BindableBase
    {

        public IList<User> Users { get; }
        public UserRepo UserRepo { get; }
        public DelegateCommand LoginButtonClick { get; private set; }
        public String Password { private get; set; }

        public LoginViewModel()
        {
            UserRepo = new UserRepo(new ClinicAppAuthContext());
            Users = new ObservableCollection<User>(UserRepo.GetAll());
            LoginButtonClick = new DelegateCommand(LoginExecute, LoginCanExecute)/*.ObservesProperty(() => Password)*/;

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
            }
            else
            {
                StatusBarVisibility = Visibility.Visible;
            }
        }

    }
}
