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

        //
        public Customer Login()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var request = new RestRequest(Method.GET)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; },
                Resource = "api/customers/"
            };

            var customer = new Customer();
            Console.Write("\n Please enter name: ");
            customer.Name = Console.ReadLine();
            Console.Write("\b Please enter password: \b");
            
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
            customer.Pw = Str;

            Console.Clear();

            request.Resource += customer.Name + "/" + customer.Pw;
            customer = client.Execute<Customer>(request).Data;

            if (customer.Name == null)
            {
                Console.WriteLine("\n *** Invalid name or password! ***");
                Console.WriteLine(" *** Please try again! ***");
                System.Threading.Thread.Sleep(4000);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("===========================");
                Console.WriteLine("Welcome " + customer.Name + "\n");
                Console.Write("  ALLSTORAGE: " + customer.AllStorage);
                Console.Write("  USED STORAGE: " + customer.FreeStorage);
                Console.WriteLine();
            }
            return customer;
        }

        public void ListTransactions()
        {
            var transaction = new Transaction();
            var transList = new List<Transaction>(transaction.ListTransactions(true));
            var order = new Order();
            var orderList = new List<Order>(order.ListCustomerOrders(this.Id, true));

            Console.WriteLine("Displaying Transactions for " + this.Name + " based on sent orders");
            Console.WriteLine("===========================");

            foreach (var trans in transList.Where(a => orderList.Any(b => a.OrderId == b.Id)))
            {
                Console.WriteLine("TRANS. ID:  " + trans.Id);
                Console.WriteLine(" ORDER ID:  " + trans.OrderId);
                Console.WriteLine("     GATE:  " + trans.Gate);
                Console.WriteLine("     TIME:  " + trans.Time);
                Console.WriteLine(" LOCATION:  " + trans.Location);
                Console.WriteLine("DIRECTION:  " + trans.Direction);
                Console.WriteLine("TIMESTAMP:  " + trans.TimeStamp);
                Console.WriteLine(" DISP. ID:  " + trans.DispatcherId);
                Console.WriteLine("===========================");
            }
        }

        //on creating 
    }
}
