using System;
using System.Collections.Generic;
using System.Text;
using SQLite.Net;

namespace Data.Services
{
    public interface IDatabase
    {
        SQLiteConnection Connection { get; }
    }
}
