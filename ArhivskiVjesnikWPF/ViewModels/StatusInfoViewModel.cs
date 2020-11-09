using Caliburn.Micro;

namespace ArhivskiVjesnikWPF.ViewModels
{
    public class StatusInfoViewModel : Screen
    {
        public string Message { get; set; }

        public void OK()
        {
            TryClose();
        }
    }
}
