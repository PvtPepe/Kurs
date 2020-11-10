using ClinicAppDAL.Models.AuthModel;
using Prism.Mvvm;

namespace ClinicApp.ViewModels
{
    public class ShellWindowViewModel : BindableBase
    {
        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            set => SetProperty(ref _currentUser, value);
        }

        public ShellWindowViewModel()
        {

        }
    }
}
