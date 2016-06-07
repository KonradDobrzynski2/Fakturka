using Data.AnotherClass;
using Data.Messengers;
using Data.Model;
using Data.Repositories;
using Data.Services;
using Data.ViewModel.Another;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Data.ViewModel
{
    public class AddWithDraftsProductsViewModel : BaseViewModel
    {
        private INavigation navigation;

        private ProductDbRepository productDbRepository;

        public ICommand AddUProductToDatabaseCommand { get; set; }
        public ICommand ClearAllFieldsCommand { get; set; }

        private string ParentCode = string.Empty;

        #region BindingPropertis
        private bool isEnabledAllFields = true;
        public bool IsEnabledAllFields
        {
            get { return isEnabledAllFields; }
            set
            {
                Set(ref isEnabledAllFields, value);
            }
        }

        private string name = string.Empty;
        public string Name
        {
            get { return name; }
            set
            {
                Set(ref name, value);

                if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
                else if (Name != string.Empty && Status == NameErrorMessage)
                {
                    Status = string.Empty;
                }
            }
        }

        private string code = string.Empty;
        public string Code
        {
            get { return code; }
            set
            {
                Set(ref code, value);

                if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
                if (Code != string.Empty && Status == CodeErrorMessage)
                {
                    Status = string.Empty;
                }
            }
        }

        private string category = string.Empty;
        public string Category
        {
            get { return category; }
            set
            {
                if (value == "Zarządzaj kategoriami (+/-)")
                {
                    NavigateToCategoryView();
                }
                else
                {
                    Set(ref category, value);
                }

                if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
            }
        }

        private string vat = string.Empty;
        public string Vat
        {
            get { return vat; }
            set
            {
                Set(ref vat, value);

                if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
                else if (Vat != string.Empty && Status == VatErrorMessage)
                {
                    Status = string.Empty;
                }
            }
        }

        private string unitOfMeasure = string.Empty;
        public string UnitOfMeasure
        {
            get { return unitOfMeasure; }
            set
            {
                Set(ref unitOfMeasure, value);

                if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
            }
        }

        private string netBuy = string.Empty;
        public string NetBuy
        {
            get { return netBuy; }
            set
            {
                Set(ref netBuy, value);

                if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
                else if (NetBuy != string.Empty && Status == NetBuy)
                {
                    Status = string.Empty;
                }
            }
        }

        private string grossBuy = string.Empty;
        public string GrossBuy
        {
            get { return grossBuy; }
            set
            {
                Set(ref grossBuy, value);

                if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
                else if (GrossBuy != string.Empty && Status == GrossBuy)
                {
                    Status = string.Empty;
                }
            }
        }

        private string netSale = string.Empty;
        public string NetSale
        {
            get { return netSale; }
            set
            {
                Set(ref netSale, value);

                if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
                else if (NetSale != string.Empty && Status == NetSale)
                {
                    Status = string.Empty;
                }
            }
        }

        private string grossSale = string.Empty;
        public string GrossSale
        {
            get { return grossSale; }
            set
            {
                Set(ref grossSale, value);

                if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
                else if (GrossSale != string.Empty && Status == GrossSale)
                {
                    Status = string.Empty;
                }
            }
        }

        private string status = string.Empty;
        public string Status
        {
            get { return status; }
            set
            {
                Set(ref status, value);
            }
        }

        private string foregroundStatus;
        public string ForegroundStatus
        {
            get { return foregroundStatus; }
            set
            {
                Set(ref foregroundStatus, value);
            }
        }


        private ObservableCollection<string> categoryColection;
        public ObservableCollection<string> CategoryColection
        {
            get { return categoryColection; }
            set
            {
                if (!value.Contains("Zarządzaj kategoriami (+/-)"))
                {
                    value.Add("Zarządzaj kategoriami (+/-)");
                }

                Set(ref categoryColection, value);
            }
        }

        private ObservableCollection<byte> vatColection;
        public ObservableCollection<byte> VatColection
        {
            get { return vatColection; }
            set
            {
                Set(ref vatColection, value);
            }
        }

        private ObservableCollection<string> unitOfMeasureColection;
        public ObservableCollection<string> UnitOfMeasureColection
        {
            get { return unitOfMeasureColection; }
            set
            {
                Set(ref unitOfMeasureColection, value);
            }
        }
        #endregion

        #region LocalPropertis
        private string AddMessage = "Dodano nowy produkt!";

        private string NameErrorMessage = "Prosze podać nazwę produktu";

        private string CodeErrorMessage = "Prosze podać kod produktu";
        private string CodeReproducibility = "Towar o podanym kodzie już istnieje";

        private string VatErrorMessage = "Prosze podać stawkę VAT";

        private string NetBuyMessage = "Prosze podać cenę netto zakupu";
        private string GrossBuyMessage = "Prosze podać cenę brutto zakupu";
        private string NetSaleMessage = "Prosze podać cenę netto sprzedaży";
        private string GrossSaleMessage = "Prosze podać brutto sprzedaży";

        private string PriceErrorMessage = "Któraś z cen jest niepoprawna";
        #endregion

        public AddWithDraftsProductsViewModel(IDatabase database)
        {
            navigation = new WindowsNavigation();

            productDbRepository = new ProductDbRepository(database.Connection);

            AddUProductToDatabaseCommand = new RelayCommand(AddProductToDatabase);
            ClearAllFieldsCommand = new RelayCommand(ClearAllFields);

            CategoryColection = productDbRepository.CategoryFieldsColectionMethod();
            VatColection = productDbRepository.VatFieldsColectionMethod();
            UnitOfMeasureColection = productDbRepository.UnitOfMeasureColectionMethod();

            Messenger.Default.Register<Product>(this, "AddWithDrafts", this.FillFields);
        }

        #region BindingMethod
        private void AddProductToDatabase()
        {
            #region CheckPropertis
            if (Name == string.Empty)
            {
                Status = NameErrorMessage;
                ForegroundStatus = "Red";
                return;
            }
            else if (Code == string.Empty)
            {
                Status = CodeErrorMessage;
                ForegroundStatus = "Red";
                return;
            }
            else if (ParentCode != Code && productDbRepository.CheckReproducibility(Code))
            {
                Status = CodeReproducibility;
                ForegroundStatus = "Red";
                return;
            }
            else if (Vat == string.Empty)
            {
                Status = VatErrorMessage;
                ForegroundStatus = "Red";
                return;
            }
            else if (NetBuy == string.Empty)
            {
                Status = NetBuyMessage;
                ForegroundStatus = "Red";
                return;
            }
            else if (GrossBuy == string.Empty)
            {
                Status = GrossBuyMessage;
                ForegroundStatus = "Red";
                return;
            }
            else if (NetSale == string.Empty)
            {
                Status = NetSaleMessage;
                ForegroundStatus = "Red";
                return;
            }
            else if (GrossSale == string.Empty)
            {
                Status = GrossSaleMessage;
                ForegroundStatus = "Red";
                return;
            }
            #endregion

            NetBuy = Strings.ReplaceDotToComma(NetBuy);
            NetSale = Strings.ReplaceDotToComma(NetSale);
            GrossBuy = Strings.ReplaceDotToComma(GrossBuy);
            GrossSale = Strings.ReplaceDotToComma(GrossSale);

            try
            {
                Product product = new Product()
                {
                    Name = Name,
                    Code = Code,
                    Category = Category,
                    VAT = byte.Parse(Vat),
                    UnitOfMeasure = UnitOfMeasure,
                    NetBuy = (double)Math.Round(decimal.Parse(NetBuy), 2),
                    NetSale = (double)Math.Round(decimal.Parse(NetSale), 2),
                    GrossBuy = (double)Math.Round(decimal.Parse(GrossBuy), 2),
                    GrossSale = (double)Math.Round(decimal.Parse(GrossSale), 2),
                    WhereIsIt = "Database"
                };

                productDbRepository.DeleteProductInDrafts(ParentCode);
                productDbRepository.Insert(product);

                ClearFields();

                Status = AddMessage;
                ForegroundStatus = "Green";

                IsEnabledAllFields = false;
            }
            catch
            {
                ForegroundStatus = "Red";
                Status = PriceErrorMessage;
            }
        }

        private void NavigateToCategoryView()
        {
            navigation.NavigateTo(Type.GetType(string.Format("Data.ViewModel.{0}", "CreateCategoryViewModel")));

            Messenger.Default.Send<LocalButtonAndTitle>(new LocalButtonAndTitle() { Visability = "Visible", Title = "Kopie robocze / Dodaj produkt z kopii roboczych / Zarządzaj kategoriami (+/-)" });
        }

        private void ClearAllFields()
        {
            ClearFields();
        }
        #endregion

        #region AnotherMethods
        private void ClearFields()
        {
            Name = string.Empty;
            Code = string.Empty;
            Vat = string.Empty;
            Category = string.Empty;
            Vat = string.Empty;
            UnitOfMeasure = string.Empty;
            NetBuy = string.Empty;
            NetSale = string.Empty;
            GrossBuy = string.Empty;
            GrossSale = string.Empty;
            Status = string.Empty;
        }

        public override void OnResume()
        {
            base.OnResume();

            CategoryColection = productDbRepository.CategoryFieldsColectionMethod();

            IsEnabledAllFields = true;
        }

        private void FillFields(Product commingProduct)
        {
            Name = commingProduct.Name;
            Code = commingProduct.Code;
            Category = commingProduct.Category;
            Vat = commingProduct.VAT.ToString();
            UnitOfMeasure = commingProduct.UnitOfMeasure;

            if (commingProduct.NetBuy != double.MaxValue)
            {
                NetBuy = commingProduct.NetBuy.ToString();
            }
            if (commingProduct.GrossBuy != double.MaxValue)
            {
                GrossBuy = commingProduct.GrossBuy.ToString();
            }
            if (commingProduct.NetSale != double.MaxValue)
            {
                NetSale = commingProduct.NetSale.ToString();
            }
            if (commingProduct.GrossSale != double.MaxValue)
            {
                GrossSale = commingProduct.GrossSale.ToString();
            }

            ParentCode = commingProduct.Code;
        }
        #endregion
    }
}
