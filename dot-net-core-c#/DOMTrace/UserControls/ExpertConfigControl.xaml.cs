using System.Windows.Controls;

namespace DOMTrace.UserControls
{
    /// <summary>
    /// Interaction logic for ExpertConfigControl.xaml
    /// </summary>
    public partial class ExpertConfigControl : UserControl
    {
        public ExpertConfigControl()
        {
            InitializeComponent();
            DataContext = new BrowserControlViewModel();
        }
    }
}
