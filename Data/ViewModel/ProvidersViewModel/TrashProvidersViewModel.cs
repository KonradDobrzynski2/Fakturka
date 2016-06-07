using Data.ViewModel.Another;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Data.AnotherClass;
using System.Collections.Generic;
using Data.Repositories;
using System.Collections.ObjectModel;
using Data.Model;
using Data.Services;

namespace Data.ViewModel
{
    public class TrashProvidersViewModel : BaseViewModel
    {
        private UserDbRepository userDbRepository;

        public ICommand DeleteUserInTrashCommand { get; set; }
        public ICommand RestoreUserInTrashCommand { get; set; }

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

        private string searchFild = string.Empty;
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
        private string ErrorNotFound = "Nie znaleziono pozycji do wyświetlenia";
        #endregion

        public TrashProvidersViewModel(IDatabase database)
        {
            userDbRepository = new UserDbRepository(database.Connection);

            RestoreUserInTrashCommand = new RelayCommand(RestoreUserInTrash);
            DeleteUserInTrashCommand = new RelayCommand(DeleteUserInTrash);

            SearchFildsColection = userDbRepository.UserFieldsColectionMethod();
            searchFild = SearchFildsColection[0];
        }

        #region BindingMethod
        private void Search()
        {
            UserColection = new ObservableCollection<User>(userDbRepository.SearchUsers(SearchFild, SearchText, "Trash"));

            if (UserColection.Count == 0)
            {
                Status = ErrorNotFound;
            }
            else
            {
                Status = string.Empty;
            }
        }

        private void DeleteUserInTrash()
        {
            userDbRepository.DeleteUserInTrash(SelectedValue.Nip);

            OnResume();
        }

        private void RestoreUserInTrash()
        {
            userDbRepository.RestoreUserInTrash(SelectedValue.Nip);

            OnResume();
        }
        #endregion

        #region AnotherMethod
        public override void OnResume()
        {
            base.OnResume();

            UserColection = new ObservableCollection<User>(userDbRepository.GetAllUsersInTrash());
        }
        #endregion
    }
}
