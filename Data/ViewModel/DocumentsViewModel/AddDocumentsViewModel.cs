using Data.Model;
using System;
using Data.Repositories;
using Data.ViewModel.Another;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Windows.Input;
using Data.Singletons;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using Data.Services;
using Data.Messengers;
using System.Linq;
using Windows.UI.Xaml.Data;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Data.AnotherClass;

namespace Data.ViewModel
{
    public class AddDocumentsViewModel : BaseViewModel
    {
        private DocumentDbRepository documentDbRepository;
        private INavigation navigation;

        private Settings settings;

        public ICommand AddDocumentToDatabaseCommand { get; set; }
        public ICommand AddDocumentAndGeneratePDFCommand { get; set; }
        public ICommand ClearAllFieldsCommand { get; set; }
        public ICommand DeleteProdyctWithListCommand { get; set; }
        public ICommand NavigateCommand { get; set; }

        #region BindingPropertis
        private string typeDocument = string.Empty;
        public string TypeDocument
        {
            get { return typeDocument; }
            set
            {
                Set(ref typeDocument, value);

                if (Status == AddDocumentEndPDFMessage || Status == AddDocumentMessage || Status == AddDocumentToDraftsMessage)
                {
                    Status = string.Empty;
                    StatusVisibility = "Collapsed";
                }
                else if (TypeDocument != string.Empty && Status == TypeDocumentError)
                {
                    StatusVisibility = "Collapsed";
                    Status = string.Empty;
                }
            }
        }

        private string kindOfPrices = string.Empty;
        public string KindOfPrices
        {
            get { return kindOfPrices; }
            set
            {
                Set(ref kindOfPrices, value);

                if (Status == AddDocumentEndPDFMessage || Status == AddDocumentMessage || Status == AddDocumentToDraftsMessage)
                {
                    Status = string.Empty;
                }
                else if (KindOfPrices != string.Empty && Status == KindOfPricesError)
                {
                    StatusVisibility = "Collapsed";
                    Status = string.Empty;
                }
            }
        }

        private string checkFieldNumberDocument = string.Empty;
        public string CheckFieldNumberDocument
        {
            get
            {
                if (checkFieldNumberDocument == string.Empty)
                {
                    checkFieldNumberDocument = NumberDocumentColection[0];
                    return NumberDocumentColection[0];
                }

                return checkFieldNumberDocument;
            }
            set
            {
                Set(ref checkFieldNumberDocument, value);

                if (value == NumberDocumentColection[1])
                {
                    NumberDocumentFieldOpenClose = true;
                }
                else
                {
                    NumberDocument = string.Empty;
                    NumberDocumentFieldOpenClose = false;
                }
            }
        }

        private string numberDocument = string.Empty;
        public string NumberDocument
        {
            get { return numberDocument; }
            set
            {
                Set(ref numberDocument, value);
            }
        }

        private User recipient;
        public User Recipient
        {
            get
            {             
                return recipient;
            }
            set
            {
                Set(ref recipient, value);

                if (Status == AddDocumentEndPDFMessage || Status == AddDocumentMessage || Status == AddDocumentToDraftsMessage)
                {
                    Status = string.Empty;
                }
                else if (recipient != null && Status == RecipientError)
                {
                    StatusVisibility = "Collapsed";
                    Status = string.Empty;
                }
            }
        } 

        private User payer;
        public User Payer
        {
            get
            { 
                return payer;
            }
            set
            {
                Set(ref payer,value);

                if (Status == AddDocumentEndPDFMessage || Status == AddDocumentMessage || Status == AddDocumentToDraftsMessage)
                {
                    Status = string.Empty;
                }
                else if (payer != null && Status == PayerError)
                {
                    StatusVisibility = "Collapsed";
                    Status = string.Empty;
                }
            }
        }      

        private string paymentType = string.Empty;
        public string PaymentType
        {
            get
            {
                if (paymentType == string.Empty)
                {
                    paymentType = PaymentTypeColection[0];
                    return PaymentTypeColection[0];
                }

                return paymentType;
            }
            set
            {
                Set(ref paymentType, value);

                if (value == PaymentTypeColection[1])
                {
                    AccountNumberFieldOpenClose = true;
                }
                else
                {
                    AccountNumber = string.Empty;
                    AccountNumberFieldOpenClose = false;
                }
            }
        }

        private string accountNumber = string.Empty;
        public string AccountNumber
        {
            get { return accountNumber; }
            set
            {
                Set(ref accountNumber, value);

                if (Status == AddDocumentEndPDFMessage || Status == AddDocumentMessage || Status == AddDocumentToDraftsMessage)
                {
                    Status = string.Empty;
                }
                else if (AccountNumber != string.Empty && Status == AccountNumberError)
                {
                    StatusVisibility = "Collapsed";
                    Status = string.Empty;
                }
            }
        }

        private DateTimeOffset dateOfPayment = DateTime.Now;
        public DateTimeOffset DateOfPayment
        {
            get { return dateOfPayment; }
            set
            {
                Set(ref dateOfPayment, value);
            }
        }

        private DateTimeOffset dateOfSale = DateTime.Now;
        public DateTimeOffset DateOfSale
        {
            get { return dateOfSale; }
            set
            {
                Set(ref dateOfSale, value);
            }
        }


        private bool numberDocumentFieldOpenClose = false;
        public bool NumberDocumentFieldOpenClose
        {
            get { return numberDocumentFieldOpenClose; }
            set
            {
                Set(ref numberDocumentFieldOpenClose, value);
            }
        }

        private bool accountNumberFieldOpenClose = false;
        public bool AccountNumberFieldOpenClose
        {
            get { return accountNumberFieldOpenClose; }
            set
            {
                Set(ref accountNumberFieldOpenClose, value);
            }
        }

        private Product selectedValue;
        public Product SelectedValue
        {
            get
            {
                return selectedValue;
            }
            set
            {
                Set(ref selectedValue, value);
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

        private string statusVisibility = "Collapsed";
        public string StatusVisibility
        {
            get { return statusVisibility; }
            set
            {
                Set(ref statusVisibility, value);
            }
        }

        private string foregroundStatus = string.Empty;
        public string ForegroundStatus
        {
            get { return foregroundStatus; }
            set
            {
                Set(ref foregroundStatus, value);
            }
        }


        private ObservableCollection<string> numberDocumentColection;
        public ObservableCollection<string> NumberDocumentColection
        {
            get { return numberDocumentColection; }
            set
            {
                Set(ref numberDocumentColection, value);
            }
        }

        private ObservableCollection<string> typeDocumentColection;
        public ObservableCollection<string> TypeDocumentColection
        {
            get { return typeDocumentColection; }
            set
            {
                Set(ref typeDocumentColection, value);
            }
        }

        private ObservableCollection<string> typeOfPricesColection;
        public ObservableCollection<string> TypeOfPricesColection
        {
            get { return typeOfPricesColection; }
            set
            {
                Set(ref typeOfPricesColection, value);
            }
        }

        private ObservableCollection<string> paymentTypeColection;
        public ObservableCollection<string> PaymentTypeColection
        {
            get { return paymentTypeColection; }
            set
            {
                Set(ref paymentTypeColection, value);
            }
        }

        private ObservableCollection<Product> itemProductList;
        public ObservableCollection<Product> ItemProductList
        {
            get
            {
                return itemProductList;
            }
            set
            {
                Set(ref itemProductList, value);
            }
        }
        #endregion

        #region LocalPropertis
        private string TypeDocumentError = "Wybierz typ dokumentu";
        private string KindOfPricesError = "Wybierz rodzaj cen";
        private string RecipientError = "Określ odbiorce dokumentu";
        private string PayerError = "Określi płatnika dokumentu";
        private string AccountNumberError = "Podaj numer konta wystawcy (własny)";

        private string AddDocumentMessage = "Dokument został zapisany";
        private string AddDocumentToDraftsMessage = "Dokument został przeniesiony do kopii roboczych";
        private string AddDocumentEndPDFMessage = "Dokument został zapisany wraz z wygenerowaniem dokumentu .PDF";

        private string AddErrorMessage = "UPS, coś poszło nie tak, spróbuj jeszcze raz";
        #endregion

        public AddDocumentsViewModel(IDatabase database)
        {
            documentDbRepository = new DocumentDbRepository(database.Connection);
            navigation = new WindowsNavigation();

            settings = new Settings();

            AddDocumentToDatabaseCommand = new RelayCommand(AddDocumentToDatabase);
            AddDocumentAndGeneratePDFCommand = new RelayCommand(AddDocumentAndGeneratePDF);
            ClearAllFieldsCommand = new RelayCommand(ClearAllFields);
            DeleteProdyctWithListCommand = new RelayCommand(DeleteProductWithList);
            NavigateCommand = new RelayCommand<string>(viewModelType => Navigate(viewModelType));

            ItemProductList = new ObservableCollection<Product>();

            NumberDocumentColection = documentDbRepository.NumberDocumentColectionMethod();
            TypeDocumentColection = documentDbRepository.TypeDocumentColectionMethod();
            TypeOfPricesColection = documentDbRepository.TypeOfPricesColectionMethod();
            PaymentTypeColection = documentDbRepository.PaymentTypeColectionMethod();

            Messenger.Default.Register<User>(this,"Payer", (x => Payer = x));
            Messenger.Default.Register<User>(this, "Recipient", (x => Recipient = x));
            Messenger.Default.Register<Product>(this, this.AddProductToList);
        }

        #region BindingMethod
         private void AddDocumentToDatabase()
        {
            #region CheckPropertis
            if (TypeDocument == string.Empty)
            {
                Status = TypeDocumentError;
                StatusVisibility = "Visable";
                ForegroundStatus = "Red";
                return;
            }
            else if (kindOfPrices == string.Empty)
            {
                Status = KindOfPricesError;
                StatusVisibility = "Visable";
                ForegroundStatus = "Red";
                return;
            }
            else if (Recipient == null)
            {
                Status = RecipientError;
                StatusVisibility = "Visable";
                ForegroundStatus = "Red";
                return;
            }
            else if (Payer == null)
            {
                Status = PayerError;
                StatusVisibility = "Visable";
                ForegroundStatus = "Red";
                return;
            }
            else if (PaymentType == TypeOfPricesColection[1] && AccountNumber == string.Empty)
            {
                Status = AccountNumber;
                StatusVisibility = "Visable";
                ForegroundStatus = "Red";
                return;
            }
            #endregion

            try
            {
                Document document = new Document()
                {
                    TypeDocument = TypeDocument,
                    KindOfPrices = KindOfPrices,
                    NameDocument = NumberDocument,
                    PaymentType = PaymentType,
                    AccountNumber = AccountNumber,
                    DateOfPayment = DateOfPayment.Date.ToString().Remove(10),
                    DateOfSale = DateOfSale.Date.ToString().Remove(10),
                    WhereIsIt = "Database",
                    Recipient_ID = Recipient.Nip,
                    Payer_ID = Payer.Nip,
                    Products = ItemProductList
                };

                documentDbRepository.Insert(document);

                settings.UpdateNextIdDocument();

                ClearAllFields();
                Status = AddDocumentMessage;
                ForegroundStatus = "Green";
                StatusVisibility = "Visable";

                NumberDocument = documentDbRepository.GetNameDocument_AddToDatabase();
            }
            catch
            {
                ClearAllFields();
                Status = AddErrorMessage;
                ForegroundStatus = "Red";
                StatusVisibility = "Visable";
            }           
        }

        private void AddDocumentAndGeneratePDF()
        {
            throw new NotImplementedException();
        }

        private void DeleteProductWithList()
        {
            if (SelectedValue != null)
            {
                ItemProductList.Remove(SelectedValue);
            }
        }

        public void Navigate(string viewModelType)
        {
            navigation.NavigateTo(Type.GetType(string.Format("Data.ViewModel.{0}", viewModelType)));

            switch (viewModelType)
            {
                case "SelectPayerViewModel":
                    Messenger.Default.Send<LocalButtonAndTitle>(new LocalButtonAndTitle() { Visability = "Visible", Title = "Dodaj dokument / Wybór płatnika" });
                    break;
                case "SelectRecipientViewModel":
                    Messenger.Default.Send<LocalButtonAndTitle>(new LocalButtonAndTitle() { Visability = "Visible", Title = "Dodaj dokument / Wybór odbiorcy" });
                    break;
                case "SelectProductViewModel":
                    Messenger.Default.Send<LocalButtonAndTitle>(new LocalButtonAndTitle() { Visability = "Visible", Title = "Dodaj dokument / Wybór produktu" });
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region AnotherMethods
        private void ClearAllFields()
        {
            TypeDocument = string.Empty;
            KindOfPrices = string.Empty;
            Payer = null;
            Recipient = null;
            PaymentType = string.Empty;
            AccountNumber = string.Empty;
            DateOfPayment = DateTime.Now;
            DateOfSale = DateTime.Now;
            ItemProductList.Clear();
            Status = string.Empty;
        }

        public override void OnResume()
        {
            base.OnResume();

            NumberDocument = documentDbRepository.GetNameDocument_AddToDatabase();
        }

        private void AddProductToList(Product commingProduct)
        {
            if (ItemProductList.Any(x => x.Name == commingProduct.Name))
            {
                ItemProductList.Where(x => x.Name == commingProduct.Name).First().Quantity++;
            }
            else
            {
                commingProduct.Quantity = 1;

                ItemProductList.Add(commingProduct);
            }
        }
        #endregion
    }
}
