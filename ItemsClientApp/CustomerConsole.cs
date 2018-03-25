﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseClient
{
    public class CostumerConsole
    {
        public void InputHandler()
        {
            var customer = new Customer();
            customer = LoginCustomer();

            int userInput = 0;
            do
            {
                userInput = ChoseUserMenu();
                if (userInput == 1)
                    PlaceOrder(customer);
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
            Console.WriteLine("2. Submitted Orders");
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

        private static Customer LoginCustomer()
        {
            var customer = new Customer();
            return customer.Login();
        }

        private static void PlaceOrder(Customer customer)
        {
            var order = new Order();
            order.AddOrder(customer);
        }
    }
}
