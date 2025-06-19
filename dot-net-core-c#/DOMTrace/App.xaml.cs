using System.Configuration;
using System.Data;
using System.Windows;
using CefSharp.Wpf;

namespace DOMTrace;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        CefSharp.Cef.Initialize(new CefSettings());
    }
}