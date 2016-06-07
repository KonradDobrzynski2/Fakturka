using System.Collections.Generic;
using System.Collections.ObjectModel;
using Data.Model;
using System.Linq;
using SQLite.Net;
using System;
using Data.AnotherClass;

namespace Data.Repositories
{
    class DocumentDbRepository : BaseDbRepository
    {
        private DocumentProductcsDbRepository documentProductcsDbRepository;

        private Settings settings;

        public DocumentDbRepository(SQLiteConnection connection) : base(connection)
        {
            documentProductcsDbRepository = new DocumentProductcsDbRepository(DbConnection);

            settings = new Settings();

            DbConnection.CreateTable<Document>();
            DbConnection.CreateTable<DocumentProductcs>();

            #region Documents
            var info = DbConnection.GetTableInfo("TypeDocument_adddocuments");
            if (!info.Any())
            {
                # region CreateTypeDocument_adddocuments
                DbConnection.CreateTable<TypeDocument_adddocuments>();
                TypeDocument_adddocuments TypeDocument = new TypeDocument_adddocuments();

                TypeDocument.typeDocument = "Faktura VAT 23%";
                DbConnection.Insert(TypeDocument);

                TypeDocument.typeDocument = "Faktura VAT Marża";
                DbConnection.Insert(TypeDocument);

                TypeDocument.typeDocument = "Paragon";
                DbConnection.Insert(TypeDocument);

                #endregion
            }

            info = DbConnection.GetTableInfo("TypeOfPrices_adddocuments");
            if (!info.Any())
            {
                #region CreateTypeOfPrices_adddocuments
                DbConnection.CreateTable<TypeOfPrices_adddocuments>();
                TypeOfPrices_adddocuments TypeOfPrices = new TypeOfPrices_adddocuments();

                TypeOfPrices.typeOfPrice = "Netto";
                DbConnection.Insert(TypeOfPrices);

                TypeOfPrices.typeOfPrice = "Brutto";
                DbConnection.Insert(TypeOfPrices);
                #endregion
            }

            info = DbConnection.GetTableInfo("NumberDocument_adddocuments");
            if (!info.Any())
            {
                #region CreateNumberDocument_adddocuments
                DbConnection.CreateTable<NumberDocument_adddocuments>();
                NumberDocument_adddocuments NumberDocument = new NumberDocument_adddocuments();

                NumberDocument.numberDocument = "Automatyczny";
                DbConnection.Insert(NumberDocument);

                NumberDocument.numberDocument = "Własny";
                DbConnection.Insert(NumberDocument);
                #endregion
            }

            info = DbConnection.GetTableInfo("PaymentType_adddocuments");
            if (!info.Any())
            {
                # region CreatePaymentType_adddocuments
                DbConnection.CreateTable<PaymentType_adddocuments>();
                PaymentType_adddocuments PaymentType_adddocuments = new PaymentType_adddocuments();

                PaymentType_adddocuments.paymentType = "Gotówka";
                DbConnection.Insert(PaymentType_adddocuments);

                PaymentType_adddocuments.paymentType = "Przelew";
                DbConnection.Insert(PaymentType_adddocuments);
                #endregion
            }

            info = DbConnection.GetTableInfo("DocumentFields_adddocuments");
            if (!info.Any())
            {
                #region CreateDocumentFields_adddocuments
                DbConnection.CreateTable<DocumentFields_adddocuments>();
                DocumentFields_adddocuments DocumentFields_adddocuments = new DocumentFields_adddocuments();

                DocumentFields_adddocuments.documentField = "Typ dokumentu";
                DbConnection.Insert(DocumentFields_adddocuments);

                DocumentFields_adddocuments.documentField = "Numer dokumentu";
                DbConnection.Insert(DocumentFields_adddocuments);
                #endregion
            }
            #endregion
        }

        public void Insert(Document document)
        {
            lock (databaseLock)
            {
                DbConnection.Insert(document);
                documentProductcsDbRepository.InsertList(document.ID, document.Products);
            }
        }

        public void DeleteDokumentInDatabase(string numberDocument)
        {
            var query = "UPDATE Document SET WhereIsIt =\"Trash\" WHERE NameDocument ='" + numberDocument + "'";

            lock (databaseLock)
            {
                DbConnection.Execute(query);
            }
        }

        public void DeleteDokumentInTrash(int ID)
        {
            lock (databaseLock)
            {
                DocumentProductcsDbRepository documentProductcsDbRepository = new DocumentProductcsDbRepository(DbConnection);

                DbConnection.Delete<Document>(ID);

                documentProductcsDbRepository.DeleteProductsListDocument(ID);
            }
        }

        public void RestoreDokumentInTrash(string numberDocument)
        {
            var query = "UPDATE Document SET WhereIsIt =\"Database\" WHERE NumberDocument ='" + numberDocument + "'";

            lock (databaseLock)
            {
                DbConnection.Execute(query);
            }
        }

        public void RenameNipInDocuments(string OldNip, string NewNip)
        {
            lock (databaseLock)
            {
                var query = "UPDATE Document SET Recipient_ID='" + NewNip + "' WHERE Recipient_ID='" + OldNip + "'";
                DbConnection.Execute(query);

                query = "UPDATE Document SET Payer_ID='" + NewNip + "' WHERE Payer_ID='" + OldNip + "'";
                DbConnection.Execute(query);
            }
        }

        public ObservableCollection<Document> GetAllDocuments(bool CompleteFieldsDocument = false)
        {
            ObservableCollection<Document> documents;

            lock (databaseLock)
            {
                documents = new ObservableCollection<Document>(DbConnection.Table<Document>().ToList());

                if (CompleteFieldsDocument == true)
                {
                    foreach (var item1 in documents)
                    {
                        var list = DbConnection.Query<DocumentProductcs>("select * from DocumentProductcs where NumberDocument = '" + item1.NameDocument + "'");

                        item1.Products = new ObservableCollection<Product>();

                        foreach (var item2 in list)
                        {
                            var itemik = DbConnection.Query<Product>("select * from Product where Code = '" + item2.Code + "'").FirstOrDefault();
                            item1.Products.Add(itemik);
                        }

                    }
                }            
            }

            return documents;
        }   

        public ObservableCollection<Document> GetAllDocumentsInDatabase(bool CompleteFieldsDocument = false)
        {
            ObservableCollection<Document> documents;

            lock (databaseLock)
            {
                documents =  new ObservableCollection<Document>(DbConnection.Table<Document>().Where(x => x.WhereIsIt == "Database").ToList());

                if (CompleteFieldsDocument == true)
                {
                    foreach (var item1 in documents)
                    {
                        var list = DbConnection.Query<DocumentProductcs>("select * from DocumentProductcs where NumberDocument = '" + item1.NameDocument + "'");

                        item1.Products = new ObservableCollection<Product>();

                        foreach (var item2 in list)
                        {
                            var itemik = DbConnection.Query<Product>("select * from Product where Code = '" + item2.Code + "'").FirstOrDefault();
                            item1.Products.Add(itemik);
                        }

                    }
                }
            }

            return documents;
        }

        public ObservableCollection<Document> GetAllDocumentsInTrash(bool CompleteFieldsDocument = false)
        {
            ObservableCollection<Document> documents;

            lock (databaseLock)
            {
                documents = new ObservableCollection<Document>(DbConnection.Table<Document>().Where(x => x.WhereIsIt == "Trash").ToList());

                if (CompleteFieldsDocument == true)
                {
                    foreach (var item1 in documents)
                    {
                        var list = DbConnection.Query<DocumentProductcs>("select * from DocumentProductcs where NumberDocument = '" + item1.NameDocument + "'");

                        item1.Products = new ObservableCollection<Product>();

                        foreach (var item2 in list)
                        {
                            var itemik = DbConnection.Query<Product>("select * from Product where Code = '" + item2.Code + "'").FirstOrDefault();
                            item1.Products.Add(itemik);
                        }

                    }
                }
            }

            return documents;
        }

        public Document GetDocument(int ID, bool CompleteFieldsDocument = false)
        {
            Document document = new Document();

            lock (databaseLock)
            {
                document = DbConnection.Table<Document>().Where(x => x.ID == ID).FirstOrDefault();

                if (CompleteFieldsDocument == true)
                {
                    var list = DbConnection.Query<DocumentProductcs>("select * from DocumentProductcs where ID_Document = '" + ID + "'");;

                    Product product = new Product();
                    document.Products = new ObservableCollection<Product>();

                    foreach (var item in list)
                    {
                        product = DbConnection.Table<Product>().Where(x => x.Code == item.Code).FirstOrDefault();
                        document.Products.Add(product);
                    }
                }
            }

            return document;
        }

        public bool CheckReproducibility(string numberDocument)
        {
            return DbConnection.Table<Document>().Any(x => x.NameDocument == numberDocument);
        }

        public ObservableCollection<Document> SearchDokuments(string field, string name, string whereIsIt)
        {
            ObservableCollection<Document> documents = new ObservableCollection<Document>();

            switch (field)
            {
                case "Typ dokumentu":
                    lock (databaseLock)
                    {
                        documents = new ObservableCollection<Document>(DbConnection.Table<Document>().Where(x => x.WhereIsIt == whereIsIt && x.TypeDocument.Contains(name)).ToList());
                    }
                    break;

                case "Numer dokumentu":
                    lock (databaseLock)
                    {
                        documents = new ObservableCollection<Document>(DbConnection.Table<Document>().Where(x => x.WhereIsIt == whereIsIt && x.NameDocument.Contains(name)).ToList());
                    }
                    break;
                default:
                    break;
            }

            return documents;
        }


        public ObservableCollection<string> TypeDocumentColectionMethod()
        {
            ObservableCollection<string> type = new ObservableCollection<string>(DbConnection.Table<TypeDocument_adddocuments>().Select(x => x.typeDocument).ToList());

            return type;
        }

        public ObservableCollection<string> TypeOfPricesColectionMethod()
        {
            ObservableCollection<string> state = new ObservableCollection<string>(DbConnection.Table<TypeOfPrices_adddocuments>().Select(x => x.typeOfPrice).ToList());

            return state;
        }

        public ObservableCollection<string> NumberDocumentColectionMethod()
        {
            ObservableCollection<string> country = new ObservableCollection<string>(DbConnection.Table<NumberDocument_adddocuments>().Select(x => x.numberDocument).ToList());

            return country;
        }

        public ObservableCollection<string> PaymentTypeColectionMethod()
        {
            ObservableCollection<string> userFields = new ObservableCollection<string>(DbConnection.Table<PaymentType_adddocuments>().Select(x => x.paymentType).ToList());

            return userFields;
        }

        public ObservableCollection<string> DocumentsFieldsColectionMethod()
        {
            ObservableCollection<string> userFields = new ObservableCollection<string>(DbConnection.Table<DocumentFields_adddocuments>().Select(x => x.documentField).ToList());

            return userFields;
        }


        public string GetNameDocument_AddToDatabase()
        {
            // Pobieranie id dokumentu któy został dodany jako ostatni. Nie działa (zwracane jest 0) gdy odpalamy program i wykonamy tą linie.
            // Działa tylko wtedy kiedy po odpaleniu programu, dodamy jakiś dokument i dopiero wykonamy gdizekolwiek tą linię kodu.
            // var key = DbConnection.ExecuteScalar<int>("SELECT last_insert_rowid() from Document ");

            string NameDocument = string.Empty;

            string HelpName = string.Empty;

            string NowYear = DateTime.Now.Year.ToString();

            string NowMonth = string.Empty;

            if (DateTime.Now.Month == 10 || DateTime.Now.Month == 11 || DateTime.Now.Month == 12)
            {
                NowMonth = DateTime.Now.Month.ToString();
            }
            else
            {
                NowMonth = "0" + DateTime.Now.Month.ToString();
            }


            NameDocument = NowYear + "/" + NowMonth + "/" + settings.GetNextIdDocument();

            return NameDocument;
        }
    }
}
