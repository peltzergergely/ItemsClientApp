using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;


namespace WarehouseClient
{
    class Order
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "CostumerId")]
        public int CostumerId { get; set; }

        [DataMember(Name = "ItemName")]
        public string ItemName { get; set; }

        [DataMember(Name = "Quantity")]
        public int Quantity { get; set; }

        [DataMember(Name = "Status")]
        public string Status { get; set; }

        [DataMember(Name = "Direction")]
        public string Direction { get; set; }

        [DataMember(Name = "TimeStamp")]
        public string TimeStamp { get; set; }
        
        //új rendelés leadás ügyfél által
        public void AddOrder(Customer customer)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var order = new Order();

            Console.WriteLine("\n\n** CHOOSE DIRECTION **\n");

            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdrawal");
            Console.Write("INPUT: ");
            var direction = Console.ReadLine();
            if (direction == "2")
            {
                order.Direction = "Withdrawal";
                Console.Clear();
                Console.WriteLine("\n\n** WITHDRAWAL **\n");
            }
            else
            {
                order.Direction = "Deposit";
                Console.Clear();
                Console.WriteLine("\n\n** DEPOSIT **\n");
            }

            Console.Write("CUSTOMER ID: " + customer.Id + "\n");
            order.CostumerId = customer.Id;
            Console.Write("  ITEM NAME: ");
            order.ItemName = Console.ReadLine();
            Console.Write("   QUANTITY: ");
            order.Quantity = int.Parse(Console.ReadLine());
            Console.Write("     STATUS: Pending\n");
            order.Status = "pending";
            Console.Write("  DIRECTION: " + order.Direction + "\n");
            DateTime myDateTime = DateTime.Now;
            order.TimeStamp = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            Console.Write("  TIMESTAMP: " + order.TimeStamp);

            var request = new RestRequest("api/orders/", Method.POST);
            request.AddJsonBody(order);
            client.Execute(request);


            Console.ReadLine();
        }
        
        //rendelések lekérdezése
        public Order GetOrderById(int id)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var request = new RestRequest(Method.GET)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; },
                Resource = "api/orders/" + id
            };

            var order = new Order();

            try
            {
                order = client.Execute<Order>(request).Data;
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg.Message);
            }
            return order;
        }
        
        //jóváhagyásra váró rendelések lekérése
        public void ListPendingOrders()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var request = new RestRequest(Method.GET)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; },
                Resource = "api/orders/"
            };

            try
            {
                var restResult = client.Execute<List<Order>>(request).Data;

                Console.WriteLine();
                Console.WriteLine("===========================");
                foreach (var item in restResult.Where(i => i.Status == "pending"))
                {
                    Console.WriteLine("        ID:  " + item.Id);
                    Console.WriteLine("CostumerId:  " + item.CostumerId);
                    Console.WriteLine("  ItemName:  " + item.ItemName);
                    Console.WriteLine("  Quantity:  " + item.Quantity);
                    Console.WriteLine("    Status:  " + item.Status);
                    Console.WriteLine(" Direction:  " + item.Direction);
                    Console.WriteLine(" TimeStamp:  " + item.TimeStamp);
                    Console.WriteLine("===========================");
                }
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg.Message);
            }
        }
        
        //rendelések lekérése ügyfél szerint
        public List<Order> ListCustomerOrders(int customerId, bool onlyData)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var request = new RestRequest(Method.GET)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; },
                Resource = "api/orders/"
            };

            var ListofOrders = new List<Order>();
            var ListofCustomerOrders = new List<Order>();

            try
            {
                ListofOrders = client.Execute<List<Order>>(request).Data;
                foreach (var item in ListofOrders.Where(n => n.CostumerId == customerId))
                {
                    ListofCustomerOrders.Add(item);
                }

                if (!onlyData)
                {
                    Console.WriteLine();
                    Console.WriteLine("===========================");
                    foreach (var item in ListofCustomerOrders)
                    {
                        Console.WriteLine("        ID:  " + item.Id);
                        Console.WriteLine("CostumerId:  " + item.CostumerId);
                        Console.WriteLine("  ItemName:  " + item.ItemName);
                        Console.WriteLine("  Quantity:  " + item.Quantity);
                        Console.WriteLine("    Status:  " + item.Status);
                        Console.WriteLine(" Direction:  " + item.Direction);
                        Console.WriteLine(" TimeStamp:  " + item.TimeStamp);
                        Console.WriteLine("===========================");
                    }
                }
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg.Message);
            }
            return ListofCustomerOrders;
        }

        
        public void UpdateOrderStatus(int id, string status)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var order = new Order();

            order = order.GetOrderById(id);
            order.Status = status;

            var request = new RestRequest(Method.PUT)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; },
                Resource = "api/Orders/" + order.Id
            };
            request.AddJsonBody(order);
            client.Execute(request);
        }
    }
}
