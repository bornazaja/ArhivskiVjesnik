using ArhivskiVjesnikWPF.Commands;
using System.Windows.Input;

namespace ArhivskiVjesnikWPF.ViewModels
{
    public class CommandHandlerViewModel
    {
        private ICommand _urlCommand;

        public ICommand URLCommand
        {
            get
            {
                if (_urlCommand == null)
                {
                    _urlCommand = new DelegateCommand(x => true, (parameter) =>
                     {
                         string url = parameter as string;
                         System.Diagnostics.Process.Start(url);
                     });
                }
                return _urlCommand;
            }
        }
    }
}
