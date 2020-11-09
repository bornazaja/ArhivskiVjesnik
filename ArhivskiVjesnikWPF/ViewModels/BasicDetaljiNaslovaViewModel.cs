using ArhivskiVjesnikLibrary.BLL.DTO;
using Caliburn.Micro;

namespace ArhivskiVjesnikWPF.ViewModels
{
    public class BasicDetaljiNaslovaViewModel : Screen, IBasicDtoDetailsViewModel<NaslovDto>
    {
        public int IDNaslov { get; private set; }
        public string Naziv { get; private set; }

        public void InitDto(NaslovDto dto)
        {
            IDNaslov = dto.IDNaslov;
            Naziv = dto.Naziv;
        }
    }
}
