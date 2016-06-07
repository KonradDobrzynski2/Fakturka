using Data.Repositories;
using System.Collections.ObjectModel;
using Data.Model;
using Data.ViewModel.Another;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using Data.Services;

namespace Data.ViewModel
{
    public class CreateCategoryViewModel : BaseViewModel
    {
        private ProductDbRepository productDbRepository;

        public ICommand CreateCategoryCommand { get; set; }
        public ICommand DeleteCategoryCommand { get; set; }

        #region BindingPropertis
        private ObservableCollection<string> cateogryColection;
        public ObservableCollection<string> CategoryColection
        {
            get { return cateogryColection; }
            set
            {
                Set(ref cateogryColection, value);
            }
        }

        private string selectedValue = string.Empty;
        public string SelectedValue
        {
            get
            {
                return selectedValue;
            }
            set
            {
                Set(ref selectedValue, value);

                if (Status == CreateCategoryMessage || Status == DeleteCategoryMessage || Status == ErrorCreateMessage || Status == ErrorDeleteMessage)
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

        private string nameCategory = string.Empty;
        public string NameCategory
        {
            get { return nameCategory; }
            set
            {
                Set(ref nameCategory, value);

                if (Status == CreateCategoryMessage || Status == DeleteCategoryMessage || Status == ErrorCreateMessage || Status == ErrorDeleteMessage)
                {
                    Status = string.Empty;
                }
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
        #endregion

        #region LocalPropertis
        private string CreateCategoryMessage = "Dodano nową kategorię";

        private string DeleteCategoryMessage = "Wybrana kategoria została usunięta";

        private string ErrorCreateMessage = "Proszę podać nazwę kategorii";

        private string ErrorDeleteMessage = "Proszę wybrać kategorię z listy do usunięcia";

        private string ErrorRepeatabilityMessage = "Proszę podać nazwę kategorii";
        #endregion

        public CreateCategoryViewModel(IDatabase database)
        {
            productDbRepository = new ProductDbRepository(database.Connection);

            CreateCategoryCommand = new RelayCommand(CreateCategory);
            DeleteCategoryCommand = new RelayCommand(DeleteCategory);

            CategoryColection = productDbRepository.CategoryFieldsColectionMethod();
        }

        #region BindingMethod
        private void CreateCategory()
        {
            if (NameCategory != string.Empty)
            {
                try
                {
                    Category_addproducts category_addproducts = new Category_addproducts();
                    category_addproducts.category = NameCategory;

                    productDbRepository.InsertCategory(category_addproducts);

                    OnResume();

                    ForegroundStatus = "Green";
                    NameCategory = string.Empty;
                    Status = CreateCategoryMessage;
                }
                catch
                {
                    ForegroundStatus = "Red";
                    NameCategory = string.Empty;
                    Status = ErrorRepeatabilityMessage;
                }
            }
            else
            {
                ForegroundStatus = "Red";
                Status = ErrorCreateMessage;
            }
        }

        private void DeleteCategory()
        {
            if (selectedValue == null || selectedValue == string.Empty)
            {
                ForegroundStatus = "Red";
                Status = ErrorDeleteMessage;
            }
            else
            {
                productDbRepository.DeleteCategory(SelectedValue);

                OnResume();

                ForegroundStatus = "Green";
                Status = DeleteCategoryMessage;
            }
        }
        #endregion

        #region AnotherMethod
        public override void OnResume()
        {
            base.OnResume();

            CategoryColection = productDbRepository.CategoryFieldsColectionMethod();
        }
        #endregion
    }
}
