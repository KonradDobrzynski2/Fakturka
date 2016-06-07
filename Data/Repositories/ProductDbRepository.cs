using Data.Model;
using SQLite.Net;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;

namespace Data.Repositories
{
    class ProductDbRepository : BaseDbRepository
    {
        public ProductDbRepository(SQLiteConnection connection) : base(connection)
        {
            DbConnection.CreateTable<Product>();
            DbConnection.CreateTable<DocumentProductcs>();

            #region Products
            var info = DbConnection.GetTableInfo("Category_addproducts");
            if (!info.Any())
            {
                DbConnection.CreateTable<Category_addproducts>();

                Category_addproducts Category_addproducts = new Category_addproducts();
                Category_addproducts.category = "Artykuły przemysłowe";
                DbConnection.Insert(Category_addproducts);

                Category_addproducts.category = "Spożywcze";
                DbConnection.Insert(Category_addproducts);

                Category_addproducts.category = "Zabawki";
                DbConnection.Insert(Category_addproducts);

                Category_addproducts.category = "Wyroby mięsne";
                DbConnection.Insert(Category_addproducts);

                Category_addproducts.category = "Artykuły szklane";
                DbConnection.Insert(Category_addproducts);

                Category_addproducts.category = "Inne";
                DbConnection.Insert(Category_addproducts);
            }

            info = DbConnection.GetTableInfo("UnitOfMeasure_addproducts");
            if (!info.Any())
            {
                # region CreateUnitOfMeasure_addproducts
                DbConnection.CreateTable<UnitOfMeasure_addproducts>();
                UnitOfMeasure_addproducts UnitOfMeasure = new UnitOfMeasure_addproducts();

                UnitOfMeasure.unitOfMeasure = "[m]";
                DbConnection.Insert(UnitOfMeasure);

                UnitOfMeasure.unitOfMeasure = "[m3]";
                DbConnection.Insert(UnitOfMeasure);

                UnitOfMeasure.unitOfMeasure = "['']";
                DbConnection.Insert(UnitOfMeasure);

                UnitOfMeasure.unitOfMeasure = "[L]";
                DbConnection.Insert(UnitOfMeasure);

                UnitOfMeasure.unitOfMeasure = "[cm]";
                DbConnection.Insert(UnitOfMeasure);

                UnitOfMeasure.unitOfMeasure = "[mg]";
                DbConnection.Insert(UnitOfMeasure);

                UnitOfMeasure.unitOfMeasure = "[g]";
                DbConnection.Insert(UnitOfMeasure);

                UnitOfMeasure.unitOfMeasure = "[kg]";
                DbConnection.Insert(UnitOfMeasure);

                UnitOfMeasure.unitOfMeasure = "[t]";
                DbConnection.Insert(UnitOfMeasure);

                UnitOfMeasure.unitOfMeasure = "[szt.]";
                DbConnection.Insert(UnitOfMeasure);

                UnitOfMeasure.unitOfMeasure = "[j.]";
                DbConnection.Insert(UnitOfMeasure);
                #endregion
            }

            info = DbConnection.GetTableInfo("VAT_addproductscs");
            if (!info.Any())
            {
                # region CreateVAT_addproductscs
                DbConnection.CreateTable<VAT_addproductscs>();
                VAT_addproductscs VAT_addproductscs = new VAT_addproductscs();

                VAT_addproductscs.vat = 23;
                DbConnection.Insert(VAT_addproductscs);

                VAT_addproductscs.vat = 22;
                DbConnection.Insert(VAT_addproductscs);

                VAT_addproductscs.vat = 12;
                DbConnection.Insert(VAT_addproductscs);

                VAT_addproductscs.vat = 8;
                DbConnection.Insert(VAT_addproductscs);

                VAT_addproductscs.vat = 7;
                DbConnection.Insert(VAT_addproductscs);

                VAT_addproductscs.vat = 5;
                DbConnection.Insert(VAT_addproductscs);

                VAT_addproductscs.vat = 3;
                DbConnection.Insert(VAT_addproductscs);

                VAT_addproductscs.vat = 0;
                DbConnection.Insert(VAT_addproductscs);
                #endregion
            }
            info = DbConnection.GetTableInfo("ProductFields_addproducts");
            if (!info.Any())
            {
                DbConnection.CreateTable<ProductFields_addproducts>();

                ProductFields_addproducts productFields_addproducts = new ProductFields_addproducts();
                productFields_addproducts.productField = "Nazwa";
                DbConnection.Insert(productFields_addproducts);

                productFields_addproducts.productField = "Kod";
                DbConnection.Insert(productFields_addproducts);

                productFields_addproducts.productField = "Kategoria";
                DbConnection.Insert(productFields_addproducts);

                productFields_addproducts.productField = "Stawka VAT";
                DbConnection.Insert(productFields_addproducts);

                productFields_addproducts.productField = "J. miary";
                DbConnection.Insert(productFields_addproducts);
            }
            #endregion
        }

        public void Insert(Product product)
        {
            lock (databaseLock)
            {
                DbConnection.Insert(product);
            }
        }

        public void InsertCategory(Category_addproducts categoryAddProducts)
        {
            lock (databaseLock)
            {
                DbConnection.Insert(categoryAddProducts);
            }
        }

        public void DeleteCategory(string nameCategory)
        {
            lock (databaseLock)
            {
                DbConnection.Delete<Category_addproducts>(nameCategory);
            }
        }

        public void DeleteProductInDatabase(string code)
        {
            var query = "UPDATE Product SET WhereIsIt =\"Trash\" WHERE Code ='" + code + "'";

            lock (databaseLock)
            {
                DbConnection.Execute(query);
            }
        }

        public void DeleteProductInDrafts(string code)
        {
            lock (databaseLock)
            {
                DbConnection.Delete<Product>(code);
            }
        }

        public void DeleteProductInTrash(string code)
        {
            lock (databaseLock)
            {
                DbConnection.Delete<Product>(code);
            }
        }

        public void RestoreUserInTrash(string code)
        {
            var query = "UPDATE Product SET WhereIsIt =\"Database\" WHERE Code ='" + code + "'";

            lock (databaseLock)
            {
                DbConnection.Execute(query);
            }
        }

        public ObservableCollection<Product> GetAllProducts()
        {
            ObservableCollection<Product> product;

            lock (databaseLock)
            {
                product = new ObservableCollection<Product>(DbConnection.Table<Product>().ToList());
            }

            return product;
        }

        public ObservableCollection<Product> GetAllProductsInDatabase()
        {
            ObservableCollection<Product> products;

            lock (databaseLock)
            {
                products = new ObservableCollection<Product>(DbConnection.Table<Product>().Where(x => x.WhereIsIt == "Database").ToList());
            }

            return products;
        }

        public ObservableCollection<Product> GetAllProductsInDrafts()
        {
            ObservableCollection<Product> products;

            lock (databaseLock)
            {
                products = new ObservableCollection<Product>(DbConnection.Table<Product>().Where(x => x.WhereIsIt == "Drafts").ToList());
            }

            return products;
        }

        public ObservableCollection<Product> GetAllProductsInTrash()
        {
            ObservableCollection<Product> product;

            lock (databaseLock)
            {
                product = new ObservableCollection<Product>(DbConnection.Table<Product>().Where(x => x.WhereIsIt == "Trash").ToList());
            }

            return product;
        }

        public bool CheckReproducibility(string code)
        {
            return DbConnection.Table<Product>().Any(x => x.Code == code);
        }

        public ObservableCollection<Product> SearchUsers(string field, string _string, string whereIsIt)
        {
            ObservableCollection<Product> products = new ObservableCollection<Product>();

            switch (field)
            {
                case "Nazwa":
                    lock (databaseLock)
                    {
                        products = new ObservableCollection<Product>(DbConnection.Table<Product>().Where(x => x.WhereIsIt == whereIsIt && x.Name.Contains(_string)).ToList());
                    }
                    break;

                case "Kod":
                    lock (databaseLock)
                    {
                        products = new ObservableCollection<Product>(DbConnection.Table<Product>().Where(x => x.WhereIsIt == whereIsIt && x.Code.Contains(_string)).ToList());
                    }
                    break;

                case "Kategoria ":
                    lock (databaseLock)
                    {
                        products = new ObservableCollection<Product>(DbConnection.Table<Product>().Where(x => x.WhereIsIt == whereIsIt && x.Category.Contains(_string)).ToList());
                    }
                    break;

                case "Stawka VAT":
                    lock (databaseLock)
                    {
                        try
                        {
                            if (_string != string.Empty)
                            {
                                byte vat = byte.Parse(_string);
                                products = new ObservableCollection<Product>(DbConnection.Table<Product>().Where(x => x.WhereIsIt == whereIsIt && x.VAT == vat).ToList());
                            }
                            else
                            {
                                products = new ObservableCollection<Product>(DbConnection.Table<Product>().Where(x => x.WhereIsIt == whereIsIt).ToList());
                            }
                        }
                        catch
                        {
                            
                        }                                        
                    }
                    break;

                case "J. miary":
                    lock (databaseLock)
                    {
                        products = new ObservableCollection<Product>(DbConnection.Table<Product>().Where(x => x.WhereIsIt == whereIsIt && x.UnitOfMeasure.Contains(_string)).ToList());

                    }
                    break;               
                default:
                    break;
            }

            return products;
        }

        public ObservableCollection<string> CategoryFieldsColectionMethod()
        {
            ObservableCollection<string> category = new ObservableCollection<string>(DbConnection.Table<Category_addproducts>().Select(x => x.category).ToList());

            return category;
        }

        public ObservableCollection<byte> VatFieldsColectionMethod()
        {
            ObservableCollection<byte> vat = new ObservableCollection<byte>(DbConnection.Table<VAT_addproductscs>().Select(x => x.vat).ToList());

            return vat;
        }

        public ObservableCollection<string> UnitOfMeasureColectionMethod()
        {
            ObservableCollection<string> unitOfMeasure = new ObservableCollection<string>(DbConnection.Table<UnitOfMeasure_addproducts>().Select(x => x.unitOfMeasure).ToList());

            return unitOfMeasure;
        }

        public ObservableCollection<string> ProductFieldsColectionMethod()
        {
            ObservableCollection<string> productFields = new ObservableCollection<string>(DbConnection.Table<ProductFields_addproducts>().Select(x => x.productField).ToList());

            return productFields;
        }
    }
}
