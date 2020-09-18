using System.Collections.Generic;

namespace GUS_lib.Models{
    public class KomunikatDictionary{
        public string this[uint? code]{
            get{
                if(code==null || code==7)
                    return "Brak sesji. Sesja wygasła lub przekazano nieprawidłową wartość nagłówka sid";
                else if(code==0)
                    return "Poprzednia operacja wykonana prawidłowo";
                else if(code==1)
                    return "Kod nieaktualny";
                else if(code==2)
                    return "Do metody DaneSzukaj przekazano zbyt wiele identyfikatorów";
                else if(code==4)
                    return "Nie znaleziono podmiotów.";
                else if(code==5)
                    return "Nieprawidłowa lub pusta nazwa raportu";
                return null;
            }
        }

    }
}