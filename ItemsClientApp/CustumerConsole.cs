using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemsClientApp
{
    public class Menus
    {
        public void InputHandler()
        {
            int userInput = 0;
            do
            {
                userInput = ChoseUserMenu();
                if (userInput == 1)
                    AddOrder();
                if (userInput == 2)
                    ChoseUserMenu();
                if (userInput == 3)
                    ChoseUserMenu();
                if (userInput == 4)
                    ChoseUserMenu();
            } while (userInput != 0);
        }

        static public int ChoseUserMenu()
        {
            Console.WriteLine("\n");
            Console.WriteLine("** CUSTOMER MENU **\n");
            Console.WriteLine("1. New Order");
            Console.WriteLine("2. .........");
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

        private static void AddOrder()
        {
            var client = new RestClient("http://localhost:5000");
            var order = new Order();

            order.AddOrder();

            var request = new RestRequest("api/orders/", Method.POST);
            request.AddJsonBody(order);
            client.Execute(request);
            Console.ReadLine();
        }
    }
}
