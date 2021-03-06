﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseClient
{
    public class CustomerConsole
    {
        public void InputHandler()
        {
            var customer = new Customer();
            customer = LoginCustomer();

            if (customer != null && customer.Id != 0)
            {
                int userInput = 0;
                do
                {
                    userInput = ChoseUserMenu();
                    if (userInput == 1)
                        PlaceOrder(customer);
                    if (userInput == 2)
                        ListOrders(customer.Id);
                    if (userInput == 3)
                        ListOfTransactionsByOrderForCustomer(customer);
                    if (userInput == 4)
                        ListReceipts(customer.Id);
                    if (userInput == 5)
                        ListStoredItems(customer.Id);
                } while (userInput != 0);
            }
        }

        static public int ChoseUserMenu()
        {
            Console.WriteLine("\n");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("        ** CUSTOMER MENU **");
            Console.WriteLine("-------------------------------------\n");
            Console.WriteLine(" 1. New Order");
            Console.WriteLine(" 2. Submitted Orders");
            Console.WriteLine(" 3. Transactions");
            Console.WriteLine(" 4. Receipts");
            Console.WriteLine(" 5. List Stored Items\n");
            Console.WriteLine(" 0. Exit\n");
            Console.WriteLine("-------------------------------------\n");
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

        private static void ListOrders(int customerId)
        {
            var order = new Order();
            order.ListCustomerOrders(customerId, false);
        }

        private static void ListOfTransactionsByOrderForCustomer(Customer customer)
        {
            customer.ListTransactions();
        }

        private static void ListReceipts(int customerId)
        {
            var receipt = new Receipt();
            receipt.ListCustomerReceipts(customerId, false);
        }
        //show relevant transaction
        
        //list stored items
        private static void ListStoredItems(int customerId)
        {
            var item = new Item();
            item.GetItemsByCustomerId(customerId);
            
        }
    }
}
