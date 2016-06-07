using Data.ViewModel.Another;

namespace Data.Messengers
{
    public class LocalButtonAndTitle : BaseViewModel
    {
        private string visability;
        public string Visability
        {
            get
            {
                return visability;
            }

            set
            {
                Set(ref visability, value);
            }
        }

        private string title;

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                Set(ref title, value);
            }
        }
    }
}
