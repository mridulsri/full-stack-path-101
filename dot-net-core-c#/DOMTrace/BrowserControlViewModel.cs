using DOMTrace.Core;
using DOMTrace.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DOMTrace;

public enum ExpertType { Container, Field }

public class BrowserControlViewModel : INotifyPropertyChanged
{
    public ObservableCollection<ComboBoxOption> ExpertNames { get; set; }
    public ObservableCollection<ComboBoxOption> Regex { get; set; }

    private ComboBoxOption _selectedExpertName;
    public ComboBoxOption SelectedExpertName
    {
        get => _selectedExpertName;
        set { _selectedExpertName = value; OnPropertyChanged(); }
    }

    private ComboBoxOption _selectedRegex;
    public ComboBoxOption SelectedRegex
    {
        get => _selectedRegex;
        set { _selectedRegex = value; OnPropertyChanged(); }
    }

    private ExpertType _selectedExpertType;
    public ExpertType SelectedExpertType
    {
        get => _selectedExpertType;
        set { _selectedExpertType = value; OnPropertyChanged(); }
    }


    public BrowserControlViewModel()
    {
        ExpertNames = StaticDataContext.ExpertContainerOptionCollection;
        Regex = StaticDataContext.RegexCollection;
    }


    protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public event PropertyChangedEventHandler? PropertyChanged;
}
