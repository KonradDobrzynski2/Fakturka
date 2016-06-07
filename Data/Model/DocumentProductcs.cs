using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model
{
    public class DocumentProductcs
    {
        [ForeignKey(typeof(Document))]
        public int ID_Document { get; set; }

        [ForeignKey(typeof(Product))]
        public string Code { get; set; }

        public int Quantity { get; set; }
    }
}
