using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;

namespace Data.AnotherClass
{
    public class Settings
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        public int GetNextIdDocument()
        {
            if (localSettings.Values["NextIdDocument"] == null)
            {
                localSettings.Values["NextIdDocument"] = "1";
            }

            return Int32.Parse(localSettings.Values["NextIdDocument"].ToString());
        }

        public void UpdateNextIdDocument()
        {
            Object value = localSettings.Values["NextIdDocument"];

            value = (Int32.Parse(value.ToString()) + 1).ToString();

            localSettings.Values["NextIdDocument"] = value;
        }

        public void ResetNextIdDocument()
        {
            localSettings.Values["NextIdDocument"] = null;
        }
    }
}
