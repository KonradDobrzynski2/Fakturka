using Data.Messengers;
using Data.Services;
using Data.ViewModel.Another;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows.Input;

namespace Data.ViewModel
{
    public class MainProductsViewModel :BaseViewModel
    {
        private INavigation navigation;

        public ICommand HamburgerCommand { get; set; }
        public ICommand BackButtonNavigateCommand { get; set; }
        public ICommand MainButtonNavigateCommand { get; set; }

        #region BindingPropertis
        private bool splitview_status;
        public bool SplitView_Status
        {
            get { return splitview_status; }
            set { Set(ref splitview_status, value); }
        }

        private string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { Set(ref title, value); }
        }

        private string visibility = "Collapsed";
        public string Visibility
        {
            get { return visibility; }
            set { Set(ref visibility, value); }
        }
        #endregion

        public MainProductsViewModel()
        {
            navigation = new WindowsNavigation();

            HamburgerCommand = new RelayCommand(Hamburger);
            BackButtonNavigateCommand = new RelayCommand(BackButtonNavigate);
            MainButtonNavigateCommand = new RelayCommand<string>(viewModelType => MainButtonNavigate(viewModelType));

            Messenger.Default.Register<LocalButtonAndTitle>(this, this.CheckVisability);

            Messenger.Default.Register<int>(this, "SelectedItem_1", this.OnSelectedItemChanged);
        }

        #region BindingMethod
        public void Hamburger()
        {
            SplitView_Status = !SplitView_Status;
        }

        public void BackButtonNavigate()
        {
            string viewModelType = string.Empty;

            if (Title != null && Title.Contains("Kopie robocze / Dodaj produkt z kopii roboczych / Zarządzaj kategoriami (+/-)"))
            {
                viewModelType = "AddWithDraftsProductsViewModel";
                Title = "Kopie robocze / Dodaj produkt z kopii roboczych";
            }
            else if (Title != null && Title.Contains("Lista produktów / Edytuj produkt / Zarządzaj kategoriami (+/-)"))
            {
                viewModelType = "EditWithListProductsViewModel";
                Title = "Lista produktów / Edytuj produkt";
            }
            else if (Title != null && Title.Contains("Kopie robocze / Dodaj produkt z kopii roboczych"))
            {
                viewModelType = "DraftsProductsViewModel";
                Title = "Kopie robiocze";
                Visibility = "Collapsed";
            }
            else if (Title != null && Title.Contains("Dodaj produkt"))
            {
                viewModelType = "AddProductsViewModel";
                Title = "Dodaj produkt";
                Visibility = "Collapsed";
            }
            else if (Title != null && (Title.Contains("Lista produktów") || Title.Contains("Edytuj produkt")))
            {
                viewModelType = "ListProductsViewModel";
                Title = "Lista produktów";
                Visibility = "Collapsed";
            }
            else if (Title != null && Title.Contains("Kopie robocze"))
            {
                viewModelType = "DraftsProductsViewModel";
                Title = "Kopie robiocze";
                Visibility = "Collapsed";
            }
            else if (Title != null && Title.Contains("Kosz"))
            {
                viewModelType = "TrashProductsViewModel";
                Title = "Kosz";
                Visibility = "Collapsed";
            }

            navigation.NavigateTo(Type.GetType(string.Format("Data.ViewModel.{0}", viewModelType)));
        }

        public void MainButtonNavigate(string viewModelType)
        {
            navigation.NavigateTo(Type.GetType(string.Format("Data.ViewModel.{0}", viewModelType)));

            switch (viewModelType)
            {
                case "AddProductsViewModel":
                    Title = "Dodaj produkt";
                    break;
                case "ListProductsViewModel":
                    Title = "Lista produktów";
                    break;
                case "DraftsProductsViewModel":
                    Title = "Kopie robiocze";
                    break;
                case "TrashProductsViewModel":
                    Title = "Kosz";
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region AnotherMethod
        private void CheckVisability(LocalButtonAndTitle message)
        {
            Visibility = message.Visability;

            Title = message.Title;
        }

        public void OnSelectedItemChanged(int message)
        {
            navigation.NavigateTo(Type.GetType(string.Format("Data.ViewModel.{0}", "AddProductsViewModel")), true);

            switch (navigation.FrameContent)
            {
                case "AddProducts":
                    Title = "Dodaj produkt";
                    Visibility = "Collapsed";
                    break;
                case "ListProducts":
                    Title = "Lista produktów";
                    Visibility = "Collapsed";
                    break;
                case "DraftsProducts":
                    Title = "Kopie robiocze";
                    Visibility = "Collapsed";
                    break;
                case "TrashProducts":
                    Title = "Kosz";
                    Visibility = "Collapsed";
                    break;
                case "CreateCategory":
                    Title = "Dodaj produkt / Zarządzaj kategoriami (+/-)";
                    break;
                case "AddWithDraftsProducts":
                    Title = "Kopie robocze / Dodaj produkt z kopii roboczych";
                    break;
                case "EditWithListProducts":
                    Title = "Dodaj dokument / Wybór produktu";
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
