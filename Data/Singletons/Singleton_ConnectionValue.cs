using Data.Model;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Singletons
{
    public sealed class Singleton_ConnectionValue
    {
        private static Singleton_ConnectionValue m_oInstance = null;
        private static readonly object lock_object = new object();

        private Singleton_ConnectionValue()
        {
        }

        public static Singleton_ConnectionValue Instance
        {
            get
            {
                lock (lock_object)
                {
                    if (m_oInstance == null)
                    {
                        m_oInstance = new Singleton_ConnectionValue();
                    }

                    return m_oInstance;
                }
                 
            }
        }

        public SQLiteConnection SQLiteConnection;
    }
}
