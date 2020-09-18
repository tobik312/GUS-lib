using System;

namespace GUS_lib.Models.Raport{
    public interface IDzialalnosc : IPelnyRaport{
        string nazwa{get;set;}
        DateTime dataPowstania{get;set;}
        DateTime dataRozpoczeciaDzialalnosci{get;set;}
        DateTime dataZawieszeniaDzialalnosci{get;set;}
        DateTime dataWznowieniaDzialalnosci{get;set;}
        DateTime dataZakonczeniaDzialalnosci{get;set;}
        Adres siedziba{get;set;}
    }
}