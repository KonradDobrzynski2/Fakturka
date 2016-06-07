using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModel.Another
{
    public class MainPageViewModel : BaseViewModel
    {
        private int selectedItem;
        public int SelectedItem
        {
            get { return selectedItem; }
            set
            {
                Set(ref selectedItem, value);
                Messenger.Default.Send(SelectedItem, "SelectedItem_"+ SelectedItem);
            }
        }

        public MainPageViewModel()
        {
            SelectedItem = 0;
        }
    }
}
