using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Fakturka
{
    public sealed partial class ListViewItem_providers : UserControl
    {
        public static readonly DependencyProperty _Text1 = DependencyProperty.Register("NameCompany", typeof(string),
            typeof(ListViewItem_providers), null);

        public string NameCompany
        {
            get { return GetValue(_Text1) as string; }
            set { SetValue(_Text1, value); }
        }

        public static readonly DependencyProperty _Text2 = DependencyProperty.Register("FirstName", typeof(string),
            typeof(ListViewItem_providers), null);

        public string FirstName
        {
            get { return GetValue(_Text2) as string; }
            set { SetValue(_Text2, value); }
        }

        public static readonly DependencyProperty _Text3 = DependencyProperty.Register("LastName", typeof(string),
            typeof(ListViewItem_providers), null);

        public string LastName
        {
            get { return GetValue(_Text3) as string; }
            set { SetValue(_Text3, value); }
        }

        public static readonly DependencyProperty _Text4 = DependencyProperty.Register("Type", typeof(string),
            typeof(ListViewItem_providers), null);

        public string Type
        {
            get { return GetValue(_Text4) as string; }
            set { SetValue(_Text4, value); }
        }

        public static readonly DependencyProperty _Text5 = DependencyProperty.Register("Nip", typeof(string),
            typeof(ListViewItem_providers), null);

        public string Nip
        {
            get { return GetValue(_Text5) as string; }
            set { SetValue(_Text5, value); }
        }

        public static readonly DependencyProperty _Text6 = DependencyProperty.Register("Regon", typeof(string),
            typeof(ListViewItem_providers), null);

        public string Regon
        {
            get { return GetValue(_Text6) as string; }
            set { SetValue(_Text6, value); }
        }

        public static readonly DependencyProperty _Text7 = DependencyProperty.Register("Pesel", typeof(string),
            typeof(ListViewItem_providers), null);

        public string Pesel
        {
            get { return GetValue(_Text7) as string; }
            set { SetValue(_Text7, value); }
        }

        public ListViewItem_providers()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }
    }
}
