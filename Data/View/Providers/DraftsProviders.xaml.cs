using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Data.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DraftsProviders : Page
    {
        public DraftsProviders()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (ListView.SelectedValue != null)
            {
                Frame.Navigate(typeof(AddWithDraftsProviders));
            }
        }
    }
}
