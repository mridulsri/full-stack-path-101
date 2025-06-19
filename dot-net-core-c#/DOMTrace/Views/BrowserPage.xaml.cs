using System.Windows;
using System.Windows.Controls;

namespace DOMTrace.Views;

public partial class BrowserPage : Page
{
    public BrowserPage()
    {
        InitializeComponent();
    }

    private void GoButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(AddressBar.Text))
        {
            Browser.Address = AddressBar.Text;
        }
    }
}