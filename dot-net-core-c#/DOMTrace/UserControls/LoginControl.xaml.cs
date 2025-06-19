using System.Windows;
using System.Windows.Controls;

namespace DOMTrace.UserControls;

public partial class LoginControl : UserControl
{
    public event EventHandler? LoginSuccess;
    
    public LoginControl()
    {
        InitializeComponent();
        
        // Load saved credentials
        /*
        UsernameTextBox.Text = Properties.Settings.Default.SavedUsername;
        RememberMeCheckBox.IsChecked = Properties.Settings.Default.RememberMe;
        */
    }
    
    private void LoginButton_OnClick(object sender, RoutedEventArgs e)
    {
        string username = UsernameTextBox.Text;
        string password = PasswordBox.Password;

        if (username == "admin" && password == "Password@123")
        {
            /* Need to implement using Visual studio
            if (RememberMeCheckBox.IsChecked == true)
            {
                Properties.Settings.Default.SavedUsername = username;
                Properties.Settings.Default.RememberMe = true;
            }
            else
            {
                Properties.Settings.Default.SavedUsername = string.Empty;
                Properties.Settings.Default.RememberMe = false;
            }
            
            Properties.Settings.Default.Save();
            */
            LoginSuccess?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            ErrorText.Visibility = Visibility.Visible;
        }
    }
}