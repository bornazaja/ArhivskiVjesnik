using ArhivskiVjesnikLibrary.BLL.DTO;
using System.Windows.Input;

namespace ArhivskiVjesnikWPF.ViewModels
{
    public class ClanciDataGridViewModel : DataGridViewModelBase<ClanakDto>
    {
        public ClanciDataGridViewModel(CommandHandlerViewModel commandHandlerViewModel)
        {
            URLCommand = commandHandlerViewModel.URLCommand;
        }

        public ICommand URLCommand { get; set; }
    }
}
