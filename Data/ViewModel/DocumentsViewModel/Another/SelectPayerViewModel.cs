using Data.Repositories;
using System.Collections.ObjectModel;
using Data.Model;
using Data.ViewModel.Another;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using Data.Singletons;
using System;
using Data.Messengers;
using GalaSoft.MvvmLight.Messaging;
using Data.Services;

namespace Data.ViewModel
{
    public class SelectPayerViewModel : BaseViewModel
    {
        private UserDbRepository userDbRepository;
        private INavigation navigation;

        public ICommand SelectUserCommand { get; set; }

        #region BindingPropertis
        private ObservableCollection<User> userColection;
        public ObservableCollection<User> UserColection
        {
            get { return userColection; }
            set
            {
                Set(ref userColection, value);
            }
        }

        private User selectedValue;
        public User SelectedValue
        {
            get
            {
                return selectedValue;
            }
            set
            {
                Status = string.Empty;
                Set(ref selectedValue, value);
            }
        }

        private ObservableCollection<string> searchFildsColection;
        public ObservableCollection<string> SearchFildsColection
        {
            get { return searchFildsColection; }
            set
            {
                Set(ref searchFildsColection, value);
            }
        }

        private string searchFild;
        public string SearchFild
        {
            get
            {
                return searchFild;
            }
            set
            {
                 Set(ref searchFild, value);

                Search();
            }
        }

        private string searchText = string.Empty;
        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                Set(ref searchText, value);

                if (value == string.Empty)
                {
                    OnResume();
                    Status = string.Empty;
                }

                Search();
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
        #endregion

        #region LocalPropertis
        private string ErrorSelectUser = "Nie zaznaczono żadnego użytkownika";

        private string ErrorNotFound = "Nie znaleziono pozycji do wyświetlenia";
        #endregion

        public SelectPayerViewModel(IDatabase database)
        {
            userDbRepository = new UserDbRepository(database.Connection);
            navigation = new WindowsNavigation();

            SelectUserCommand = new RelayCommand(SelectUser);

            SearchFildsColection = userDbRepository.UserFieldsColectionMethod();
            searchFild = SearchFildsColection[0];
        }

        #region BindingMethod
        private void SelectUser()
        {
            if (SelectedValue != null)
            {
                navigation.NavigateTo(Type.GetType(string.Format("Data.ViewModel.{0}", "AddDocumentsViewModel")));

                Messenger.Default.Send<LocalButtonAndTitle>(new LocalButtonAndTitle() { Visability = "Collapsed", Title = "Dodaj dokument" });

                Messenger.Default.Send<User>(SelectedValue, "Payer");
            }
            else
            {
                Status = ErrorSelectUser;
            }     
        }

        private void Search()
        {
            UserColection = new ObservableCollection<User>(userDbRepository.SearchUsers(SearchFild,SearchText,"Database"));

            if (userColection.Count == 0)
            {
                Status = ErrorNotFound;
            }
            else
            {
                Status = string.Empty;
            }
        }
        #endregion

        #region AnotherMethod
        public override void OnResume()
        {
            base.OnResume();

            UserColection = new ObservableCollection<User>(userDbRepository.GetAllUsersInDatabase());
        }
        #endregion
    }
}
