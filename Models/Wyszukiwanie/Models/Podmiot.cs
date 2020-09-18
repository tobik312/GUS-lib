namespace GUS_lib.Models{
    public class Podmiot{
        public string Nazwa{get;set;}
        public string Regon{get;set;}
        public string Nip{get;set;}
        public string Wojewodztwo{get;set;}
        public string Powiat{get;set;}
        public string Gmina{get;set;}
        public string Ulica{get;set;}
        public string Miejscowosc{get;set;}
        public string KodPocztowy{get;set;}
        public string MiejscowoscPoczty{get;set;}
        public string NrNieruchomosci{get;set;}
        public string NrLokalu{get;set;}
        public char Typ{get;set;}
        public SilosType SilosID{get;set;}
        //public DateTime DataZakonczeniaDzialalnosci{get;set;}
    }
}