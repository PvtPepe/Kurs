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
        public DelegateCommand SignupCommand { get; private set; }
        public String Password { private get; set; }
        public String RepeatPassword { private get; set; }

        public SignupViewModel()
        {
            UserRepo = new UserRepo(new ClinicAppAuthContext());
            Users = new ObservableCollection<User>(UserRepo.GetAll());
            SignupCommand = new DelegateCommand(SignUp);
            PasswordStatusBarVisibility = Visibility.Hidden;
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }

        private string errorText;
        public string ErrorText
        {
            get { return errorText; }
            set { SetProperty(ref errorText, value); }
        }

        private Visibility _passwordStatusBarVisibility;
        public Visibility PasswordStatusBarVisibility
        {
            get { return _passwordStatusBarVisibility; }
            set { SetProperty(ref _passwordStatusBarVisibility, value); }
        }

        private void SignUp()
        {
            if (UserRepo.CheckLogin(Login) || Login == null)
            {
                ErrorText = "User is exist";
                PasswordStatusBarVisibility = Visibility.Visible;
            }
            else {
                if (Password != RepeatPassword || Password == null)
                {
                    ErrorText = "Passwords are not equal";
                    PasswordStatusBarVisibility = Visibility.Visible;
                }
                else
                {
                    PasswordStatusBarVisibility = Visibility.Hidden;
                    var p = new User();
                    p.Login = Login;
                    p.Password = Password;
                    p.Role = 0;
                    p.Access = false;
                    UserRepo.Add(p);
                }

            }
        }
    }
}
