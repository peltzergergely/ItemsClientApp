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
            var dispatcher = new Dispatcher();
            dispatcher = LoginDispatcher();

            if (dispatcher.Id != 0)
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
                        ListAllTransactions();
                    if (userInput == 4)
                        ChoseUserMenu();
                } while (userInput != 0);
            }
        }

        static public int ChoseUserMenu()
        {
            Console.WriteLine("\n");
            Console.WriteLine("** DISPATCHER MENU **\n");
            Console.WriteLine("1. List New Orders");
            Console.WriteLine("2. Create Transaction");
            Console.WriteLine("3. List All Transactions");
            Console.WriteLine("4. .........");
            Console.WriteLine("0. Exit\n");
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

        private static Dispatcher LoginDispatcher()
        {
            var dispatcher = new Dispatcher();
            return dispatcher.Login();
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

        //
        private static void ListAllTransactions()
        {
            var transaction = new Transaction();
            transaction.ListTransactions(false);
        }
    }
}
