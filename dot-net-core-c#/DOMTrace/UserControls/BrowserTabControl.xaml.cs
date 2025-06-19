using CefSharp;
using DOMTrace.HelperUtlis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DOMTrace.UserControls;

public partial class BrowserTabControl : UserControl
{
    public BrowserTabControl()
    {
        InitializeComponent();

        // Register JS bridge (already done earlier)
        Browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
        Browser.JavascriptObjectRepository.Register("bound", new JsBridge(this), options: BindingOptions.DefaultBinder);
        Browser.FrameLoadEnd += Browser_FrameLoadEnd;

        // Update Address bar
        Browser.AddressChanged += (s, args) =>
        {
            if (args.NewValue == null) return;
            Dispatcher.Invoke(() => AddressBar.Text = args.NewValue.ToString() ?? string.Empty);
        };

        // Update buttons when navigation state changes
        Browser.LoadingStateChanged += (s, e) =>
        {
            Dispatcher.Invoke(() =>
            {
                BackButton.IsEnabled = e.CanGoBack;
                ForwardButton.IsEnabled = e.CanGoForward;

                LoadingProgressBar.Visibility = e.IsLoading ? Visibility.Visible : Visibility.Collapsed;
            });
        };

        // set shortcuts
        this.PreviewKeyDown += BrowserTabControl_PreviewKeyDown;
    }
    private void DevTools_Click(object sender, RoutedEventArgs e)
    {
        Browser.ShowDevTools();
    }
    private void Back_Click(object sender, RoutedEventArgs e)
    {
        if (Browser.CanGoBack)
        {
            Browser.Back();
        }
    }

    private void Forward_Click(object sender, RoutedEventArgs e)
    {
        if (Browser.CanGoForward)
        {
            Browser.Forward();
        }
    }
    private void Go_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(AddressBar.Text))
        {
            Browser.Load(AddressBar.Text);
        }
    }

    private async void Browser_FrameLoadEnd(object? sender, FrameLoadEndEventArgs e)
    {
        if (!e.Frame.IsMain) return;
        string script = ResourceHelper.ReadEmbeddedScript("DOMTrace.Scripts.highlight-tracker.js");
        e.Frame.ExecuteJavaScriptAsync(script);

        // Load Favicon
        if (e.Frame.IsMain)
        {
            await Dispatcher.InvokeAsync(() =>
            {
                try
                {
                    var uri = new Uri(Browser.Address);
                    var faviconUrl = $"{uri.Scheme}://{uri.Host}/favicon.ico";
                    Favicon.Source = new BitmapImage(new Uri(faviconUrl));
                }
                catch
                {
                    Favicon.Source = null;
                }
            });
        }

    }

    public void DisplayXPath(string xpath)
    {
        Dispatcher.Invoke(() =>
        {
            XPathOutput.Text = xpath;
        });
    }

    private void BrowserTabControl_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (Keyboard.Modifiers == ModifierKeys.Alt)
        {
            if (e.Key == Key.Left && Browser.CanGoBack)
            {
                Browser.Back();
                e.Handled = true;
            }
            else if (e.Key == Key.Right && Browser.CanGoForward)
            {
                Browser.Forward();
                e.Handled = true;
            }
        }
        else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Enter)
        {
            Go_Click(null, null);
            e.Handled = true;
        }
        else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.I)
        {
            Browser.ShowDevTools();
            e.Handled = true;
        }
    }


}


public class JsBridge
{
    private readonly BrowserTabControl _parent;

    public JsBridge(BrowserTabControl parent)
    {
        _parent = parent;
    }

    public void ReportXPath(string xpath)
    {
        _parent.DisplayXPath(xpath);
    }
}