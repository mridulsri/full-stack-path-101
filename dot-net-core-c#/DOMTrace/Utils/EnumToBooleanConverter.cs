using System.Globalization;
using System.Windows.Data;

namespace DOMTrace.Utils
{
    public class EnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value?.ToString() == parameter?.ToString();

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => (bool)value ? Enum.Parse(targetType, parameter.ToString()) : Binding.DoNothing;
    }
}
