using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace Data.Converters
{
    class PriceFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                double price = (double)value;

                if (price == double.MaxValue)
                {
                    return string.Empty;
                }

                return price.ToString("0.00");
            }  
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
