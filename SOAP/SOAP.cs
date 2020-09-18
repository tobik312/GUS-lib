using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System;

using GUS_lib.Models;

namespace GUS_lib.Utils{
    public class SOAP{

        private readonly HttpClient client = new HttpClient();
        public string apiUrl{get;set;}
        public string actionUrl{get;set;}
        public string sid{
            set{
                if(client!=null){
                    client.DefaultRequestHeaders.Remove("sid");
                    client.DefaultRequestHeaders.Add("sid",value);
                }
            }
        }

        public SOAP(string endpointUrl,string actionUrl){
            this.apiUrl = endpointUrl;
            this.actionUrl = actionUrl;
            client.BaseAddress = new Uri(endpointUrl);
            client.DefaultRequestHeaders.Accept.Clear();  
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/xop+xml")
            );
            client.DefaultRequestHeaders.Add("sid","");
            client.DefaultRequestHeaders.Add("User-Agent",
            "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");
        }

        public ResType getData<ReqType,ResType>(ReqType requestData,string customActionUrl=null){
            try{
                if(customActionUrl==null) customActionUrl = actionUrl;
                string action = typeof(ReqType).Name;
                Envelope<ReqType> envelope = new Envelope<ReqType>(customActionUrl+action,apiUrl,requestData);
                StringContent httpContext = new StringContent(envelope.serializeSOAP(),Encoding.UTF8,"application/soap+xml");

                HttpResponseMessage response = client.PostAsync("",httpContext).Result;

                Stream responseStream = response.Content.ReadAsStreamAsync().Result;
                string line,delimiter = null;
                StreamReader reader = new StreamReader(responseStream,Encoding.UTF8);

                MemoryStream stream = new MemoryStream();
                StreamWriter writer = null;
                while((line = reader.ReadLine())!=null && line!=delimiter){
                    if(line=="")
                        if(delimiter==null)
                            delimiter = reader.ReadLine()+"--";
                        else{
                            writer = new StreamWriter(stream,Encoding.UTF8);
                        }
                    if(writer!=null){
                        writer.Write(line);
                    }
                }
                writer.Flush();
                stream.Position = 0;

                if(response.StatusCode!=HttpStatusCode.InternalServerError)
                    response.EnsureSuccessStatusCode();

                XmlSerializer serializer = new XmlSerializer(typeof(Envelope<ResType>));
                Envelope<ResType> responseData = serializer.Deserialize(stream) as Envelope<ResType>;
                
                return responseData.Body.request;
            }catch(InvalidOperationException e) when(e.InnerException.InnerException is GUSException){
                throw e.InnerException.InnerException;
            }catch(InvalidOperationException e) when(e.InnerException is SOAPException){
                throw e.InnerException;
            }
        }
        
    }
}