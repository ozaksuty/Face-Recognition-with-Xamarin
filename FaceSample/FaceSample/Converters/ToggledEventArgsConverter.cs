using System;
using System.Globalization;
using Xamarin.Forms;

namespace FaceSample.Converters
{
    public class ToggledEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ToggledEventArgs eventArgs))
                throw new ArgumentException("Expected ToggledEventArgs as value", "value");

            return eventArgs.Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}