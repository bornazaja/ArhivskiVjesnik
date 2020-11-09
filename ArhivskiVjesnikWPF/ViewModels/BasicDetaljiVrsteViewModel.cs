using ArhivskiVjesnikLibrary.BLL.DTO;
using Caliburn.Micro;

namespace ArhivskiVjesnikWPF.ViewModels
{
    public class BasicDetaljiVrsteViewModel : Screen, IBasicDtoDetailsViewModel<VrstaDto>
    {
        public int IDVrsta { get; private set; }
        public string Naziv { get; private set; }

        public void InitDto(VrstaDto dto)
        {
            IDVrsta = dto.IDVrsta;
            Naziv = dto.Naziv;
        }
    }
}
