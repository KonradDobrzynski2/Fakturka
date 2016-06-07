using Data.Model;
using Data.Repositories;
using Data.ViewModel.Another;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Data.Singletons;
using System;
using Data.Messengers;
using GalaSoft.MvvmLight.Messaging;
using Data.Services;

namespace Data.ViewModel
{
    public class TrashDocumentsViewModel : BaseViewModel
    {
        private DocumentDbRepository documentDbRepository;
        private DocumentProductcsDbRepository documentProductcsDbRepository;
        private INavigation navigation;

        public ICommand RestoreProductInTrashCommand { get; set; }
        public ICommand DeleteDocumenttInTrashCommand { get; set; }
        public ICommand NavigateCommand { get; set; }

        #region BindingPropertis
        private ObservableCollection<Document> documentColection;
        public ObservableCollection<Document> DocumentColection
        {
            get { return documentColection; }
            set
            {
                Set(ref documentColection, value);
            }
        }

        private Document selectedValue;
        public Document SelectedValue
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
        private string ErrorDeleteDocument = "Nie wybrano pozycji do usunięcia";

        private string ErrorNotFound = "Nie znaleziono pozycji do wyświetlenia";
        #endregion

        public TrashDocumentsViewModel(IDatabase database)
        {
            documentDbRepository = new DocumentDbRepository(database.Connection);
            documentProductcsDbRepository = new DocumentProductcsDbRepository(database.Connection);
            navigation = new WindowsNavigation();

            RestoreProductInTrashCommand = new RelayCommand(RestoreProductInTrash);
            DeleteDocumenttInTrashCommand = new RelayCommand(DeleteDocumenttInTrash);
            NavigateCommand = new RelayCommand<string>(ID_Document => Navigate(ID_Document));

            SearchFildsColection = documentDbRepository.DocumentsFieldsColectionMethod();
            searchFild = SearchFildsColection[0];

            Singleton_ConnectionValue.Instance.SQLiteConnection = database.Connection;
        }

        #region BindingMethod
        private void DeleteDocumenttInTrash()
        {
            if (SelectedValue != null)
            {
                documentDbRepository.DeleteDokumentInTrash(SelectedValue.ID);

                OnResume();
            }
            else
            {
                Status = ErrorDeleteDocument;
            }
        }

        private void RestoreProductInTrash()
        {
            if (SelectedValue != null)
            {
                documentDbRepository.RestoreDokumentInTrash(SelectedValue.NameDocument);

                OnResume();
            }
            else
            {
                Status = ErrorDeleteDocument;
            }
        }

        private void Navigate(string ID_Document)
        {
            if (selectedValue != null)
            {
                navigation.NavigateTo(Type.GetType(string.Format("Data.ViewModel.{0}", "ProductViewViewModel")));

                Messenger.Default.Send<LocalButtonAndTitle>(new LocalButtonAndTitle() { Visability = "Visible", Title = "Kosz / Lista towarów na fakturze nr. " + documentDbRepository.GetDocument(Int32.Parse(ID_Document)).NameDocument});

                Messenger.Default.Send<ObservableCollection<Product>>(documentProductcsDbRepository.GetProductsListInDocument(Int32.Parse(ID_Document)));
            }
        }

        private void Search()
        {
            DocumentColection = new ObservableCollection<Document>(documentDbRepository.SearchDokuments(SearchFild, SearchText, "Database"));

            if (DocumentColection.Count == 0)
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

            DocumentColection = new ObservableCollection<Document>(documentDbRepository.GetAllDocumentsInTrash());
        }
        #endregion
    }
}
