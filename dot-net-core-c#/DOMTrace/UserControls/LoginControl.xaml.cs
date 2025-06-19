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
        UsernameTextBox.Text = Settings.Default.SavedUsername;
        RememberMeCheckBox.IsChecked = Settings.Default.RememberMe;
    }

    private void LoginButton_OnClick(object sender, RoutedEventArgs e)
    {
        string username = UsernameTextBox.Text;
        string password = PasswordBox.Password;

        if (username == "admin" && password == "admin")
        {
            if (RememberMeCheckBox.IsChecked == true)
            {
                Settings.Default.SavedUsername = username;
                Settings.Default.RememberMe = true;
            }
            else
            {
                Settings.Default.SavedUsername = string.Empty;
                Settings.Default.RememberMe = false;
            }

            Settings.Default.Save(); // Save settings

            LoginSuccess?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            ErrorText.Visibility = Visibility.Visible;
        }
    }
}