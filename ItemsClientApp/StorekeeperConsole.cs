using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseClient;

namespace ItemsClientApp
{
    class StorekeeperConsole
    {
        public void InputHandler()
        {
            int userInput = 0;
            do
            {
                userInput = StorekeeperMenu();
                if (userInput == 1)
                    GetTransactions();
                if (userInput == 2)
                    StorekeeperMenu();
                if (userInput == 3)
                    StorekeeperMenu();
                if (userInput == 4)
                    StorekeeperMenu();
            } while (userInput != 0);
        }

        static public int StorekeeperMenu()
        {
            Console.WriteLine("\n");
            Console.WriteLine("** STOREKEEPER MENU **\n");
            Console.WriteLine("1. List Transactions");
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

        private static void GetTransactions()
        {
            var transaction = new Transaction();
            transaction.GetTransactions();
        }

    }
}
