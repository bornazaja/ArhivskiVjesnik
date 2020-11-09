using Caliburn.Micro;

namespace ArhivskiVjesnikWPF.ViewModels
{
    public class ContentControlInfoViewModel : Screen
    {
        public object ViewModel { get; set; }

        public void OK()
        {
            TryClose();
        }
    }
}
