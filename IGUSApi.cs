using System.Collections.Generic;

using GUS_lib.Models;

namespace GUS_lib{
    public interface IGUSApi{
        
        void Login(bool force);
        bool Logout();
        GetValueResponse GetValue(GetValueType type);
        Podmiot SzukajPodmiot(string nip,string regon,string krs);
        List<Podmiot> SzukajPodmioty(string[] nipy,string[] regony9,string[] regony14,string[] krsy);
        Podmiot SzukajPodmiotNip(string nip);
        Podmiot SzukajPodmiotRegon(string regon);
        Podmiot SzukajPodmiotKrs(string krs);
        List<Podmiot> SzukajPodmiotyNip(params string[] nipy);
        List<Podmiot> SzukajPodmiotyRegon9(params string[] regony);
        List<Podmiot> SzukajPodmiotyRegon14(params string[] regony);
        List<Podmiot> SzukajPodmiotyKrs(params string[] krsy);

    }
}