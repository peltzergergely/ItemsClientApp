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
            var storekeeper = new Storekeeper();
            storekeeper = LoginStorekeeper();

            if (storekeeper.Id != 0)
            {
                int userInput = 0;
                do
                {
                    userInput = StorekeeperMenu();
                    if (userInput == 1)
                        GetTransactions();
                    if (userInput == 2)
                        GetInProgressTransactions();
                    if (userInput == 3)
                        CreateReceipt(storekeeper.Id);
                    if (userInput == 4)
                        StorekeeperMenu();
                } while (userInput != 0);
            }
        }

        static public int StorekeeperMenu()
        {
            Console.WriteLine("\n");
            Console.WriteLine("** STOREKEEPER MENU **\n");
            Console.WriteLine("1. List Transactions");
            Console.WriteLine("2. List InProgress Transactions");
            Console.WriteLine("3. Create Receit");
            //Console.WriteLine("4. .........");
            Console.WriteLine("0. Exit\n");
            Console.Write(" Chose Option: ");
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
        //raktáros belépés
        private static Storekeeper LoginStorekeeper()
        {
            var storekeeper = new Storekeeper();
            return storekeeper.Login();
        }
        //tranzakciók lekérése
        private static void GetTransactions()
        {
            var transaction = new Transaction();
            transaction.ListTransactions(false);
        }
        //nyugta létrehozása
        private static void CreateReceipt(int storekeeperID)
        {
            var storekID = storekeeperID;
            var receipt = new Receipt();
            receipt.AddReceipt(storekID);
        }
        //folyamatban lévő tranzakciók lekérése
        private static void GetInProgressTransactions()
        {
            var transaction = new Transaction();
            transaction.ListInProgressTransactions();
        }
    }
}
