using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Urlaubsplaner.Util
{
    // Used to be able to show an empty date field if datetime is null
    // taken from https://stackoverflow.com/questions/15696752/c-sharp-wpf-datagrid-converters
    [ValueConversion(typeof(DateTime), typeof(String))]
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            if(date == DateTime.MinValue)
            {
                return "";
            }
            return date;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            DateTime resultDateTime;
            if (DateTime.TryParse(strValue, out resultDateTime))
            {
                return resultDateTime;
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
