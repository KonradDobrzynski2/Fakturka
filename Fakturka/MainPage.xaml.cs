using Windows.UI.Xaml.Controls;
using Windows.Globalization;
using Data.View;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Text;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Fakturka
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainDocuments Documents { get { return MainDocuments; } }

        public MainProducts Products { get { return MainProducts; } }

        public MainProviders Providers { get { return MainProviders; } }

        public int SelectedItem { get { return Pivot.SelectedIndex; } }

        public MainPage()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "pl";

            this.InitializeComponent();
        }

        // powinno być to jakoś w stylach ale nie ma jużna to czasu...
        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            for (int i = 0; i < Pivot.Items.Count; i++)
            {
                if (i == Pivot.SelectedIndex)
                {
                    PivotItem selectedPivotItem = Pivot.SelectedItem as PivotItem;
                    (selectedPivotItem.Header as TabHeader_TextBlock).Foreground = new SolidColorBrush(Colors.SteelBlue);
                    (selectedPivotItem.Header as TabHeader_TextBlock).FontWeight = FontWeights.Bold;
                }
                else
                {
                    PivotItem unselectedPivotItem = Pivot.Items[i] as PivotItem;
                    //(unselectedPivotItem.Header as TabHeader_TextBlock).Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0xc9, 0xd8, 0xf9));
                    (unselectedPivotItem.Header as TabHeader_TextBlock).FontWeight = FontWeights.Normal;
                    (unselectedPivotItem.Header as TabHeader_TextBlock).Foreground = new SolidColorBrush(Colors.Gray);
                }
            }
        }
    }
}
