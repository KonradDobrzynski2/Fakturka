using Data.AnotherClass;
using Data.ViewModel.Another;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Data.Model
{
    public class Product : BaseViewModel
    {
        public Product()
        { }

        public Product(string Name, string Code, string Category, byte VAT, string UnitOfMeasure,
            double NetBuy, double GrossBuy, double NetSale, double GrossSale, string WhereIsIt)
        {
            this.Name = Name;
            this.Code = Code;
            this.Category = Category;
            this.VAT = VAT;
            this.UnitOfMeasure = UnitOfMeasure;
            this.NetBuy = NetBuy;
            this.GrossBuy = GrossBuy;
            this.NetSale = NetSale;
            this.GrossSale = GrossSale;
            this.WhereIsIt = WhereIsIt;
            Quantity = 0;
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                Set(ref name, value);
            }
        }

    
        private string code;
        [PrimaryKey]
        public string Code
        {
            get { return code; }
            set
            {
                Set(ref code, value);
            }
        }


        public string category;
        public string Category
        {
            get { return category; }
            set
            {
                Set(ref category, value);
            }
        }


        private byte vat;
        public byte VAT
        {
            get { return vat; }
            set
            {
                Set(ref vat, value);
            }
        }


        public string unitOfMeasure;
        public string UnitOfMeasure
        {
            get { return unitOfMeasure; }
            set
            {
                Set(ref unitOfMeasure, value);
            }
        }


        public double netBuy;
        public double NetBuy
        {
            get { return netBuy; }
            set
            {
                Set(ref netBuy, value);
            }
        }


        public double grossBuy;
        public double GrossBuy
        {
            get { return grossBuy; }
            set
            {
                Set(ref grossBuy, value);
            }
        }


        public double netSale;
        public double NetSale
        {
            get { return netSale; }
            set
            {
                Set(ref netSale, value);
            }
        }


        public double grossSale;
        public double GrossSale
        {
            get { return grossSale; }
            set
            {
                Set(ref grossSale, value);
            }
        }


        public string WhereIsIt { get; set; }


        private int quantity;
        public int Quantity
        {
            get { return quantity; }

            set
            {
                if (Strings.IsDigit(value.ToString()))
                {
                    Set(ref quantity, value);
                    //Quantity = 5;
                }
                else
                {
                    Quantity = 1;
                }
            }
        }


        [ManyToMany(typeof(DocumentProductcs))]
        public ObservableCollection<Document> Documents { get; set; }
    }
}
