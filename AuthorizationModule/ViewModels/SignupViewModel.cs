using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ClinicAppDAL.Models.AuthModel;
using ClinicAppDAL.Repos.AuthRepo;
using ClinicAppDAL.EF;
using System.Collections.ObjectModel;
using System.Windows;

namespace AuthorizationModule.ViewModels
{
    public class SignupViewModel : BindableBase
    {
        public IList<User> Users { get; }
        public UserRepo UserRepo { get; }
        DelegateCommand SignUpButtonClick;
        public String Password { private get; set; }
        public String RepeatPassword { private get; set; }

        public SignupViewModel()
        {
            UserRepo = new UserRepo(new ClinicAppAuthContext());
            Users = new ObservableCollection<User>(UserRepo.GetAll());

            LoginStatusBarVisibility = Visibility.Hidden;
            PasswordStatusBarVisibility = Visibility.Hidden;
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }

        private Visibility _loginStatusBarVisibility;
        public Visibility LoginStatusBarVisibility
        {
            get { return _loginStatusBarVisibility; }
            set { SetProperty(ref _loginStatusBarVisibility, value); }
        }

        private Visibility _passwordStatusBarVisibility;
        public Visibility PasswordStatusBarVisibility
        {
            get { return _passwordStatusBarVisibility; }
            set { SetProperty(ref _passwordStatusBarVisibility, value); }
        }

    }
}
