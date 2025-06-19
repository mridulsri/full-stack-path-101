using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        /*
        Properties.Settings.Default.SavedUsername = string.Empty;
        Properties.Settings.Default.RememberMe = false;
        Properties.Settings.Default.Save();
        */
    }
}