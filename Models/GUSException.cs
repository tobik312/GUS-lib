using System;
using System.Xml.Serialization;

namespace GUS_lib.Models{
    public class GUSException : Exception{

        public uint Code{get;set;}

        public GUSException(string Message,uint code) : base(Message){
            this.Code = code;
        }
        
    }
}