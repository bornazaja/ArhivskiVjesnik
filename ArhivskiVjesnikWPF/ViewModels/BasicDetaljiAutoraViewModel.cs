using ArhivskiVjesnikLibrary.BLL.DTO;
using Caliburn.Micro;

namespace ArhivskiVjesnikWPF.ViewModels
{
    public class BasicDetaljiAutoraViewModel : Screen, IBasicDtoDetailsViewModel<AutorDto>
    {
        public int IDAutor { get; private set; }
        public string Ime { get; private set; }
        public string Prezime { get; private set; }

        public void InitDto(AutorDto dto)
        {
            IDAutor = dto.IDAutor;
            Ime = dto.Ime;
            Prezime = dto.Prezime;
        }
    }
}
