using System;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace GUS_lib.Models{
    public abstract class GUSDataResponse<T> : List<T>,IXmlSerializable where T: class,new(){
        //Parse by hand object with htmlencodedData
        //I do not have any idea why they put data in this format
        public void ReadXml(XmlReader reader){
            //Skip root element with is child type
            reader.ReadStartElement();
            //Read result data
            string dataXml = HttpUtility.HtmlDecode(reader.ReadInnerXml());
            if(dataXml=="") return;
            //Parse data to xml document
            XmlDocument dataDoc = new XmlDocument();
            dataDoc.LoadXml(dataXml);
            //Find data node
            XmlNodeList data = dataDoc.GetElementsByTagName("dane");
            //Set by hand properties and catch exception
            foreach(XmlNode dane in data){
                T obj = new T();
                if(dane.FirstChild.Name=="ErrorCode"){
                    uint code = UInt32.Parse(dane.FirstChild.InnerText);
                    string message = dane.FirstChild.NextSibling.InnerText;
                    throw new GUSException(message,code);
                }
                foreach(XmlNode node in dane.ChildNodes){
                    var prop = obj.GetType().GetProperty(node.Name);
                    if(prop==null) continue;
                    Type type= prop.PropertyType;
                    if(type.IsEnum){
                        byte value = byte.Parse(node.InnerText);
                        prop.SetValue(obj,Enum.ToObject(type,value));
                    }else if(type.Equals(typeof(char)))
                        prop.SetValue(obj,node.InnerText[0]);
                    else if(type.Equals(typeof(string)))
                        prop.SetValue(obj,node.InnerText);
                }
                this.Add(obj);
            }
            reader.Close();
        }

        //Actually response do not have to be serialized
        //Basicly TODO later
        public void WriteXml(XmlWriter writer){
            
        }

        public XmlSchema GetSchema(){
            return null;
        }

    }
}