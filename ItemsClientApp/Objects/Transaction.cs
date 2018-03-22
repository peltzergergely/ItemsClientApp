using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ItemsClientApp
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

        //gotta pass selected order with informations to this one
        public Transaction AddTransaction()
        {
            var transaction = new Transaction();
            var order = new Order();

            Console.WriteLine("\n\n** CREATE TRANSACTION **\n");
            Console.Write("          Order ID: ");
            int orderId = int.Parse(Console.ReadLine());
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

            //transaction.OrderId = 3;
            //transaction.Gate = 2;
            //transaction.Time = "03-26 14:00";
            //transaction.Location = 2;
            //transaction.Direction = "Deposit";
            //transaction.TimeStamp = "2018-03-22T23:28:47";
            //transaction.DispatcherId = 1;


            //order.UpdateOrderStatus(order.Id, "processed");

            return transaction;
        }
    }
}
