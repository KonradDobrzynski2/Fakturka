using System;
using System.Collections.Generic;
using System.Text;
using SQLite.Net;

namespace Data.Repositories
{
    class BaseDbRepository
    {
        protected static object databaseLock = new object();

        protected SQLiteConnection DbConnection { get; }

        public BaseDbRepository(SQLiteConnection connection)
        {
            DbConnection = connection;
        }
    }
}
