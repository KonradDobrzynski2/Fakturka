﻿using Data.Repositories;
using System.Collections.ObjectModel;
using Data.Model;
using Data.ViewModel.Another;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using Data.Services;
using GalaSoft.MvvmLight.Messaging;
using Data.Messengers;
using System;

namespace Data.ViewModel
{
    public class DraftsProvidersViewModel : BaseViewModel
    {
        private INavigation navigation;

        private UserDbRepository userDbRepository;

        public ICommand DeleteUserInDraftsCommand { get; set; }
        public ICommand AddProvidersWithDraftsCommand { get; set; }
        
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

        private string status;
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

        public DraftsProvidersViewModel(IDatabase database)
        {
            navigation = new WindowsNavigation();

            userDbRepository = new UserDbRepository(database.Connection);

            DeleteUserInDraftsCommand = new RelayCommand(DeleteUserInDrafts);
            AddProvidersWithDraftsCommand = new RelayCommand(AddProvidersWithDrafts);

            SearchFildsColection = userDbRepository.UserFieldsColectionMethod();
            searchFild = SearchFildsColection[0];
        }

        #region BindingMethod
        private void Search()
        {
            UserColection = new ObservableCollection<User>(userDbRepository.SearchUsers(SearchFild, SearchText, "Drafts"));

            if (userColection.Count == 0)
            {
                Status = ErrorNotFound;
            }
            else
            {
                Status = string.Empty;
            }
        }

        private void DeleteUserInDrafts()
        {
            if (selectedValue != null)
            {
                userDbRepository.DeleteUserInDrafts(SelectedValue.Nip);
            }
            OnResume();
        }

        private void AddProvidersWithDrafts()
        {
            if (SelectedValue != null)
            {
                navigation.NavigateTo(Type.GetType(string.Format("Data.ViewModel.{0}", "AddWithDraftsProvidersViewModel")));

                Messenger.Default.Send<User>(SelectedValue, "AddWithDrafts");
                Messenger.Default.Send<LocalButtonAndTitle>(new LocalButtonAndTitle() { Visability = "Visible", Title = "Kopie robocze / Dodaj kontrahenta z kopii roboczych" });
            }         
        }
        #endregion

        #region AnotherMethod
        public override void OnResume()
        {
            base.OnResume();

            UserColection = new ObservableCollection<User>(userDbRepository.GetAllUsersInDrafts());
        }
        #endregion
    }
}
