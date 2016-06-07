using Data.ViewModel.Another;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using Data.Repositories;
using System.Collections.ObjectModel;
using Data.Model;
using Data.Services;

namespace Data.ViewModel
{
    public class TrashProductsViewModel : BaseViewModel
    {
        private ProductDbRepository productDbRepository;

        public ICommand DeleteProductInTrashCommand { get; set; }
        public ICommand RestoreProductInTrashCommand { get; set; }

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

        public TrashProductsViewModel(IDatabase database)
        {
            productDbRepository = new ProductDbRepository(database.Connection);

            RestoreProductInTrashCommand = new RelayCommand(RestoreProductInTrash);
            DeleteProductInTrashCommand = new RelayCommand(DeleteProductInTrash);

            SearchFildsColection = productDbRepository.ProductFieldsColectionMethod();
            searchFild = SearchFildsColection[0];
        }

        #region BindingMethod
        private void Search()
        {
            ProductColection = new ObservableCollection<Product>(productDbRepository.SearchUsers(SearchFild, SearchText, "Trash"));

            if (productColection.Count == 0)
            {
                Status = ErrorNotFound;
            }
            else
            {
                Status = string.Empty;
            }
        }

        private void DeleteProductInTrash()
        {
            productDbRepository.DeleteProductInTrash(SelectedValue.Code);

            OnResume();
        }

        private void RestoreProductInTrash()
        {
            productDbRepository.RestoreUserInTrash(SelectedValue.Code);

            OnResume();
        }
        #endregion

        #region AnotherMethod
        public override void OnResume()
        {
            base.OnResume();

            ProductColection = new ObservableCollection<Product>(productDbRepository.GetAllProductsInTrash());
        }
        #endregion
    }
}
