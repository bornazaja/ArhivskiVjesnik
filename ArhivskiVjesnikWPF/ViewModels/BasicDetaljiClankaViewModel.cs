using ArhivskiVjesnikLibrary.BLL.DTO;
using System;
using System.Windows.Input;

namespace ArhivskiVjesnikWPF.ViewModels
{
    public class BasicDetaljiClankaViewModel : IBasicDtoDetailsViewModel<ClanakDto>
    {
        public BasicDetaljiClankaViewModel(CommandHandlerViewModel commandHandlerViewModel)
        {
            URLCommand = commandHandlerViewModel.URLCommand;
        }

        public int IDClanak { get; private set; }
        public string Naziv { get; private set; }
        public int Godiste { get; private set; }
        public int Broj { get; private set; }
        public int Volumen { get; private set; }
        public DateTime DatumIzdavanja { get; private set; }
        public DateTime DatumObjave { get; private set; }
        public string URL { get; private set; }
        public ICommand URLCommand { get; private set; }

        public void InitDto(ClanakDto dto)
        {
            IDClanak = dto.IDClanak;
            Naziv = dto.Naziv;
            Godiste = dto.Godiste;
            Broj = dto.Broj;
            Volumen = dto.Volumen;
            DatumIzdavanja = dto.DatumIzdavanja;
            DatumObjave = dto.DatumObjave;
            URL = dto.URL;
        }
    }
}
