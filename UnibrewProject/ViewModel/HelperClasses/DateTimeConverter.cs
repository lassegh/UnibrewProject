using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace UnibrewProject.ViewModel.HelperClasses
{
    /// <summary>
    /// Konverterer mellem DateTime og DateTimeOffset
    /// </summary>
    public class DateTimeConverter : IValueConverter
    {
        /// <summary>
        /// Konverterer til DateTimeOffset
        /// </summary>
        /// <param name="value">Den dateTime, der skal konverteres</param>
        /// <returns>DateTimeOffset</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime d = (DateTime)value;

            return new DateTimeOffset(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second, new TimeSpan());

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                return new DateTime(((DateTimeOffset)value).Ticks);
            }

            return null;
        }
    }
}
