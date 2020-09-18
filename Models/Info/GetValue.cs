using System.Xml.Serialization;

using GUS_lib.Utils;

namespace GUS_lib.Models{
    
    [XmlRoot(Namespace=RequestNamespaces.NSG)]
    public class GetValue{
        
        public GetValueType pNazwaParametru{get;set;}

    }
}