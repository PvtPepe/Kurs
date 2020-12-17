using ClinicAppDAL.Models.AuthModel;
using ClinicAppDAL.Repos.AuthRepo;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Regions;

namespace ClinicModule.ViewModels
{
    public class UserInfoViewModel : BindableBase, INavigationAware
    {
        public UserInfoViewModel()
        {

        }

        private User selectedUser;
        public User SelectedUser
        {
            get { return selectedUser; }
            set { SetProperty(ref selectedUser, value); }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("user"))
                SelectedUser = navigationContext.Parameters.GetValue<User>("user");
        }
    }
}
