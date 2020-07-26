using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace InfoCardsAppClient
{
    class BooleanToVisibilityConverter : IValueConverter
    {
        public BooleanToVisibilityConverter() { }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                throw new ArgumentException("Value type is not bool");
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Visibility))
                throw new ArgumentException("Value type is not Visibility");
            return (Visibility)value == Visibility.Visible ? true : false;
        }
    }
}
