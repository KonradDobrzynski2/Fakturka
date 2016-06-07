using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using System;

namespace Data.Model
{
    public class User
    {
        public User()
        { }

        public User(string NameCompany, string FirstName, string LastName, string Type, string Nip, string Regon, string Pesel,
            string PostalCode, string City, string Address, string State, string Country, string Email, string PhoneNumber, string WhereIsIt)
        {
            this.NameCompany = NameCompany;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Type = Type;
            this.Nip = Nip;
            this.Regon = Regon;
            this.Pesel = Pesel;
            this.PostalCode = PostalCode;
            this.City = City;
            this.Address = Address;
            this.State = State;
            this.Country = Country;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.WhereIsIt = WhereIsIt;
        }

        public string NameCompany { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Type { get; set; }

        [PrimaryKey]
        public string Nip { get; set; }

        public string Regon { get; set; }    

        public string Pesel { get; set; }
        
        public string PostalCode { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string WhereIsIt { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Document> DocList_WhereUserIs_Recipient { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Document> DocList_WhereUserIs_Payer { get; set; }

        public static implicit operator User(Product v)
        {
            throw new NotImplementedException();
        }
    }
}
