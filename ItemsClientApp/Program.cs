using ItemsClientApp;
using RestSharp;
using System;
using System.Collections.Generic;

namespace WarehouseClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuPicker();
        }

        public static void MenuPicker()
        {
            int userInput = 0;
            do
            {
                userInput = MenuDisplayer();
                if (userInput == 1)
                {
                    Console.Clear();
                    CostumerConsole custcon = new CostumerConsole();
                    custcon.InputHandler();
                    Console.Clear();
                }
                if (userInput == 2)
                {
                    Console.Clear();
                    DispatcherConsole dispcon = new DispatcherConsole();
                    dispcon.InputHandler();
                    Console.Clear();
                }
                if (userInput == 3)
                {
                    Console.Clear();
                    var storekeeperconsole = new StorekeeperConsole();
                    storekeeperconsole.InputHandler();
                    Console.Clear();
                }
                if (userInput == 4)
                {
                    Console.Clear();
                    var itemhandler = new ItemsHanderPOC();
                    itemhandler.MenuPicker();
                    Console.Clear();
                }

            } while (userInput != 0);
        }

        static public int MenuDisplayer()
        {
            Console.WriteLine("\n");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("             * MAIN MENU *           ");
            Console.WriteLine("-------------------------------------\n");
            Console.WriteLine("   1. COSTUMER");
            Console.WriteLine("   2. DISPATCHER");
            Console.WriteLine("   3. STOREKEEPER");
            Console.WriteLine("   4. ITEMS\n");
            Console.WriteLine("   0. EXIT\n");
            Console.WriteLine("-------------------------------------\n");
            Console.Write("Chose Option: ");
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

    }
}
