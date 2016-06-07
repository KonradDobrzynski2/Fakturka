using System.IO;
using Windows.Storage;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using Data.Model.AnotherClass;
using Data.AnotherClass;

namespace Data.Services
{
    public class WindowsDatabase : IDatabase
    {
        private SQLiteConnection connection;
        public SQLiteConnection Connection
        {
            get
            {
                if (connection == null)
                {                     
                    var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "gjhfdscvhtrrtrtr.db");
                    connection = new SQLiteConnection(new SQLitePlatformWinRT(), path);

                    ////Odkomentować kiedy tworzymy baze na nowo

                    ////Tworzenie przykłądowych produktó i użytkowników                
                    //SampleDataInDatabase SampleDataInDatabase = new SampleDataInDatabase(connection);
                    //SampleDataInDatabase.CreateProducts();
                    //SampleDataInDatabase.CreateUsers();

                    //// Zerowanie zmiennej przechowującej ID potrzebne do określania nazwy faktór
                    //Settings settings = new Settings();
                    //settings.ResetNextIdDocument();
                }

                return connection;
            }
        }
    }
}
