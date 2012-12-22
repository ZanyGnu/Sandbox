
namespace WinCoursera.Converters
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Windows.Data;

    public class HtmlToTextConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            // Remove some HTML elements, not complete enough to cover all cases.  
            return HttpUtility.HtmlDecode(Regex.Replace(value.ToString(), "<[^>]+>", string.Empty));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // We dont use TwoWay binding.
            throw new NotImplementedException();
        }
    } 
}
