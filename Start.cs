using System;

using GUS_lib.Models;
using GUS_lib.Utils;

namespace GUS_lib{
    public class Start{
        
        public static void Main(string[] args){
            GUS gusClient = new GUS("abcde12345abcde12345",true);
            try{
                gusClient.Login();
                var a = (IStatusUslugi) gusClient.GetValue(GetValueType.StatusUslugi);
                Console.WriteLine(a.Result);
            }catch(GUSException e){
                Console.WriteLine(e.Message);
            }catch(SOAPException e){
                Console.WriteLine(e.Message);
            }
        }

    }
}