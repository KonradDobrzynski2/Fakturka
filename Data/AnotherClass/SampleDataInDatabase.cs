using Data.Repositories;
using SQLite.Net;

namespace Data.Model.AnotherClass
{
    class SampleDataInDatabase : BaseDbRepository
    {
        public SampleDataInDatabase(SQLiteConnection connection) : base(connection)
        {
            DbConnection.CreateTable<User>();
            DbConnection.CreateTable<Product>();
        }

        public void CreateUsers()
        {
            User user1 = new User("Sklep przemysłowo-monopolowy", "Andrzej", "Kowalski", "Dostawca", "5672363287", "", "", "09-145", "Warszawa", "Marszałka 6", "Mazowieckie", "Polska", "Kowalski@wp.pl", "764-346-327", "Database");
            DbConnection.Insert(user1);
            User user2 = new User("Hurtownia rzeczy metalowych", "Marzanna", "Poręcza", "Odbiorca", "7960460321", "794869295", "79405928312", "13-45", "Poznań", "Reymonta 23", "Opolskie", "Polska", "Marzanna154@wp.pl", "654-765-258", "Database");
            DbConnection.Insert(user2);
            User user3 = new User("OBI", "Krystyna", "Lasek", "Dostawca i odbiorca", "6840693821", "", "", "76-235", "Kraków", "Aleja zachodnia 48", "Lubuskie", "Polska", "", "946-33-215", "Database");
            DbConnection.Insert(user3);
            User user4 = new User("Stoisko z E-papierosami NEVA", "Kazik", "Zawodowy", "Dostawca", "9674783491", "", "", "09-145", "Gdańsk", "", "Mazowieckie", "Polska", "Karela@onet.pl", "774-773-795", "Database");
            DbConnection.Insert(user4);
            User user5 = new User("Hurtownia Olsza", "Marek", "Brzeski", "Odbiorca", "1468049541", "", "", "09-145", "Warszawa", "Marszałka 6", "", "Polska", "Alexa@wp.pl", "694-302-239", "Database");
            DbConnection.Insert(user5);
            User user6 = new User("Castorama", "Katarzyna", "Takrys", "Inny", "9803060193", "", "", "54-265", "Warszawa", "", "Mazowieckie", "Polska", "KostaSW@gmail.com", "593-320-033", "Database");
            DbConnection.Insert(user6);

            User user7 = new User("Hurtownia SGT", "Milena", "Łasik", "", "", "", "", "-145", "Warszawa", "Polna 109", "Mazowieckie", "Polska", "", "765-387-289", "Drafts");
            DbConnection.Insert(user7);
            User user8 = new User("Sklep AGD IREK", "Krystyna", "", "Dostawca", "5493849325", "", "", "09-145", "Warszawa", "Marszałka 6", "", "", "IREK_AGD@wp.pl", "", "Drafts");
            DbConnection.Insert(user8);
            User user9 = new User("Zakład mięsny BIZON", "", "", "", "5039673021", "", "", "", "Toruń", "Leśna 145", "", "", "BIZON@wp.pl", "432-543-235", "Drafts");
            DbConnection.Insert(user9);
            User user10 = new User("Hurtownia zabawek IGOR", "Tomasz", "Buła", "Inny", "5403906920", "", "", "09-145", "Warszawa", "Marszałka 6", "Mazowieckie", "Polska", "ZABAWKA_IGOR@wp.pl", "22 743 7654", "Drafts");
            DbConnection.Insert(user10);

            User user11 = new User("Hurtownia plasteliny PASTEL", "Zenon", "KAsicki", "Odbiorca", "4326650295", "504938206", "67049684632", "42-896", "Zawały", "Lutonicza 97", "Dolnośląskie", "Polska", "Lutik_AGS@gmail.com", "439-364-245", "Trash");
            DbConnection.Insert(user11);
            User user12 = new User("Sklep ogrodniczy", "Samanta", "KWaśnielska", "Dostawca", "5948372642", "", "", "", "", "", "", "Polska", "", "22 6503 954", "Trash");
            DbConnection.Insert(user12);
            User user13 = new User("Zakład piwa KASZTEL", "Kasztelan", "Soplica", "Inny", "4932039053", "", "", "09-145", "Warszawa", "Marszałka 32", "Mazowieckie", "Polska", "", "", "Trash");
            DbConnection.Insert(user13);
        }

        public void CreateProducts()
        {
            Product product1 = new Product("Zabawka - konik", "765373463424", "Zabawki", 23, "szt.", 20.45, 25.43, 40.00, 45.55, "Database");
            DbConnection.Insert(product1);
            Product product2 = new Product("Proszek do prania ARIEL", "6456TT45AT4", "Chemia", 7, "kg", 43.33, 57.76, 90.99, 120.00, "Database");
            DbConnection.Insert(product2);
            Product product3 = new Product("Woda mineralna ŻYWIEC", "645764353234", "", 22, "L", 1.45, 1.88, 2.00, 2.30, "Database");
            DbConnection.Insert(product3);
            Product product4 = new Product("Pierścionek srebrny", "KGJOUI6444", "Inne", 0, "szt.", 100.00, 123.00, 150.00, 176.00, "Database");
            DbConnection.Insert(product4);
            Product product5 = new Product("Szynka z Opola", "3457643RFCD4", "Wyroby mięsne", 3, "g", 14.55, 17.54, 20.54, 22.65, "Database");
            DbConnection.Insert(product5);
            Product product6 = new Product("E papieros SPINER", "FGFH5654SS", "Inne", 0, "szt.", 140.00, 166.77, 180.00, 200.00, "Database");
            DbConnection.Insert(product6);


            Product product7 = new Product("Blok kolorowy AGE", "9677643233", "", 5, "szt.", 3.55, 4.00, 4.23, 4.80, "Drafts");
            DbConnection.Insert(product7);
            Product product8 = new Product("Energetyk OSHEE", "PO66634EW3", "Spożywcze", 12, "szt.", 2.44, 2.78, 3.00, 3.25, "Drafts");
            DbConnection.Insert(product8);
            Product product9 = new Product("Ręcznik papierowy SOSNA", "FGFH556FD", "", 23, "szt.", 7.45, 8.00, 8.30, 8.50, "Drafts");
            DbConnection.Insert(product9);
            Product product10 = new Product("Rura metalowa", "FDG45346732", "Artykuły przemysłowe", 23, "", 50.40, 60.00, 70.45, 80.00, "Drafts");
            DbConnection.Insert(product10);

            Product product11 = new Product("Klucz 14", "85663242345", "Artykuły przemysłowe", 0, "szt.", 19.45, 20.00, 25.00, 26.90, "Trash");
            DbConnection.Insert(product11);
            Product product12 = new Product("Naczynie szklane", "78754632", "", 3, "szt.", 50.44, 55.99, 60.00, 75.00, "Trash");
            DbConnection.Insert(product12);
            Product product13 = new Product("Chusteczki papierowe", "SDFD464323", "Chemia", 5, "", 0.30, 0.50, 0.90, 1.23, "Trash");
            DbConnection.Insert(product13);
        }
    }
}
