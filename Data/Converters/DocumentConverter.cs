using Data.Model;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace Data.Converters
{
    class DocumentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value.ToString() == string.Empty)
            {
                return value;
            }
            else
            {
                User user = new User();

                UserDbRepository userDbRepository = new UserDbRepository(Singletons.Singleton_ConnectionValue.Instance.SQLiteConnection);

                user = userDbRepository.GetUserInDatabase((string)value);

                if (user == null)
                {
                    return "B.Danych ; Numer NIP: " + value.ToString() + " ";
                }
                else
                {
                    return user.NameCompany;
                }
            }   
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
