using Data.Services;
using Data.ViewModel.Another;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using Data.Messengers;
using System.Windows.Input;

namespace Data.ViewModel
{
    public class MainDocumentsViewModel : BaseViewModel
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

        public MainDocumentsViewModel()
        {
            navigation = new WindowsNavigation();

            HamburgerCommand = new RelayCommand(Hamburger);
            BackButtonNavigateCommand = new RelayCommand(BackButtonNavigate);
            MainButtonNavigateCommand = new RelayCommand<string>(viewModelType => MainButtonNavigate(viewModelType));

            Messenger.Default.Register<LocalButtonAndTitle>(this, this.CheckVisability);

            Messenger.Default.Register<int>(this,"SelectedItem_0", this.OnSelectedItemChanged);
        }

        #region BindingMethod
        public void Hamburger()
        {
            SplitView_Status = !SplitView_Status;  
        }

        public void BackButtonNavigate()
        {
            string viewModelType = string.Empty;

            if (Title != null && Title.Contains("Dodaj dokument"))
            {
                viewModelType = "AddDocumentsViewModel";
                Title = "Dodaj dokument";
            }
            else if (Title != null && Title.Contains("Lista dokumentów"))
            {
                viewModelType = "ListDocumentsViewModel";
                Title = "Lista dokumentów";

            }
            else if (Title != null && Title.Contains("Kopie robocze"))
            {
                viewModelType = "DraftsDocumentsViewModel";
                Title = "Kopie robiocze";
            }
            else if (Title != null && Title.Contains("Kosz"))
            {
                viewModelType = "TrashDocumentsViewModel";
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
                case "AddDocumentsViewModel":
                    Title = "Dodaj dokument";
                    break;
                case "ListDocumentsViewModel":
                    Title = "Lista dokumentów";
                    break;
                case "DraftsDocumentsViewModel":
                    Title = "Kopie robiocze";
                    break;
                case "TrashDocumentsViewModel":
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
            navigation.NavigateTo(Type.GetType(string.Format("Data.ViewModel.{0}", "AddDocumentsViewModel")),true);

            switch (navigation.FrameContent)
            {
                case "AddDocuments":
                    Title = "Dodaj dokument";
                    Visibility = "Collapsed";
                    break;
                case "ListDocuments":
                    Title = "Lista dokumentów";
                    Visibility = "Collapsed";
                    break;
                case "DraftsDocuments":
                    Title = "Kopie robiocze";
                    Visibility = "Collapsed";
                    break;
                case "TrashDocuments":
                    Title = "Kosz";
                    Visibility = "Collapsed";
                    break;
                case "SelectPayer":
                    Title = "Dodaj dokument / Wybór płatnika";
                    break;
                case "SelectRecipient":
                    Title = "Dodaj dokument / Wybór odbiorcy";
                    break;
                case "SelectProduct":
                    Title = "Dodaj dokument / Wybór produktu";
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
