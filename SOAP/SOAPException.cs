using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace GUS_lib.Utils{

    [XmlRoot("Fault")]
    public sealed class SOAPException : Exception,IXmlSerializable{
        public string Code{get;set;}
        public new string Message{get;private set;}

        public void ReadXml(XmlReader reader){
            reader.MoveToContent();
            while(reader.Read()){
                if(reader.NodeType==XmlNodeType.Element){
                    if(reader.LocalName=="Subcode"){
                        reader.Read();
                        reader.Read();
                        this.Code = reader.ReadContentAsString();

                    }else if(reader.LocalName=="Reason"){
                        reader.Read();
                        reader.Read();
                        this.Message = reader.ReadContentAsString();
                        break;
                    }
                }
            }
        }

        public void WriteXml(XmlWriter writer){}

        public XmlSchema GetSchema(){
            return null;
        }
    }
}