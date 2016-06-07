using Data.Messengers;
using Data.Services;
using Data.ViewModel.Another;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows.Input;

namespace Data.ViewModel
{
    public class MainProvidersViewModel : BaseViewModel
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

        public MainProvidersViewModel()
        {
            navigation = new WindowsNavigation();

            HamburgerCommand = new RelayCommand(Hamburger);
            BackButtonNavigateCommand = new RelayCommand(BackButtonNavigate);
            MainButtonNavigateCommand = new RelayCommand<string>(viewModelType => MainButtonNavigate(viewModelType));

            Messenger.Default.Register<LocalButtonAndTitle>(this, this.CheckVisability);

            Messenger.Default.Register<int>(this, "SelectedItem_2", this.OnSelectedItemChanged);
        }

        #region BindingMethod
        public void Hamburger()
        {
            SplitView_Status = !SplitView_Status;
        }

        public void BackButtonNavigate()
        {
            string viewModelType = string.Empty;

            if (Title.Contains("Kopie robocze / Dodaj kontrahenta"))
            {
                viewModelType = "DraftsProvidersViewModel";
                Title = "Kopie robiocze";
            }
            else if (Title != null && Title.Contains("Dodaj kontrahenta"))
            {
                viewModelType = "AddProvidersViewModel";
                Title = "Dodaj kontrahenta";
            }
            else if (Title != null && (Title.Contains("Lista kontrahentów") || Title.Contains("Edytuj kontrahenta")))
            {
                viewModelType = "ListProvidersViewModel";
                Title = "Lista kontrahentów";
            }
            else if (Title != null && (Title.Contains("Kopie robocze")))
            {
                viewModelType = "DraftsProvidersViewModel";
                Title = "Kopie robiocze";
            }
            else if (Title != null && Title.Contains("Kosz"))
            {
                viewModelType = "TrashProvidersViewModel";
                Title = "Kosz";
            }

            navigation.NavigateTo(Type.GetType(string.Format("Data.ViewModel.{0}", viewModelType)));

            Visibility = "Collapsed";
        }

        public void MainButtonNavigate(string viewModelType)
        {
            navigation.NavigateTo(Type.GetType(string.Format("Data.ViewModel.{0}", viewModelType)));

            switch (viewModelType)
            {
                case "AddProvidersViewModel":
                    Title = "Dodaj kontrahenta";
                    break;
                case "ListProvidersViewModel":
                    Title = "Lista kontrahentów";
                    break;
                case "DraftsProvidersViewModel":
                    Title = "Kopie robiocze";
                    break;
                case "TrashProvidersViewModel":
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
            navigation.NavigateTo(Type.GetType(string.Format("Data.ViewModel.{0}", "AddProvidersViewModel")), true);

            switch (navigation.FrameContent)
            {
                case "AddProviders":
                    Title = "Dodaj kontrahenta";
                    Visibility = "Collapsed";
                    break;
                case "ListProviders":
                    Title = "Lista kontrahentów";
                    Visibility = "Collapsed";
                    break;
                case "DraftsProviders":
                    Title = "Kopie robiocze";
                    Visibility = "Collapsed";
                    break;
                case "TrashProviders":
                    Title = "Kosz";
                    Visibility = "Collapsed";
                    break;
                case "AddWithDraftsProviders":
                    Title = "Lista kontrahentów / Edytuj kontrahenta";
                    break;
                case "EditWithListProviders":
                    Title = "Kopie robocze / Dodaj kontrahenta z kopii roboczych";
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
