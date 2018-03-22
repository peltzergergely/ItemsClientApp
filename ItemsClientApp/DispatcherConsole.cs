using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemsClientApp
{
    class Dispatcher
    {
        public void InputHandler()
        {
            int userInput = 0;
            do
            {
                userInput = ChoseUserMenu();
                if (userInput == 1)
                    GetOrders();
                if (userInput == 2)
                    CreateTransaction();
                if (userInput == 3)
                    ChoseUserMenu();
                if (userInput == 4)
                    ChoseUserMenu();
            } while (userInput != 0);
        }

        static public int ChoseUserMenu()
        {
            Console.WriteLine("\n");
            Console.WriteLine("** DISPATCHER MENU **\n");
            Console.WriteLine("1. List New Orders");
            Console.WriteLine("2. Create Transaction");
            Console.WriteLine("3. .........");
            Console.WriteLine("4. .........");
            Console.WriteLine("0. Exit");
            Console.Write("INPUT: ");
            var result = Console.ReadLine();
            Console.Clear();
            try
            {
                return Convert.ToInt32(result);
            }
            catch (FormatException)
            {
                Console.WriteLine("\n ** INPUT CAN ONLY BE NUMERIC PLEASE TRY AGAIN **");
                return 9;
            }
        }

        private static void GetOrders()
        {
            //Console.Write("ADD ID OR LEAVE EMPTY: ");
            //var id = Console.ReadLine();
            var client = new RestClient("http://localhost:5000");
            var request = new RestRequest(Method.GET)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; },
                Resource = "api/orders/"
            };

            //Get by Id
            //if (id != "")
            //{
            //    request.Resource += id;
            //}

            try
            {
                var restResult = client.Execute<List<Order>>(request).Data;

                Console.WriteLine();
                Console.WriteLine("===========================");
                foreach (var item in restResult)
                {
                    if (item.Status == "pending")
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
        }

        //get order id, change order status to "processed", load in order fields.
        private static void CreateTransaction()
        {
            var client = new RestClient("http://localhost:5000");
            var transaction = new Transaction();

            transaction = transaction.AddTransaction();

            var request = new RestRequest("api/Transactions/", Method.POST);
            request.AddJsonBody(transaction);
            client.Execute(request);
            Console.ReadLine();
        }
    }
}
