using Data.ViewModel.Another;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Data.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainDocuments : Page
    {
        public Frame RootFrame
        {
            get
            {
                return MyFrame;
            }
        }

        public MainDocuments()
        {
            this.InitializeComponent();
        }
    }
}
