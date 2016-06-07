using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace Data.Converters
{
    class VatFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            byte v = (byte)value;

            if (v == byte.MaxValue)
            {
                return "- %";
            }
            else
            {
                return v.ToString() + " %";
            }  
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
