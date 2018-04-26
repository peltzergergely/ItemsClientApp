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
    class Storekeeper
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Pw")]
        public string Pw { get; set; }

        //
        public Storekeeper Login()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var request = new RestRequest(Method.GET)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; },
                Resource = "api/storekeepers/"
            };

            var storekeeper = new Storekeeper();
            Console.Write("\n Please enter name: ");
            storekeeper.Name = Console.ReadLine();
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
            storekeeper.Pw = Str;
            Console.Clear();

            request.Resource += storekeeper.Name + "/" + storekeeper.Pw;
            storekeeper = client.Execute<Storekeeper>(request).Data;

            if (storekeeper == null || storekeeper.Name == null || storekeeper.Pw == null)
            {
                Console.WriteLine("\n *** Invalid name or password! ***");
                Console.WriteLine(" *** Please try again! ***");
                System.Threading.Thread.Sleep(4000);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("===========================");
                Console.WriteLine("Welcome " + storekeeper.Name);
                Console.WriteLine();
            }
            return storekeeper;
        }
    }
}
