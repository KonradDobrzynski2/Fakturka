using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class Document
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string TypeDocument { get; set; }

        public string KindOfPrices { get; set; }

        public string NameDocument { get; set; }

        public string PaymentType { get; set; }

        public string AccountNumber { get; set; }

        public string DateOfPayment { get; set; }

        public string DateOfSale { get; set; }

        public string WhereIsIt { get; set; }

        [ForeignKey(typeof(User))]
        public string Recipient_ID { get; set; }

        [ForeignKey(typeof(User))]
        public string Payer_ID { get; set; }

        [ManyToMany(typeof(DocumentProductcs))]
        public ObservableCollection<Product> Products { get; set; }
    }
}
