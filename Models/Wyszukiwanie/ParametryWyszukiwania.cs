using System.Xml.Serialization;

using GUS_lib.Utils;

namespace GUS_lib.Models{

    [XmlRoot(Namespace=RequestNamespaces.DAT)]
    public class ParametryWyszukiwania{
        public string Regon{get;set;}
        public string Nip{get;set;}
        public string Krs{get;set;}
        public string Nipy{get;set;}
        public string Regon9zn{get;set;}
        public string Regon14zn{get;set;}
        public string Krsy{get;set;}
    }
}