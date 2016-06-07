using Fakturka;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Data.Services
{
    public class WindowsNavigation : INavigation
    {
        private string frameContent;

        public string FrameContent
        {
            get { return frameContent; }
            set { frameContent = value; }
        }


        public void NavigateTo(Type viewModelType, bool onlyIfContentIsNull = false)
        {
            Frame frame = null;

            if(Window.Current.Content is MainPage)
            {
                var main = Window.Current.Content as MainPage;

                switch(main.SelectedItem)
                {
                    case 0:
                        frame = main.Documents.RootFrame;
                        break;
                    case 1:
                        frame = main.Products.RootFrame;
                        break;
                    case 2:
                        frame = main.Providers.RootFrame;
                        break;
                    case 3:
                        break;
                }
            }

            // okreslic ktory z glownych widokow jest widoczny
            //frame = property MyFrame z jednego z glownych widokow w pivocie

            if (!onlyIfContentIsNull || frame.Content == null)
            {

                var pageTypeString = viewModelType.ToString()
                    .Replace("Data.ViewModel", "Data.View")
                    .Replace("ViewModel", string.Empty);

                var pageType = Type.GetType(pageTypeString);

                frame.Navigate(pageType);
            }

            FrameContent = frame.Content.ToString().Remove(0, 10);
        }

        public void GoBack()
        {
            var frame = Window.Current.Content as Frame;

            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }
    }
}
