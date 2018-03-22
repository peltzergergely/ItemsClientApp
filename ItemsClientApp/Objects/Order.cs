using RestSharp;
using System;
using System.Runtime.Serialization;


namespace ItemsClientApp
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

        public void AddOrder()
        {
            Console.WriteLine("\n\n** CHOOSE DIRECTION **\n");

            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdrawal");
            var direction = Console.ReadLine();
            if (direction == "2")
            {
                this.Direction = "Withdrawal";
                Console.Clear();
                Console.WriteLine("\n\n** WITHDRAWAL **\n");
            }
            else
            {
                this.Direction = "Deposit";
                Console.Clear();
                Console.WriteLine("\n\n** DEPOSIT **\n");
            }

            Console.Write("COSTUMER ID: 2\n");
            this.CostumerId = 2;
            Console.Write("  ITEM NAME: ");
            this.ItemName = Console.ReadLine();
            Console.Write("   QUANTITY: ");
            this.Quantity = int.Parse(Console.ReadLine());
            Console.Write("     STATUS: Pending\n");
            this.Status = "pending";
            Console.Write("  DIRECTION: " + this.Direction + "\n");
            DateTime myDateTime = DateTime.Now;
            this.TimeStamp = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            Console.Write("  TIMESTAMP: " + this.TimeStamp);
        }

        public Order GetOrderById(int id)
        {
            var client = new RestClient("http://localhost:5000");
            var request = new RestRequest(Method.GET)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; },
                Resource = "api/orders/" + id
            };

            var order = new Order();

            try
            {
                order = client.Execute<Order>(request).Data;

                //Console.WriteLine();
                //Console.WriteLine("===========================");
                //{
                //    Console.WriteLine("         ID:  " + order.Id);
                //    Console.WriteLine("COSTUMER ID:  " + order.CostumerId);
                //    Console.WriteLine("   ITEMNAME:  " + order.ItemName);
                //    Console.WriteLine("   QUANTITY:  " + order.Quantity);
                //    Console.WriteLine("     STATUS:  " + order.Status);
                //    Console.WriteLine("  DIRECTION:  " + order.Direction);
                //    Console.WriteLine("  TIMESTAMP:  " + order.TimeStamp);
                //    Console.WriteLine("===========================");
                //}
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg.Message);
            }
            return order;
        }

        //sadly patch isn't working have to load in and then change
        //load in order by id, then change order.status and put
        public void UpdateOrderStatus(int id, string status)
        {
            var client = new RestClient("http://localhost:5000");
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
