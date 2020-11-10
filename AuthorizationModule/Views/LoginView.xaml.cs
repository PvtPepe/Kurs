using System.Windows.Controls;

namespace AuthorizationModule.Views
{
    /// <summary>
    /// Interaction logic for AuthView
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void AuthLoginPassBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if(this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password;
            }
        }
    }
}
