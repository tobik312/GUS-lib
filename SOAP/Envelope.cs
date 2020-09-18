using System;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Linq;

using GUS_lib.Models;

namespace GUS_lib.Utils{

    public static class RequestNamespaces{
        public const string SOAP = "http://www.w3.org/2003/05/soap-envelope";
        public const string NS = "http://CIS/BIR/PUBL/2014/07";
        public const string WSA = "http://www.w3.org/2005/08/addressing";
        public const string DAT = "http://CIS/BIR/PUBL/2014/07/DataContract";
        public const string NSG = "http://CIS/BIR/2014/07";
    }

    public sealed class Header{

        [XmlElement(Namespace=RequestNamespaces.WSA)]
        public string Action{get;set;}

        [XmlElement(Namespace=RequestNamespaces.WSA)]
        public string To{get;set;}
    }

    public sealed class Body<T> : IXmlSerializable{

        public T request{get;set;}

        private XmlSerializer reqSerializer = new XmlSerializer(typeof(T),RequestNamespaces.NS);

        public void ReadXml(XmlReader reader){
            reader.ReadStartElement();     
            if(reader.LocalName=="Fault"){
                reqSerializer = new XmlSerializer(typeof(SOAPException),RequestNamespaces.SOAP);
                throw ((SOAPException) reqSerializer.Deserialize(reader));
            }
            request = (T) reqSerializer.Deserialize(reader);
        }

        public void WriteXml(XmlWriter writer){
            reqSerializer.Serialize(writer,this.request);
            writer.Flush();
        }
        
        public XmlSchema GetSchema(){
            return null;
        }
    }


    [XmlRoot("Envelope",Namespace = RequestNamespaces.SOAP)]
    public sealed class Envelope<T>{

        public Header Header{get;set;} = new Header();
        public Body<T> Body{get;set;} = new Body<T>();
        private static XmlSerializerNamespaces ns;

        public Envelope(){}

        public Envelope(string action,string to,T data){
            Header.Action = action;
            Header.To = to;
            Body.request = data;
        }

        static Envelope(){
            ns = new XmlSerializerNamespaces();
            ns.Add("soap",RequestNamespaces.SOAP);
            ns.Add("ns",RequestNamespaces.NS);
            ns.Add("wsa",RequestNamespaces.WSA);
            ns.Add("dat",RequestNamespaces.DAT);
            ns.Add("nsg",RequestNamespaces.NSG);
        }

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces namespaces{
            get{
                return ns;
            }
            set{}
        }
    
        public string serializeSOAP(){
            //Serialize
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            XmlWriterSettings settings = new XmlWriterSettings{
                Encoding = Encoding.UTF8,
                Indent = true,
                OmitXmlDeclaration = true,
            };
            //Write to string
            StringBuilder builder = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(builder,settings);
            serializer.Serialize(writer,this);
            
            return builder.ToString();
        }
    }
}