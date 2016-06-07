using Data.Repositories;
using System.Collections.ObjectModel;
using Data.Model;
using Data.ViewModel.Another;
using System.Collections.Generic;
using Data.Singletons;
using Data.Services;
using GalaSoft.MvvmLight.Messaging;

namespace Data.ViewModel
{
    public class ProductViewViewModel : BaseViewModel
    {
        private ProductDbRepository productDbRepository;

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
        private string ErrorNotFound = "Nie znaleziono pozycji do wyświetlenia";
        #endregion

        public ProductViewViewModel(IDatabase database)
        {
            productDbRepository = new ProductDbRepository(database.Connection);

            SearchFildsColection = productDbRepository.ProductFieldsColectionMethod();
            searchFild = SearchFildsColection[0];

            Messenger.Default.Register<ObservableCollection<Product>>(this, (x => ProductColection = x ));
        }

        #region BindingMethod
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
        }
        #endregion
    }
}
