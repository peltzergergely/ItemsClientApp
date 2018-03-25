using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using RestSharp;
using System.Configuration;

namespace WarehouseClient
{
    class Customer
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Pw")]
        public string Pw { get; set; }

        [DataMember(Name = "AllStorage")]
        public string AllStorage { get; set; }

        [DataMember(Name = "FreeStorage")]
        public string FreeStorage { get; set; }

        //login menu

        public Customer Login()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var request = new RestRequest(Method.GET)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; },
                Resource = "api/customers/"
            };

            var customer = new Customer();
            Console.WriteLine("Név: Bill");
            customer.Name = "Bill";
            Console.WriteLine("Jelszó: 123456");
            customer.Pw = "123456";
            Console.Clear();

            request.Resource += customer.Name + "/" + customer.Pw;

            customer = client.Execute<Customer>(request).Data;

            Console.WriteLine();
            Console.WriteLine("===========================");
            Console.WriteLine("Welcome " + customer.Name);
            Console.Write("  ALLSTORAGE: " + customer.AllStorage);
            Console.Write("  USED STORAGE: " + customer.FreeStorage);
            Console.WriteLine();

            return customer;
        }


        //check free space on enter
        //show relevant transaction
        //list stored items
    }
}
