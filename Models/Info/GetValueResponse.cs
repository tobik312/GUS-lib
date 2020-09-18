using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Globalization;

using GUS_lib.Utils;

namespace GUS_lib.Models{

    [XmlRoot(Namespace=RequestNamespaces.NSG)]
    public class GetValueResponse :IXmlSerializable,
    IStatusSesji,IStatusUslugi,IStanDanych,IKomunikatUslugi,IKomunikatTresc,IKomunikatKod{

        private static InvalidOperationException operationError(string value) =>
            new InvalidOperationException("The respose do not include `"+value+"` value");

        StatusUslugi IGetValueResult<StatusUslugi>.Result{
            get{
                int value;
                if(Int32.TryParse(this.Result,NumberStyles.Integer,null,out value))
                    if(value>=0 && value<=2) return (StatusUslugi) value;
                throw operationError("StatusUslugi");
            }
        }
        bool IGetValueResult<bool>.Result{
            get{
                if(this.Result=="0")
                    return false;
                else if(this.Result=="1")
                    return true;
                throw operationError("StatusSesji");
            }
        }
        uint? IGetValueResult<uint?>.Result{
            get{
                uint value;
                if(UInt32.TryParse(this.Result,NumberStyles.Integer,null,out value))
                    return value;
                else if(this.Result==null)
                    return null;
                throw operationError("KomunikatKod");
            }
        }
        public string Result{get;private set;}

        public void ReadXml(XmlReader reader){
            reader.Read();
            this.Result = reader.ReadInnerXml();
        }

        public void WriteXml(XmlWriter writer){}

        public XmlSchema GetSchema(){
            return null;
        }
    }

}