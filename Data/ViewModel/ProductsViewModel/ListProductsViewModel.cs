using Data.Repositories;
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
    public class ListProductsViewModel : BaseViewModel
    {
        private INavigation navigation;

        private ProductDbRepository productDbRepository;

        public ICommand DeleteProductCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand EditProductCommand { get; set; }

        #region BindingPropertis
        private ObservableCollection<Product> productColection;
        public ObservableCollection<Product> ProductColection
        {
            get { return productColection; }
            set
            {
                Set(ref productColection, value);
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
        private string ErrorDeleteUser = "Nie wybrano pozycji do usunięcia";

        private string ErrorNotFound = "Nie znaleziono pozycji do wyświetlenia";
        #endregion

        public ListProductsViewModel(IDatabase database)
        {
            navigation = new WindowsNavigation();

            productDbRepository = new ProductDbRepository(database.Connection);

            DeleteProductCommand = new RelayCommand(DeleteProduct);
            SearchCommand = new RelayCommand(Search);
            EditProductCommand = new RelayCommand(EditProduct);

            SearchFildsColection = productDbRepository.ProductFieldsColectionMethod();
            searchFild = SearchFildsColection[0];
        }

        #region BindingMethod
        private void DeleteProduct()
        {
            if (SelectedValue != null)
            {
                productDbRepository.DeleteProductInDatabase(SelectedValue.Code);

                OnResume();
            }
            else
            {
                Status = ErrorDeleteUser;
            }     
        }

        private void EditProduct()
        {
            if (SelectedValue != null)
            {
                navigation.NavigateTo(Type.GetType(string.Format("Data.ViewModel.{0}", "EditWithListProductsViewModel")));

                Messenger.Default.Send<Product>(SelectedValue, "AddWithList");
                Messenger.Default.Send<LocalButtonAndTitle>(new LocalButtonAndTitle() { Visability = "Visible", Title = "Lista prduktów / Edytuj produkt" });
            }
        }

        private void Search()
        {
            ProductColection = new ObservableCollection<Product>(productDbRepository.SearchUsers(SearchFild,SearchText,"Database"));

            if (productColection.Count == 0)
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

            ProductColection = new ObservableCollection<Product>(productDbRepository.GetAllProductsInDatabase());
        }
        #endregion
    }
}
