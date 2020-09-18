using System.Xml.Serialization;

namespace GUS_lib.Models{
    public enum GetValueType{
        [XmlEnum(Name="StanDanych")]
        StanDanych,
        [XmlEnum(Name="KomunikatKod")]
        KomunikatKod,
        [XmlEnum(Name="KomunikatTresc")]
        KomunikatTresc,
        [XmlEnum(Name="StatusSesji")]
        StatusSesji,
        [XmlEnum(Name="StatusUslugi")]
        StatusUslugi,
        [XmlEnum(Name="KomunikatUslugi")]
        KomunikatUslugi
    }
}