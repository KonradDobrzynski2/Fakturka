using Data.Repositories;
using System.Collections.ObjectModel;
using Data.Model;
using Data.ViewModel.Another;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using Data.Singletons;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using System;
using Data.Messengers;
using Data.Services;

namespace Data.ViewModel
{
    public class SelectProductViewModel : BaseViewModel
    {
        private ProductDbRepository productDbRepository;
        private INavigation navigation;

        public ICommand SelectProductCommand { get; set; }
        public ICommand SearchCommand { get; set; }

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
        private string ErrorSelectProduct = "Nie zaznaczono żadnego produktu";

        private string ErrorNotFound = "Nie znaleziono pozycji do wyświetlenia";
        #endregion

        public SelectProductViewModel(IDatabase database)
        {
            productDbRepository = new ProductDbRepository(database.Connection);
            navigation = new WindowsNavigation();         
            
            SelectProductCommand = new RelayCommand(SelectProduct);
            SearchCommand = new RelayCommand(Search);

            SearchFildsColection = productDbRepository.ProductFieldsColectionMethod();
            searchFild = SearchFildsColection[0];
        }

        #region BindingMethod
        private void SelectProduct()
        {
            if (SelectedValue != null)
            {
                navigation.NavigateTo(Type.GetType(string.Format("Data.ViewModel.{0}", "AddDocumentsViewModel")));

                Messenger.Default.Send<LocalButtonAndTitle>(new LocalButtonAndTitle() { Visability = "Collapsed", Title = "Dodaj dokument" });

                Messenger.Default.Send<Product>( SelectedValue );
            }
            else
            {
                Status = ErrorSelectProduct;
            }
        }

        private void Search()
        {
            ProductColection = new ObservableCollection<Product>(productDbRepository.SearchUsers(SearchFild, SearchText, "Database"));

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
