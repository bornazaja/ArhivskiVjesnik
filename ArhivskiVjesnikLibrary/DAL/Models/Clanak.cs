using System;

namespace ArhivskiVjesnikLibrary.DAL.Models
{
    public class Clanak
    {
        public int IDClanak { get; set; }
        public string Naziv { get; set; }
        public int Godiste { get; set; }
        public int Volumen { get; set; }
        public int Broj { get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public DateTime DatumObjave { get; set; }
        public string URL { get; set; }
    }
}
