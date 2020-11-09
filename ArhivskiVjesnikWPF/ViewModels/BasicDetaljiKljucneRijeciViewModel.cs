using ArhivskiVjesnikLibrary.BLL.DTO;
using Caliburn.Micro;

namespace ArhivskiVjesnikWPF.ViewModels
{
    public class BasicDetaljiKljucneRijeciViewModel : Screen, IBasicDtoDetailsViewModel<KljucnaRijecDto>
    {
        public int IDKljucnaRijec { get; private set; }
        public string Vrijednost { get; private set; }

        public void InitDto(KljucnaRijecDto dto)
        {
            IDKljucnaRijec = dto.IDKljucnaRijec;
            Vrijednost = dto.Vrijednost;
        }
    }
}
