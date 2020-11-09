using ArhivskiVjesnikLibrary.BLL.DTO;
using Caliburn.Micro;

namespace ArhivskiVjesnikWPF.ViewModels
{
    public class BasicDetaljiSazetkaViewModel : Screen, IBasicDtoDetailsViewModel<SazetakDto>
    {
        public int IDSazetak { get; private set; }
        public string Opis { get; private set; }

        public void InitDto(SazetakDto dto)
        {
            IDSazetak = dto.IDSazetak;
            Opis = dto.Opis;
        }
    }
}
