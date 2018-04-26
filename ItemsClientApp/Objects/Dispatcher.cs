using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using RestSharp;
using System.Configuration;
using System.Net;

namespace WarehouseClient
{
    class Dispatcher
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Pw")]
        public string Pw { get; set; }

        //
        public Dispatcher Login()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var request = new RestRequest(Method.GET)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; },
                Resource = "api/dispatchers/"
            };

            var dispatcher = new Dispatcher();
            Console.Write("\n Please enter name: ");
            dispatcher.Name = Console.ReadLine();
            Console.Write(" Please enter password: ");
            //jelszó elrejtése...
            String Str = "";
            Boolean asd = true;
            while (asd)
            {
                char s = Console.ReadKey(true).KeyChar;
                if (s == '\r')
                {
                    asd = false;
                }
                else
                {
                    Str = Str + s.ToString();
                }
            }
            dispatcher.Pw = Str;
            Console.Clear();

            request.Resource += dispatcher.Name + "/" + dispatcher.Pw;
            dispatcher = client.Execute<Dispatcher>(request).Data;

            if (dispatcher == null || dispatcher.Name == null || dispatcher.Pw == null)
            {
                Console.WriteLine("\n *** Invalid name or password! ***");
                Console.WriteLine(" *** Please try again! ***");
                System.Threading.Thread.Sleep(4000);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("===========================");
                Console.WriteLine("Welcome " + dispatcher.Name);
                Console.WriteLine();
            }
            return dispatcher;
        }
    }
}
