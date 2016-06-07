using System;

namespace Data.Services
{
    public interface INavigation
    {
        string FrameContent { get; set; }

        void NavigateTo(Type viewModelType, bool onlyIfContentIsNull = false);
        void GoBack();
    }
}
