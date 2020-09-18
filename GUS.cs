using System;
using System.Linq;
using System.Collections.Generic;

using GUS_lib.Utils;
using GUS_lib.Models;

namespace GUS_lib{

    public class GUS : IGUSApi{

        private string key{get;set;}
        private string sid{get;set;}
        private DateTime sidExp{get;set;}
        private SOAP soapClient{get;set;} = new SOAP(
                    "https://wyszukiwarkaregon.stat.gov.pl/wsBIR/UslugaBIRzewnPubl.svc",
                    "http://CIS/BIR/PUBL/2014/07/IUslugaBIRzewnPubl/"
                );

        public static KomunikatDictionary KomunikatValue = new KomunikatDictionary();
        
        public GUS(string key,bool sandbox = false){
            this.key = key;
            if(sandbox)
                this.soapClient.apiUrl = "https://wyszukiwarkaregontest.stat.gov.pl/wsBIR/UslugaBIRzewnPubl.svc";
        }

        private static DateTime WarsawTime{
            get{
                DateTimeOffset warsawTime;
                try{
                    warsawTime = 
                        TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTimeOffset.UtcNow, "Europe/Warsaw");
                }catch(TimeZoneNotFoundException){
                    warsawTime = 
                        TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTimeOffset.UtcNow,"Central European Standard Time");
                }
                return warsawTime.DateTime;
            }
        }

        /*Service access*/
        public void Login(bool force=false){
            DateTime warsawTime = GUS.WarsawTime;
            if(warsawTime.Hour==3 && warsawTime.Minute==20){
                soapClient.sid = null;
                sid = null;
                throw new GUSException("Czyszczenie puli sesji.Spróbuj ponownie za chwilę",0);
            }

            if(sidExp == null || DateTime.Now>sidExp || force){
                sidExp = DateTime.Now.AddMinutes(58);
                ZalogujResponse res = soapClient.getData<Zaloguj,ZalogujResponse>(
                    new Zaloguj(){
                        pKluczUzytkownika = this.key
                    }
                );
                if(res.ZalogujResult==null) throw new GUSException("Nieudana próba logowania.",0);
                sid = res.ZalogujResult;
                soapClient.sid = sid;
            }
        }
        public bool Logout(){
            if(sid!=null){
                WylogujResponse res = soapClient.getData<Wyloguj,WylogujResponse>(
                    new Wyloguj(){
                        pIdentyfikatorSesji = this.sid
                    }
                );
                if(res.WylogujResult){
                    soapClient.sid = null;
                    sid = null;
                }
                return res.WylogujResult;
            }
            return true;
        }

        /*Service info*/
        public GetValueResponse GetValue(GetValueType type){
            return soapClient.getData<GetValue,GetValueResponse>(
                new GetValue(){
                    pNazwaParametru = type
                }
            ,"http://CIS/BIR/2014/07/IUslugaBIR/");
        }

        /*Search entity*/
        public Podmiot SzukajPodmiot(string nip = null,string regon = null,string krs = null){
            Login();
            return soapClient.getData<DaneSzukajPodmioty,DaneSzukajPodmiotyResponse>(
                new DaneSzukajPodmioty(){
                    pParametryWyszukiwania = new ParametryWyszukiwania(){
                        Regon = regon,
                        Nip = nip,
                        Krs = krs
                    }
                }
            ).FirstOrDefault();
        }

        public List<Podmiot> SzukajPodmioty(string[] nipy = null,string[] regony9 = null,string[] regony14 = null,string[] krsy = null){
            Login();
            return soapClient.getData<DaneSzukajPodmioty,DaneSzukajPodmiotyResponse>(
                new DaneSzukajPodmioty(){
                    pParametryWyszukiwania = new ParametryWyszukiwania(){
                        Regon9zn = String.Join(",",regony9),
                        Regon14zn = String.Join(",",regony14),
                        Nipy = String.Join(",",nipy),
                        Krsy = String.Join(",",krsy)
                    }
                }
            );
        }

        public Podmiot SzukajPodmiotNIP(string nip) => SzukajPodmiot(nip);
        public Podmiot SzukajPodmiotRegon(string regon) => SzukajPodmiot(null,regon);
        public Podmiot SzukajPodmiotKrs(string krs) => SzukajPodmiot(null,null,krs);
        public List<Podmiot> SzukajPodmiotyNip(params string[] nipy) => SzukajPodmioty(nipy);
        public List<Podmiot> SzukajPodmiotyRegon9(params string[] regony) => SzukajPodmioty(null,regony);
        public List<Podmiot> SzukajPodmiotyRegon14(params string[] regony) => SzukajPodmioty(null,null,regony);
        public List<Podmiot> SzukajPodmiotyKrs(params string[] krsy) => SzukajPodmioty(null,null,null,krsy);

    }
}