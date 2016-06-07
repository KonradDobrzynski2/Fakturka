using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model
{
    public class Category_addproducts
    {
        [PrimaryKey]
        public string category { get; set; }
    }
}
