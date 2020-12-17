using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ClinicAppDAL.Models.AuthModel;
using ClinicAppDAL.Repos.AuthRepo;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace ClinicModule.ViewModels
{
    public class UsersListViewModel : BindableBase
    {

        private UserRepo repo;
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        public DelegateCommand<User> SelectedCommand { get; private set; }
        public DelegateCommand ApproveCommand { get; private set; }
        public DelegateCommand MakeAdminCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }
        public DelegateCommand SearchCommand { get; private set; }

        public UsersListViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
            repo = new UserRepo(new ClinicAppDAL.EF.ClinicAppAuthContext());
            Users = new ObservableCollection<User>(repo.GetAll());
            SelectedCommand = new DelegateCommand<User>(Selected);
            DeleteCommand = new DelegateCommand(Delete, canDelete).ObservesProperty(() => SelectedUser);
            ApproveCommand = new DelegateCommand(Approve,canDelete).ObservesProperty(() => SelectedUser);
            SearchCommand = new DelegateCommand(Search);
            MakeAdminCommand = new DelegateCommand(MakeAdmin,canDelete).ObservesProperty(() => SelectedUser);
        }

        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get { return users; }
            set { SetProperty(ref users, value); }
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set { SetProperty(ref _selectedUser, value); }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        private void Delete()
        {
            _dialogService.ShowDialog("ClinicNotificationView", r =>
            {
                if (r.Result == ButtonResult.Yes)
                {
                    repo.Delete(SelectedUser);
                    Users = new ObservableCollection<User>(repo.GetAll());
                }
            });
            
        }

        private bool canDelete()
        {
            return SelectedUser != null;
        }

        private void Selected(User obj)
        {
            var p = new NavigationParameters();
            p.Add("user", obj);
            _regionManager.RequestNavigate("UserMainRegion", "UserInfoView", p);
        }

        private void Search()
        {
            Users = new ObservableCollection<User>(repo.GetSome(x => x.Login.StartsWith(SearchText)));
        }

        private void Approve()
        {
            repo.Approve(SelectedUser);
            _regionManager.Regions["UserMainRegion"].RemoveAll();
            Selected(SelectedUser);
        }

        private void MakeAdmin()
        {
            repo.MakeAdmin(SelectedUser);
            _regionManager.Regions["UserMainRegion"].RemoveAll();
            Selected(SelectedUser);
        }
    }
}
