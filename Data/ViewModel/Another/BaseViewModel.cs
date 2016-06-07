using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModel.Another
{
    public class BaseViewModel :ViewModelBase
    {
        private ViewState states;
        public ViewState States
        {
            get
            {
                if (states == null)
                {
                    states = ServiceLocator.Current.GetInstance<ViewState>();
                }

                return states;
            }
        }

        public virtual void OnResume()
        {
        }
    }
}
