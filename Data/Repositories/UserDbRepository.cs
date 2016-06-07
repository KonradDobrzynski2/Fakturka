using System.Collections.Generic;
using Data.Model;
using System.Linq;
using SQLite.Net;
using System.Collections.ObjectModel;

namespace Data.Repositories
{
    class UserDbRepository : BaseDbRepository
    {
        public UserDbRepository(SQLiteConnection connection) : base(connection)
        {
            DbConnection.CreateTable<User>();

            #region Providers
            var info = DbConnection.GetTableInfo("Type_addproviders");
            if (!info.Any())
            {
                # region CreateType_addproviders
                DbConnection.CreateTable<Type_addproviders>();
                Type_addproviders Type = new Type_addproviders();

                Type.type = "Dostawca";
                DbConnection.Insert(Type);

                Type.type = "Odbiorca";
                DbConnection.Insert(Type);

                Type.type = "Dostawca i odbiorca";
                DbConnection.Insert(Type);

                Type.type = "Inny";
                DbConnection.Insert(Type);
                #endregion
            }

            info = DbConnection.GetTableInfo("State_addproviders");
            if (!info.Any())
            {
                #region CreateState_addproviders
                DbConnection.CreateTable<State_addproviders>();
                State_addproviders State = new State_addproviders();

                State.state = "Kujawsko-pomorskie";
                DbConnection.Insert(State);

                State.state = "Lubelskie";
                DbConnection.Insert(State);

                State.state = "Małopolskie";
                DbConnection.Insert(State);

                State.state = "Opolskie";
                DbConnection.Insert(State);

                State.state = "Łódzkie";
                DbConnection.Insert(State);

                State.state = "Lubuskie";
                DbConnection.Insert(State);

                State.state = "Mazowieckie";
                DbConnection.Insert(State);

                State.state = "Podkarpackie";
                DbConnection.Insert(State);

                State.state = "Śląskie";
                DbConnection.Insert(State);

                State.state = "Podlaskie";
                DbConnection.Insert(State);

                State.state = "Świętokrzyskie";
                DbConnection.Insert(State);

                State.state = "Warmińsko-mazurskie";
                DbConnection.Insert(State);

                State.state = "Zachodniopomorskie";
                DbConnection.Insert(State);

                State.state = "Wielkopolskie";
                DbConnection.Insert(State);

                State.state = "Dolnośląskie";
                DbConnection.Insert(State);

                State.state = "Nieokreślone";
                DbConnection.Insert(State);

                State.state = "(Inne państwo)";
                DbConnection.Insert(State);
                #endregion
            }

            info = DbConnection.GetTableInfo("Country_addproviders");
            if (!info.Any())
            {
                #region CreateCountry_addproviders
                DbConnection.CreateTable<Country_addproviders>();
                Country_addproviders Country = new Country_addproviders();

                Country.country = "Wielka Brytania";
                DbConnection.Insert(Country);

                Country.country = "Norwegia";
                DbConnection.Insert(Country);

                Country.country = "Szwecja";
                DbConnection.Insert(Country);

                Country.country = "Finlandia";
                DbConnection.Insert(Country);

                Country.country = "Portugalia";
                DbConnection.Insert(Country);

                Country.country = "Hiszpania";
                DbConnection.Insert(Country);

                Country.country = "Francja";
                DbConnection.Insert(Country);

                Country.country = "Holandia";
                DbConnection.Insert(Country);

                Country.country = "Belgia";
                DbConnection.Insert(Country);

                Country.country = "Polska";
                DbConnection.Insert(Country);

                Country.country = "Niemcy";
                DbConnection.Insert(Country);

                Country.country = "Dania";
                DbConnection.Insert(Country);

                Country.country = "Szwajcaria";
                DbConnection.Insert(Country);

                Country.country = "Czechy";
                DbConnection.Insert(Country);

                Country.country = "Włochy";
                DbConnection.Insert(Country);

                Country.country = "Holandia";
                DbConnection.Insert(Country);

                Country.country = "Belgia";
                DbConnection.Insert(Country);

                Country.country = "Słowacja";
                DbConnection.Insert(Country);

                Country.country = "Austria";
                DbConnection.Insert(Country);

                Country.country = "Rosja";
                DbConnection.Insert(Country);

                Country.country = "Ukraina";
                DbConnection.Insert(Country);
                #endregion
            }

            info = DbConnection.GetTableInfo("UserFields_addproviders");
            if (!info.Any())
            {
                # region CreateUserFields_addproviders
                DbConnection.CreateTable<UserFields_addproviders>();
                UserFields_addproviders userFields_addproviders = new UserFields_addproviders();

                userFields_addproviders.userField = "Nazwa kontrahenta";
                DbConnection.Insert(userFields_addproviders);

                userFields_addproviders.userField = "Typ";
                DbConnection.Insert(userFields_addproviders);

                userFields_addproviders.userField = "Nip";
                DbConnection.Insert(userFields_addproviders);

                userFields_addproviders.userField = "Kod pocztowy";
                DbConnection.Insert(userFields_addproviders);

                userFields_addproviders.userField = "Miejscowość";
                DbConnection.Insert(userFields_addproviders);

                userFields_addproviders.userField = "Województwo";
                DbConnection.Insert(userFields_addproviders);

                userFields_addproviders.userField = "Kraj";
                DbConnection.Insert(userFields_addproviders);
                #endregion
            }
            #endregion
        }

        public void Insert(User user)
        {
            lock (databaseLock)
            {              
                DbConnection.Insert(user);
            }
        }

        public void DeleteUserInDatabase(User user)
        {
            var query = "UPDATE User SET WhereIsIt =\"Trash\" WHERE Nip ='" + user.Nip + "'";

            lock (databaseLock)
            {
                DbConnection.Execute(query);
            }
        }

        public void DeleteUserInDrafts(string nip)
        {
            lock (databaseLock)
            {
                DbConnection.Delete<User>(nip);
            }
        }

        public void DeleteUserInTrash(string nip)
        {
            lock (databaseLock)
            {
                DbConnection.Delete<User>(nip);
            }
        }

        public void RestoreUserInTrash(string nip)
        {
            var query = "UPDATE User SET WhereIsIt =\"Database\" WHERE Nip ='" + nip + "'";

            lock (databaseLock)
            {
                DbConnection.Execute(query);
            }
        }

        public ObservableCollection<User> GetAllUsers(bool CompleteUserFields = false)
        {
            ObservableCollection<User> users;

            lock (databaseLock)
            {
                users = new ObservableCollection<User>(DbConnection.Table<User>().ToList());

                if (CompleteUserFields == true)
                {
                    foreach (var item in users)
                    {
                        item.DocList_WhereUserIs_Recipient = DbConnection.Query<Document>("SELECT * FROM Document WHERE Recipient_ID = '" + item.Nip + "'");
                        item.DocList_WhereUserIs_Payer = DbConnection.Query<Document>("SELECT * FROM Document WHERE Payer_ID = '" + item.Nip + "'");
                    }
                }
            }

            return users;
        }

        public ObservableCollection<User> GetAllUsersInDatabase(bool CompleteUserFields = false)
        {
            ObservableCollection<User> users;

            lock (databaseLock)
            {
                users = new ObservableCollection<User>(DbConnection.Table<User>().Where(x => x.WhereIsIt == "Database").ToList());

                if (CompleteUserFields == true)
                {
                    foreach (var item in users)
                    {
                        item.DocList_WhereUserIs_Recipient = DbConnection.Query<Document>("SELECT * FROM Document WHERE Recipient_ID = '" + item.Nip + "'");
                        item.DocList_WhereUserIs_Payer = DbConnection.Query<Document>("SELECT * FROM Document WHERE Payer_ID = '" + item.Nip + "'");
                    }
                }
            }

            return users;
        }

        public ObservableCollection<User> GetAllUsersInDrafts(bool CompleteUserFields = false)
        {
            ObservableCollection<User> users;

            lock (databaseLock)
            {
                users = new ObservableCollection<User>(DbConnection.Table<User>().Where(x => x.WhereIsIt == "Drafts").ToList());

                if (CompleteUserFields == true)
                {
                    foreach (var item in users)
                    {
                        item.DocList_WhereUserIs_Recipient = DbConnection.Query<Document>("SELECT * FROM Document WHERE Recipient_ID = '" + item.Nip + "'");
                        item.DocList_WhereUserIs_Payer = DbConnection.Query<Document>("SELECT * FROM Document WHERE Payer_ID = '" + item.Nip + "'");
                    }
                }                
            }

            return users;
        }

        public ObservableCollection<User> GetAllUsersInTrash(bool CompleteUserFields = false)
        {
            ObservableCollection<User> users;

            lock (databaseLock)
            {
                users = new ObservableCollection<User>(DbConnection.Table<User>().Where(x => x.WhereIsIt == "Trash").ToList());

                if (CompleteUserFields == true)
                {
                    foreach (var item in users)
                    {
                        item.DocList_WhereUserIs_Recipient = DbConnection.Query<Document>("SELECT * FROM Document WHERE Recipient_ID = '" + item.Nip + "'");
                        item.DocList_WhereUserIs_Payer = DbConnection.Query<Document>("SELECT * FROM Document WHERE Payer_ID = '" + item.Nip + "'");
                    }
                }
            }

            return users;
        }

        public User GetUserInDatabase(string nip)
        {
            User user = new User();

            lock (databaseLock)
            {             
                user = DbConnection.Table<User>().Where(x => x.WhereIsIt == "Database" && x.Nip == nip).FirstOrDefault();
            }

            return user;
        }

        public User GetUserInDrafts(string nip)
        {
            User user = new User();

            lock (databaseLock)
            {
                user = DbConnection.Table<User>().Where(x => x.WhereIsIt == "Drafts" && x.Nip == nip).FirstOrDefault();
            }

            return user;
        }

        public User GetUserInTrash(string nip)
        {
            User user = new User();

            lock (databaseLock)
            {
                user = DbConnection.Table<User>().Where(x => x.WhereIsIt == "Trash" && x.Nip == nip).FirstOrDefault();
            }

            return user;
        }


        public bool CheckReproducibility(string nip)
        {
            return DbConnection.Table<User>().Any(x => x.Nip == nip);
        }

        public ObservableCollection<User> SearchUsers(string field, string name, string whereIsIt, bool CompleteUserFields = false)
        {
            ObservableCollection<User> users = new ObservableCollection<User>();

            switch (field)
            {
                case "Nazwa kontrahenta":
                    lock (databaseLock)
                    {
                        users = new ObservableCollection<User>(DbConnection.Table<User>().Where(x => x.WhereIsIt==whereIsIt && x.NameCompany.Contains(name)).ToList());

                        if (CompleteUserFields == true)
                        {
                            foreach (var item in users)
                            {
                                item.DocList_WhereUserIs_Recipient = DbConnection.Query<Document>("SELECT * FROM Document WHERE Recipient_ID = '" + item.Nip + "'");
                                item.DocList_WhereUserIs_Payer = DbConnection.Query<Document>("SELECT * FROM Document WHERE Payer_ID = '" + item.Nip + "'");
                            }
                        }
                    }
                    break;

                case "Typ":
                    lock (databaseLock)
                    {
                        users = new ObservableCollection<User>(DbConnection.Table<User>().Where(x => x.WhereIsIt == whereIsIt && x.Type.Contains(name)).ToList());

                        if (CompleteUserFields == true)
                        {
                            foreach (var item in users)
                            {
                                item.DocList_WhereUserIs_Recipient = DbConnection.Query<Document>("SELECT * FROM Document WHERE Recipient_ID = '" + item.Nip + "'");
                                item.DocList_WhereUserIs_Payer = DbConnection.Query<Document>("SELECT * FROM Document WHERE Payer_ID = '" + item.Nip + "'");
                            }
                        }
                    }
                    break;

                case "Nip":
                    lock (databaseLock)
                    {
                        users = new ObservableCollection<User>(DbConnection.Table<User>().Where(x => x.WhereIsIt == whereIsIt && x.Nip.Contains(name)).ToList());

                        if (CompleteUserFields == true)
                        {
                            foreach (var item in users)
                            {
                                item.DocList_WhereUserIs_Recipient = DbConnection.Query<Document>("SELECT * FROM Document WHERE Recipient_ID = '" + item.Nip + "'");
                                item.DocList_WhereUserIs_Payer = DbConnection.Query<Document>("SELECT * FROM Document WHERE Payer_ID = '" + item.Nip + "'");
                            }
                        }
                    }
                    break;

                case "Kod pocztowy":
                    lock (databaseLock)
                    {
                        users = new ObservableCollection<User>(DbConnection.Table<User>().Where(x => x.WhereIsIt == whereIsIt && x.PostalCode.Contains(name)).ToList());

                        if (CompleteUserFields == true)
                        {
                            foreach (var item in users)
                            {
                                item.DocList_WhereUserIs_Recipient = DbConnection.Query<Document>("SELECT * FROM Document WHERE Recipient_ID = '" + item.Nip + "'");
                                item.DocList_WhereUserIs_Payer = DbConnection.Query<Document>("SELECT * FROM Document WHERE Payer_ID = '" + item.Nip + "'");
                            }
                        }
                    }
                    break;

                case "Miejscowość":
                    lock (databaseLock)
                    {
                        users = new ObservableCollection<User>(DbConnection.Table<User>().Where(x => x.WhereIsIt == whereIsIt && x.City.Contains(name)).ToList());

                        if (CompleteUserFields == true)
                        {
                            foreach (var item in users)
                            {
                                item.DocList_WhereUserIs_Recipient = DbConnection.Query<Document>("SELECT * FROM Document WHERE Recipient_ID = '" + item.Nip + "'");
                                item.DocList_WhereUserIs_Payer = DbConnection.Query<Document>("SELECT * FROM Document WHERE Payer_ID = '" + item.Nip + "'");
                            }
                        }
                    }
                    break;

                case "Województwo":
                    lock (databaseLock)
                    {
                        users = new ObservableCollection<User>(DbConnection.Table<User>().Where(x => x.WhereIsIt == whereIsIt && x.State.Contains(name)).ToList());

                        if (CompleteUserFields == true)
                        {
                            foreach (var item in users)
                            {
                                item.DocList_WhereUserIs_Recipient = DbConnection.Query<Document>("SELECT * FROM Document WHERE Recipient_ID = '" + item.Nip + "'");
                                item.DocList_WhereUserIs_Payer = DbConnection.Query<Document>("SELECT * FROM Document WHERE Payer_ID = '" + item.Nip + "'");
                            }
                        }
                    }
                    break;

                case "Kraj":
                    lock (databaseLock)
                    {
                        users = new ObservableCollection<User>(DbConnection.Table<User>().Where(x => x.WhereIsIt == whereIsIt && x.Country.Contains(name)).ToList());

                        if (CompleteUserFields == true)
                        {
                            foreach (var item in users)
                            {
                                item.DocList_WhereUserIs_Recipient = DbConnection.Query<Document>("SELECT * FROM Document WHERE Recipient_ID = '" + item.Nip + "'");
                                item.DocList_WhereUserIs_Payer = DbConnection.Query<Document>("SELECT * FROM Document WHERE Payer_ID = '" + item.Nip + "'");
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

            return users;
        }


        public ObservableCollection<string> TypeColectionMethod()
        {
            ObservableCollection<string> type = new ObservableCollection<string>(DbConnection.Table<Type_addproviders>().Select(x => x.type).ToList());

            return type;
        }

        public ObservableCollection<string> StateColectionMethod()
        {
            ObservableCollection<string> state = new ObservableCollection<string>(DbConnection.Table<State_addproviders>().Select(x => x.state).ToList());

            return state;
        }

        public ObservableCollection<string> CountryColectionMethod()
        {
            ObservableCollection<string> country = new ObservableCollection<string>(DbConnection.Table<Country_addproviders>().Select(x => x.country).ToList());

            return country;
        }

        public ObservableCollection<string> UserFieldsColectionMethod()
        {
            ObservableCollection<string> userFields = new ObservableCollection<string>(DbConnection.Table<UserFields_addproviders>().Select(x => x.userField).ToList());

            return userFields;
        }
    }
}
