using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseClient
{
    class DispatcherConsole
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
            var order = new Order();
            order.ListPendingOrders();
        }

        private static void CreateTransaction()
        {
            var transaction = new Transaction();
            transaction.AddTransaction();
        }
    }
}
