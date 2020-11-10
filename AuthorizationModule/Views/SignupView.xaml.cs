using System.Windows.Controls;

namespace AuthorizationModule.Views
{
    /// <summary>
    /// Interaction logic for SignupView
    /// </summary>
    public partial class SignupView : UserControl
    {
        public SignupView()
        {
            InitializeComponent();
        }

        private void AuthSignupPassBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password;
            }
        }

        private void AuthSignupRepPassBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).RepeatPassword = ((PasswordBox)sender).Password;
            }
        }
    }
}
