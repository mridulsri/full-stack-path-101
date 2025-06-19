using System.Windows;

namespace DOMTrace;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        //MainFrame.Navigate(new Views.LoginPage());
    }

    private void LoginView_OnLoginSuccess(object? sender, EventArgs e)
    {
        LoginView.Visibility = Visibility.Collapsed;
        MainTabs.Visibility = Visibility.Visible;
    }

    private void LogoutButton_OnClick(object sender, RoutedEventArgs e)
    {
        MainTabs.Visibility = Visibility.Collapsed;
        LoginView.Visibility = Visibility.Visible;

        // Optionally clear saved credentials on logout
        Settings.Default.SavedUsername = string.Empty;
        Settings.Default.RememberMe = false;
        Settings.Default.Save(); // Save settings

    }
}