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
    class Transaction
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "OrderId")]
        public int OrderId { get; set; }

        [DataMember(Name = "Gate")]
        public int Gate { get; set; }

        [DataMember(Name = "Time")]
        public string Time { get; set; }

        [DataMember(Name = "Location")]
        public int Location { get; set; }

        [DataMember(Name = "Direction")]
        public string Direction { get; set; }

        [DataMember(Name = "TimeStamp")]
        public string TimeStamp { get; set; }

        [DataMember(Name = "DispatcherId")]
        public int DispatcherId { get; set; }

        public void GetTransactions()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var request = new RestRequest(Method.GET)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; },
                Resource = "api/transactions/"
            };

            try
            {
                var transactionList = client.Execute<List<Transaction>>(request).Data;

                Console.WriteLine();
                Console.WriteLine("===========================");
                foreach (var transaction in transactionList)
                {
                    Console.WriteLine("TRANS. ID:  " + transaction.Id);
                    Console.WriteLine(" ORDER ID:  " + transaction.OrderId);
                    Console.WriteLine("     GATE:  " + transaction.Gate);
                    Console.WriteLine("     TIME:  " + transaction.Time);
                    Console.WriteLine(" LOCATION:  " + transaction.Location);
                    Console.WriteLine("DIRECTION:  " + transaction.Direction);
                    Console.WriteLine("TIMESTAMP:  " + transaction.TimeStamp);
                    Console.WriteLine(" DISP. ID:  " + transaction.DispatcherId);
                    Console.WriteLine("===========================");
                }
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg.Message);
            }
        }

        public void AddTransaction()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var transaction = new Transaction();
            var order = new Order();

            Console.WriteLine("\n\n** CREATE TRANSACTION **\n");
            Console.Write("          Order ID: ");
            int orderId = int.Parse(Console.ReadLine());
            //get order details
            order = order.GetOrderById(orderId);

            transaction.OrderId = order.Id;
            Console.Write("              GATE: ");
            transaction.Gate = int.Parse(Console.ReadLine());
            Console.Write("TIME (MM-dd HH:mm): ");
            transaction.Time = Console.ReadLine();
            Console.Write("          LOCATION: ");
            transaction.Location = int.Parse(Console.ReadLine());
            transaction.Direction = order.Direction;
            Console.Write("         DIRECTION: " + transaction.Direction + "\n");
            DateTime myDateTime = DateTime.Now;
            transaction.TimeStamp = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            Console.Write("         TIMESTAMP: " + transaction.TimeStamp + "\n");
            transaction.DispatcherId = 1;
            Console.Write("     DISPATCHER ID: " + transaction.DispatcherId.ToString() + "\n");

            var request = new RestRequest("api/Transactions/", Method.POST);
            request.AddJsonBody(transaction);
            client.Execute(request);

            //change the status of the order
            order.UpdateOrderStatus(order.Id, "processed");
            Console.ReadLine();
        }
    }
}
