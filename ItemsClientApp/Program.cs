using RestSharp;
using System;
using System.Collections.Generic;

namespace ItemsClientApp
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
                    Menus custcon = new Menus();
                    custcon.InputHandler();
                    Console.Clear();
                }
                if (userInput == 2)
                {
                    Console.Clear();
                    Dispatcher dispcon = new Dispatcher();
                    dispcon.InputHandler();
                    Console.Clear();
                }
                if (userInput == 3)
                    MenuDisplayer();
                if (userInput == 4)
                {
                    Console.Clear();
                    var itemhandler = new ItemHandler();
                    itemhandler.MenuPicker();
                    Console.Clear();
                }

            } while (userInput != 0);
        }

        static public int MenuDisplayer()
        {
            Console.WriteLine("\n");
            Console.WriteLine("** MAIN MENU **\n");
            Console.WriteLine("1. COSTUMER");
            Console.WriteLine("2. DISPATCHER");
            Console.WriteLine("3. WH PERSON");
            Console.WriteLine("4. Items");
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

    }
}
