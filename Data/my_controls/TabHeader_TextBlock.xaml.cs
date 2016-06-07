using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Fakturka
{
    public sealed partial class TabHeader_TextBlock : UserControl
    {
        public static readonly DependencyProperty _Text1 = DependencyProperty.Register("Text", typeof(string),
            typeof(TabHeader_TextBlock), null);

        public string Text
        {
            get { return GetValue(_Text1) as string; }
            set { SetValue(_Text1, value); }
        }

        public static readonly DependencyProperty _Text2 = DependencyProperty.Register("Icon", typeof(string),
            typeof(TabHeader_TextBlock), null);

        public string Icon
        {
            get { return GetValue(_Text2) as string; }
            set { SetValue(_Text2, value); }
        }

        public static readonly DependencyProperty _Text3 = DependencyProperty.Register("Font", typeof (string),
            typeof (TabHeader_TextBlock), null);

        public string Font
        {
            get { return GetValue(_Text3) as string; }
            set { SetValue(_Text3, value); }
        }
        
        public TabHeader_TextBlock()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }
    }
}
