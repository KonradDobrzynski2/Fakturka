using Data.AnotherClass;
using Data.Messengers;
using Data.Model;
using Data.Repositories;
using Data.Services;
using Data.ViewModel.Another;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Data.ViewModel
{
    public class AddWithDraftsProvidersViewModel : BaseViewModel
    {
        private UserDbRepository userDbRepository;
                
        public ICommand AddUserToDatabaseCommand { get; set; }
        public ICommand ClearAllFieldsCommand { get; set; }

        private string ParentNip = string.Empty;

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

        private string nameCompany = string.Empty;
        public string NameCompany
        {
            get { return nameCompany; }
            set
            {         
                Set(ref nameCompany, value);

                if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
                else if (NameCompany != string.Empty && Status == NameCompanyErrorMessage)
                {
                    Status = string.Empty;
                }
            }
        }

        private string firstName = string.Empty;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                Set(ref firstName, value);

                if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
                else if (FirstName != string.Empty && Status == FirstNameErrorMessage)
                {
                    Status = string.Empty;
                }
            }
        }

        private string lastName = string.Empty;
        public string LastName
        {
            get { return lastName; }
            set
            {
                Set(ref lastName, value);

                if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
                else if (LastName != string.Empty && Status == LastNameErrorMessage)
                {
                    Status = string.Empty;
                }
            }
        }

        private string type = string.Empty;
        public string Type
        {
            get { return type; }
            set
            {
                Set(ref type, value);

                if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
                else if (Type != string.Empty && Status == TypeErrorMessage)
                {
                    Status = string.Empty;
                }
            }
        }

        private string nip = string.Empty;
        public string Nip
        {
            get { return nip; }
            set
            {
                Set(ref nip, value);

                if (!userDbRepository.CheckReproducibility(nip) && Status == NipReproducibility)
                {
                    Status = string.Empty;
                }
                else if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
                else if ((Nip != string.Empty && Status == NipErrorMessage) || (Nip != string.Empty && Status == NipSyntaxError))
                {
                    Status = string.Empty;
                }
                else if (Nip.Length == 10 && Status == NipLengthErrorMessage)
                {
                    Status = string.Empty;
                }
            }
        }

        private string regon = string.Empty;
        public string Regon
        {
            get { return regon; }
            set
            {
                Set(ref regon, value);

                if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
                else if ((Regon.Length == 9 && Status == RegonLengthErrorMessage) || (Regon != string.Empty && Status == RegonSyntaxError))
                {
                    Status = string.Empty;
                }
            }
        }

        private string pesel = string.Empty;
        public string Pesel
        {
            get { return pesel; }
            set
            {
                Set(ref pesel, value);

                if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
                else if ((Pesel.Length == 11 && Status == PeselLengthErrorMessage) || (Pesel != string.Empty && Status == PeselSyntaxError))
                {
                    Status = string.Empty;
                }
            }
        }

        private string postalCode = string.Empty;
        public string PostalCode
        {
            get { return postalCode; }
            set
            {
                Set(ref postalCode, value);

                if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
                else if (PostalCode != string.Empty && Status == PostCodeErrorMessage)
                {
                    Status = string.Empty;
                }
            }
        }

        private string city = string.Empty;
        public string City
        {
            get { return city; }
            set
            {
                Set(ref city, value);

                if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
                else if (City != string.Empty && Status == CityErrorMessage)
                {
                    Status = string.Empty;
                }
            }
        }

        private string address = string.Empty;
        public string Address
        {
            get { return address; }
            set
            {
                Set(ref address, value);

                if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
                else if (Address != string.Empty && Status == AddressErrorMessage)
                {
                    Status = string.Empty;
                }
            }
        }

        private string state = string.Empty;
        public string State
        {
            get { return state; }
            set
            {
                Set(ref state, value);

                if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
            }
        }

        private string country = string.Empty;
        public string Country
        {
            get { return country; }
            set
            {
                Set(ref country, value);

                if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
            }
        }

        private string email = string.Empty;
        public string Email
        {
            get { return email; }
            set
            {
                Set(ref email, value);

                if (Status == AddMessage)
                {
                    Status = string.Empty;
                }
            }
        }

        private string phoneNumber = string.Empty;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                Set(ref phoneNumber, value);

                if (Status == AddMessage)
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

        private ObservableCollection<string> typeColection;
        public ObservableCollection<string> TypeColection
        {
            get { return typeColection; }
            set
            {
                Set(ref typeColection, value);
            }
        }

        private ObservableCollection<string> stateColection;
        public ObservableCollection<string> StateColection
        {
            get { return stateColection; }
            set
            {
                Set(ref stateColection, value);
            }
        }

        private ObservableCollection<string> countryColection;
        public ObservableCollection<string> CountryColection
        {
            get { return countryColection; }
            set
            {
                Set(ref countryColection, value);
            }
        }
        #endregion

        #region LocalPropertis
        private string AddMessage = "Dodano nowego kontrahenta!";

        private string NameCompanyErrorMessage = "Prosze podać nazwę kontrahenta";

        private string FirstNameErrorMessage = "Prosze podać imię kontrahena";

        private string LastNameErrorMessage = "Prosze podać nazwisko kontrahenta";

        private string TypeErrorMessage = "Prosze określić typ kontrahenta";

        private string NipErrorMessage = "Prosze podać NIP kontrahenta";
        private string NipLengthErrorMessage = "NIP powinien składać się z 10 cyfr";
        private string NipReproducibility = "Kontrahent o podanym numerze NIP już istnieje!";
        private string NipSyntaxError = "Nip powinien zawierać tylko cyfry!";

        private string RegonLengthErrorMessage = "Regon powinien składać się z 9 cyfr";
        private string RegonSyntaxError = "Regon powinien zawierać tylko cyfry!";

        private string PeselLengthErrorMessage = "Pesel powinien składać się z 11 cyfr";
        private string PeselSyntaxError = "Pesel powinien zawierać tylko cyfry!";

        private string PostCodeErrorMessage = "Prosze wpisać kod pocztowy";

        private string CityErrorMessage = "Prosze podać miejscowość";

        private string AddressErrorMessage = "Prosze podać adres";
        
        private string EmailSyntaxError = "Adres E-mail jest niepoprawny";

        private string FatalError = "Dodawanie nie powiodło się. Spróbuj jeszcze raz.";

        #endregion

        public AddWithDraftsProvidersViewModel(IDatabase database)
        {
            userDbRepository = new UserDbRepository(database.Connection);

            AddUserToDatabaseCommand = new RelayCommand(AddUserToDatabase);
            ClearAllFieldsCommand = new RelayCommand(ClearAllFields);

            TypeColection = userDbRepository.TypeColectionMethod();          
            StateColection = userDbRepository.StateColectionMethod();
            CountryColection = userDbRepository.CountryColectionMethod();

            Messenger.Default.Register<User>(this, "AddWithDrafts", this.FillFields);
        }

        #region BindingMethod
        private void AddUserToDatabase()
        {
            try
            {
                #region CheckPropertis
                if (Nip != ParentNip && userDbRepository.CheckReproducibility(Nip))
                {
                    Status = NipReproducibility;
                    ForegroundStatus = "Red";
                    return;
                }
                else if (NameCompany == string.Empty)
                {
                    Status = NameCompanyErrorMessage;
                    ForegroundStatus = "Red";
                    return;
                }
                else if (FirstName == string.Empty)
                {
                    Status = FirstNameErrorMessage;
                    ForegroundStatus = "Red";
                    return;
                }
                else if (LastName == string.Empty)
                {
                    Status = LastNameErrorMessage;
                    ForegroundStatus = "Red";
                    return;
                }
                else if (Type == string.Empty)
                {
                    Status = TypeErrorMessage;
                    ForegroundStatus = "Red";
                    return;
                }
                else if (Nip == string.Empty)
                {
                    Status = NipErrorMessage;
                    ForegroundStatus = "Red";
                    return;
                }
                else if (!Strings.IsDigit(Nip))
                {
                    Status = NipSyntaxError;
                    ForegroundStatus = "Red";
                    return;
                }
                else if (Nip.Length < 10)
                {
                    Status = NipLengthErrorMessage;
                    ForegroundStatus = "IndianRed";
                    return;
                }
                else if (Regon != string.Empty && !Strings.IsDigit(Regon))
                {
                    Status = RegonSyntaxError;
                    ForegroundStatus = "Red";
                    return;
                }
                else if (Regon != string.Empty && Regon.Length < 9)
                {
                    Status = RegonLengthErrorMessage;
                    ForegroundStatus = "IndianRed";
                    return;
                }
                else if (Pesel != string.Empty && !Strings.IsDigit(Pesel))
                {
                    Status = PeselSyntaxError;
                    ForegroundStatus = "Red";
                    return;
                }
                else if (Pesel != string.Empty && Pesel.Length < 11)
                {
                    Status = PeselLengthErrorMessage;
                    ForegroundStatus = "IndianRed";
                    return;
                }
                else if (PostalCode == string.Empty)
                {
                    Status = PostCodeErrorMessage;
                    ForegroundStatus = "Red";
                    return;
                }
                else if (City == string.Empty)
                {
                    Status = CityErrorMessage;
                    ForegroundStatus = "Red";
                    return;
                }
                else if (Address == string.Empty)
                {
                    Status = AddressErrorMessage;
                    ForegroundStatus = "Red";
                    return;
                }
                else if (Email != string.Empty && !Email.Contains("@"))
                {
                    Status = EmailSyntaxError;
                    ForegroundStatus = "Red";
                    return;
                }
                #endregion

                User user = new User()
                {
                    NameCompany = NameCompany,
                    FirstName = FirstName,
                    LastName = LastName,
                    Type = Type,
                    Nip = Nip,
                    Regon = Regon,
                    Pesel = Pesel,
                    PostalCode = PostalCode,
                    City = City,
                    Address = Address,
                    State = State,
                    Country = Country,
                    Email = Email,
                    PhoneNumber = PhoneNumber,
                    WhereIsIt = "Database",
                };

                userDbRepository.DeleteUserInDrafts(ParentNip);
                userDbRepository.Insert(user);

                ClearFields();

                Status = AddMessage;
                ForegroundStatus = "Green";

                IsEnabledAllFields = false;
            }
            catch
            {
                ForegroundStatus = "Red";
                Status = FatalError;
            }
            
        }

        private void ClearAllFields()
        {
            ClearFields();
        }
        #endregion

        #region AnotherMethods
        private void ClearFields()
        {
            NameCompany = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Type = string.Empty;
            Nip = string.Empty;
            Regon = string.Empty;
            Pesel = string.Empty;
            PostalCode = string.Empty;
            City = string.Empty;
            Address = string.Empty;
            State = string.Empty;
            Country = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;

            Status = string.Empty;
        }     

        public override void OnResume()
        {
            base.OnResume();

            IsEnabledAllFields = true;
        }
        private void FillFields(User commingUser)
        {
            NameCompany = commingUser.NameCompany;
            FirstName = commingUser.FirstName;
            LastName = commingUser.LastName;
            Type = commingUser.Type;
            Nip = commingUser.Nip;
            Regon = commingUser.Regon;
            Pesel = commingUser.Pesel;
            PostalCode = commingUser.PostalCode;
            City = commingUser.City;
            Address = commingUser.Address;
            State = commingUser.State;
            Country = commingUser.Country;
            Email = commingUser.Email;
            PhoneNumber = commingUser.PhoneNumber;

            ParentNip = commingUser.Nip;
        }
        #endregion
    }
}
