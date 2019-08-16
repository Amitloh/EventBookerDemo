using System;
using System.Globalization;
using MvvmCross.Converters;

namespace EventBookerAndroid.Core.Converters
{
    public class DateTimeToTimeConverter : MvxValueConverter<DateTime, string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return value.ToString("hh:mm tt", CultureInfo.InvariantCulture);
        }
    }
}
