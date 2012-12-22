using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinCoursera.Converters
{
    public class SyndicationItemTextToTrimmedTextConverter: HtmlToTextConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int maxLength = 200;
            string htmlConvertedText = (string) base.Convert(value, targetType, parameter, culture);

            if (htmlConvertedText != null && htmlConvertedText.Length >= maxLength)
            {
                htmlConvertedText = htmlConvertedText.Substring(0, maxLength) + "... ";
            }

            return htmlConvertedText;
        }
    }
}
