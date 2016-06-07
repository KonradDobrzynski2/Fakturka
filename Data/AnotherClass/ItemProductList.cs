using Data.ViewModel.Another;

namespace Data.AnotherClass
{
    public class ItemProductList : BaseViewModel
    {
        private string nameProduct;
        public string NameProduct
        {
            get { return nameProduct; }

            set
            {
                Set(ref nameProduct, value);
            }
        }

        private int quantity;
        public int Quantity
        {
            get { return quantity; }

            set
            {
                Set(ref quantity, value);
            }
        }
    }
}
