using System.Collections.Generic;
using Data.Model;
using System.Linq;
using SQLite.Net;
using System.Collections.ObjectModel;

namespace Data.Repositories
{
    class DocumentProductcsDbRepository : BaseDbRepository
    {
        public DocumentProductcsDbRepository(SQLiteConnection connection) : base(connection)
        {
            DbConnection.CreateTable<DocumentProductcs>();
        }

        public void Insert(DocumentProductcs documentProductcs)
        {
            lock (databaseLock)
            {              
                DbConnection.Insert(documentProductcs);
            }
        }

        public void InsertList(int ID_Document, ObservableCollection<Product> listProducts)
        {
            lock (databaseLock)
            {
                DocumentProductcs DocumentProductcs = new DocumentProductcs() { ID_Document = ID_Document };

                foreach (var item in listProducts)
                {
                    DocumentProductcs.Code = item.Code;
                    DocumentProductcs.Quantity = item.Quantity;
                    DbConnection.Insert(DocumentProductcs);
                }
            }
        }

        public ObservableCollection<Product> GetProductsListInDocument(int ID_Document)
        {
            lock (databaseLock)
            {
                ObservableCollection<Product> ProductList = new ObservableCollection<Product>();

                ObservableCollection<DocumentProductcs> DocumentProductcs;

                Product product = new Product();

                DocumentProductcs = new ObservableCollection<DocumentProductcs>(DbConnection.Table<DocumentProductcs>().Where(x => x.ID_Document == ID_Document).ToList());

                foreach (var item in DocumentProductcs)
                {
                    product = DbConnection.Table<Product>().Where(x => x.Code == item.Code).FirstOrDefault();

                    if (product != null)
                    {
                        product.Quantity = item.Quantity;
                    }
                    else
                    {
                        product = new Product();

                        product.Name = "Produkt nie istnieje już w bazie";
                        product.Category = "-- --";
                        product.Code = item.Code;
                        product.GrossBuy = double.MaxValue;
                        product.GrossSale = double.MaxValue;
                        product.NetBuy = double.MaxValue;
                        product.NetSale = double.MaxValue;
                        product.Quantity = 0;
                        product.VAT = byte.MaxValue;
                    }

                    ProductList.Add(product);
                }

                return ProductList;
            }
        }

        public void DeleteProductsListDocument(int ID)
        {
            lock (databaseLock)
            {
                DbConnection.Table<DocumentProductcs>().Delete(x => x.ID_Document == ID);
            }
        }

        public void RenameCodeInDocuments(string OldCode, string NewCode)
        {
            lock (databaseLock)
            {
                var query = "UPDATE DocumentProductcs SET Code ='" + NewCode + "' WHERE Code = '" + OldCode + "'";

                DbConnection.Execute(query);
            }
        }
    }
}
