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
        public string Location { get; set; }

        [DataMember(Name = "Direction")]
        public string Direction { get; set; }

        [DataMember(Name = "TimeStamp")]
        public string TimeStamp { get; set; }

        [DataMember(Name = "DispatcherId")]
        public int DispatcherId { get; set; }

        [DataMember(Name = "Status")]
        public string Status { get; set; }

        public List<Transaction> ListTransactions(bool onlyData)
        {
            

            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var request = new RestRequest(Method.GET)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; },
                Resource = "api/transactions/"
            };

            var transList = new List<Transaction>();

            try
            {
                transList = client.Execute<List<Transaction>>(request).Data;

                if (!onlyData)
                {
                    Console.WriteLine();
                    Console.WriteLine("===============================");
                    foreach (var transaction in transList)
                    {
                        Console.WriteLine("TRANS. ID:  " + transaction.Id);
                        Console.WriteLine(" ORDER ID:  " + transaction.OrderId);
                        Console.WriteLine("     GATE:  " + transaction.Gate);
                        Console.WriteLine("     TIME:  " + transaction.Time);
                        Console.WriteLine(" LOCATION:  " + transaction.Location);
                        Console.WriteLine("DIRECTION:  " + transaction.Direction);
                        Console.WriteLine("TIMESTAMP:  " + transaction.TimeStamp);
                        Console.WriteLine(" DISP. ID:  " + transaction.DispatcherId);
                        Console.WriteLine("   STATUS:  " + transaction.Status);
                        Console.WriteLine("===============================");
                    }
                }
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg.Message);
            }
            return transList;
        }


        public Transaction GetTransactionById(int id)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var request = new RestRequest(Method.GET)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; },
                Resource = "api/Transactions/" + id
            };

            var transaction = new Transaction();

            try
            {
                transaction = client.Execute<Transaction>(request).Data;
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg.Message);
            }
            return transaction;
        }

        //folyamatban lévő tranzakciók lekérése
        public void ListInProgressTransactions()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var request = new RestRequest(Method.GET)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; },
                Resource = "api/transactions/"
            };

            var transList = new List<Transaction>();
            try
            {
                transList = client.Execute<List<Transaction>>(request).Data;

                Console.WriteLine();
                Console.WriteLine("===============================");

                
                    foreach (var transaction in transList.Where(i => i.Status == "in-progress"))
                    {
                        Console.WriteLine("TRANS. ID:  " + transaction.Id);
                        Console.WriteLine(" ORDER ID:  " + transaction.OrderId);
                        Console.WriteLine("     GATE:  " + transaction.Gate);
                        Console.WriteLine("     TIME:  " + transaction.Time);
                        Console.WriteLine(" LOCATION:  " + transaction.Location);
                        Console.WriteLine("DIRECTION:  " + transaction.Direction);
                        Console.WriteLine("TIMESTAMP:  " + transaction.TimeStamp);
                        Console.WriteLine(" DISP. ID:  " + transaction.DispatcherId);
                        Console.WriteLine("   STATUS:  " + transaction.Status);
                        Console.WriteLine("===============================");
                    }
                
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg.Message);
            }
        }
        
        //tranzakció létrehozás
        public void AddTransaction()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var transaction = new Transaction();
            var order = new Order();

            Console.WriteLine("\n\n        ** CREATE TRANSACTION **\n");
            Console.Write("          Order ID: ");
            int orderId = int.Parse(Console.ReadLine());
            //get order details
            order = order.GetOrderById(orderId);

            //if withrawal, get the right itemlist and list locations!
            string location = "";
            if (order.Direction == "Withdrawal")
            {
                var item = new Item();
                var itemList = new List<Item>(item.GetItems(true));

                foreach (var i in itemList.Where(a => a.OwnerId == order.CostumerId && a.Name == order.ItemName && a.Status == "In Place"))
                {
                    Console.WriteLine("LOCATION IN SYSTEM: " + i.Location);
                    location = i.Location;
                }
                
            }
            //Form and data input
            transaction.OrderId = order.Id;
            Console.WriteLine("        *ITEM NAME: " + order.ItemName);
            Console.WriteLine("         *QUANTITY: " + order.Quantity);
            Console.WriteLine("        *DIRECTION: " + order.Direction + "\n");
            transaction.Direction = order.Direction;
            Console.Write("              GATE: ");
            transaction.Gate = int.Parse(Console.ReadLine());
            Console.Write(" TIME(MM-dd HH:mm): ");
            transaction.Time = Console.ReadLine();
            if (location != "")
            {
                Console.Write("          LOCATION: " + location + "\n");
                transaction.Location = location;
            }
            else
            {
                Console.Write("          LOCATION: ");
                transaction.Location = Console.ReadLine();
            }
            DateTime myDateTime = DateTime.Now;
            transaction.TimeStamp = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            Console.Write("         TIMESTAMP: " + transaction.TimeStamp + "\n");
            transaction.DispatcherId = 1;
            Console.Write("     DISPATCHER ID: " + transaction.DispatcherId.ToString() + "\n");
            transaction.Status = "in-progress";
            Console.Write("            STATUS: In-progress\n");

            var request = new RestRequest("api/Transactions/", Method.POST);
            request.AddJsonBody(transaction);
            client.Execute(request);

            //create item
            if (order.Direction == "Deposit")
            {
                var item = new Item();
                
                item.Name = order.ItemName;
                item.OwnerId = order.CostumerId;
                item.Location = transaction.Location;
                item.Status = "Waiting for " + order.Direction;
                item.OrderId = order.Id;

                item.AddItem(true);
            }
            else //modify the item, location should come from here
            {
                var item = new Item();
                var itemList = new List<Item>(item.GetItems(true));

                foreach (var i in itemList.Where(a => a.OwnerId == order.CostumerId && a.Name == order.ItemName))
                {
                    i.Status = "Waiting for " + order.Direction;
                    i.PutItem(true, i.Id);
                }
            }

            //change the status of the order
            order.UpdateOrderStatus(order.Id, "processed");
            Console.ReadLine();
        }

        //tranzakció státuszának átírása
        public void UpdateTransactionStatus(int id, string status)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var transaction = new Transaction();

            transaction = transaction.GetTransactionById(id);
            transaction.Status = status;

            var request = new RestRequest(Method.PUT)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; },
                Resource = "api/Transactions/" + transaction.Id
            };
            request.AddJsonBody(transaction);
            client.Execute(request);
        }
    }
}
