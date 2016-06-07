using Data.Services;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Data.ViewModel.Another
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IDatabase, WindowsDatabase>();

            SimpleIoc.Default.Register<MainPageViewModel>();

            SimpleIoc.Default.Register<MainDocumentsViewModel>();
            SimpleIoc.Default.Register<AddDocumentsViewModel>();
            SimpleIoc.Default.Register<ListDocumentsViewModel>();
            SimpleIoc.Default.Register<TrashDocumentsViewModel>();
            SimpleIoc.Default.Register<SelectRecipientViewModel>();
            SimpleIoc.Default.Register<SelectPayerViewModel>();
            SimpleIoc.Default.Register<SelectProductViewModel>();
            SimpleIoc.Default.Register<ProductViewViewModel>();

            SimpleIoc.Default.Register<MainProductsViewModel>();
            SimpleIoc.Default.Register<AddProductsViewModel>();
            SimpleIoc.Default.Register<ListProductsViewModel>();
            SimpleIoc.Default.Register<DraftsProductsViewModel>();
            SimpleIoc.Default.Register<TrashProductsViewModel>();
            SimpleIoc.Default.Register<CreateCategoryViewModel>();
            SimpleIoc.Default.Register<AddWithDraftsProductsViewModel>();
            SimpleIoc.Default.Register<EditWithListProductsViewModel>();

            SimpleIoc.Default.Register<MainProvidersViewModel>();
            SimpleIoc.Default.Register<AddProvidersViewModel>();
            SimpleIoc.Default.Register<ListProvidersViewModel>();
            SimpleIoc.Default.Register<DraftsProvidersViewModel>();
            SimpleIoc.Default.Register<TrashProvidersViewModel>();
            SimpleIoc.Default.Register<AddWithDraftsProvidersViewModel>();
            SimpleIoc.Default.Register<EditWithListProvidersViewModel>();
        }

        public MainPageViewModel MainPageViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<MainPageViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }

        public MainDocumentsViewModel MainDocumentsViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<MainDocumentsViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }
        public AddDocumentsViewModel AddDocumentsViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<AddDocumentsViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }
        public ListDocumentsViewModel ListDocumentsViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<ListDocumentsViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }
        public TrashDocumentsViewModel TrashDocumentsViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<TrashDocumentsViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }
        public SelectRecipientViewModel SelectRecipientViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<SelectRecipientViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }
        public SelectPayerViewModel SelectPayerViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<SelectPayerViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }
        public SelectProductViewModel SelectProductViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<SelectProductViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }
        public ProductViewViewModel ProductViewViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<ProductViewViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }

        public MainProductsViewModel MainProductsViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<MainProductsViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }
        public AddProductsViewModel AddProductsViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<AddProductsViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }
        public ListProductsViewModel ListProductsViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<ListProductsViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }
        public DraftsProductsViewModel DraftsProductsViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<DraftsProductsViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }
        public TrashProductsViewModel TrashProductsViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<TrashProductsViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }
        public CreateCategoryViewModel CreateCategoryViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<CreateCategoryViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }
        public AddWithDraftsProductsViewModel AddWithDraftsProductsViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<AddWithDraftsProductsViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }
        public EditWithListProductsViewModel EditWithListProductsViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<EditWithListProductsViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }

        public MainProvidersViewModel MainProvidersViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<MainProvidersViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }
        public AddProvidersViewModel AddProvidersViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<AddProvidersViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }
        public ListProvidersViewModel ListProvidersViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<ListProvidersViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }
        public DraftsProvidersViewModel DraftsProvidersViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<DraftsProvidersViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }
        public TrashProvidersViewModel TrashProvidersViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<TrashProvidersViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }
        public AddWithDraftsProvidersViewModel AddWithDraftsProvidersViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<AddWithDraftsProvidersViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }
        public EditWithListProvidersViewModel EditWithListProvidersViewModel
        {
            get
            {
                var viewModel = ServiceLocator.Current.GetInstance<EditWithListProvidersViewModel>();
                viewModel.OnResume();
                return viewModel;
            }
        }
    }
}
