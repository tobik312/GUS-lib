using System.Xml.Serialization;

namespace GUS_lib.Models{
    public enum RaportType{
        [XmlEnum(Name="BIR11OsFizycznaDaneOgolne")]
        FizycznaDaneOgolne,
        [XmlEnum(Name="BIR11OsFizycznaDzialalnoscCeidg")]
        FizycznaDzialalnoscCeidg,
        [XmlEnum(Name="BIR11OsFizycznaDzialalnoscRolnicza")]
        FizycznaDzialalnoscRolnicza,
        [XmlEnum(Name="BIR11OsFizycznaDzialalnoscPozostala")]
        FizycznaDzialalnoscPozostala,
        [XmlEnum(Name="BIR11OsFizycznaDzialalnoscSkreslonaDo20141108")]
        FizycznaDzialalnoscSkreslonaDo20141108,
        [XmlEnum(Name="BIR11OsFizycznaPkd")]
        FizycznaPkd,
        [XmlEnum(Name="BIR11OsFizycznaListaJednLokalnych")]
        FizycznaListaJednLokalnych,
        [XmlEnum(Name="BIR11JednLokalnaOsFizycznej")]
        JednLokalnaOsFizycznej,
        [XmlEnum(Name="BIR11JednLokalnaOsFizycznejPkd")]
        JednLokalnaOsFizycznejPkd,
        [XmlEnum(Name="BIR11OsPrawna")]
        OsPrawna,
        [XmlEnum(Name="BIR11OsPrawnaPkd")]
        PrawnaPkd,
        [XmlEnum(Name="BIR11OsPrawnaListaJednLokalnych")]
        OsPrawnaListaJednLokalnych,
        [XmlEnum(Name="BIR11JednLokalnaOsPrawnej")]
        JednLokalnaOsPrawnej,
        [XmlEnum(Name="BIR11JednLokalnaOsPrawnejPkd")]
        JednLokalnaOsPrawnejPkd,
        [XmlEnum(Name="BIR11OsPrawnaSpCywilnaWspolnicy")]
        PrawnaSpCywilnaWspolnicy,
        [XmlEnum(Name="BIR11TypPodmiotu")]
        TypPodmiotu
    }
}