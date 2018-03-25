using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseClient
{
    class ItemsHanderPOC
    {
        public void MenuPicker()
        {
            var item = new Item();
            int userInput = 0;
            do
            {
                userInput = DisplayMenu();
                if (userInput == 1)
                    item.GetItems();
                if (userInput == 2)
                    item.AddItem();
                if (userInput == 3)
                    item.PutItem();
                if (userInput == 4)
                    item.DeleteItem();
            } while (userInput != 0);
        }

        static public int DisplayMenu()
        {
            Console.WriteLine("\n");
            Console.WriteLine("** Items Menu **\n");
            Console.WriteLine("1. LIST ITEMS (BY ID)");
            Console.WriteLine("2. ADD ITEM");
            Console.WriteLine("3. UPDATE ITEM");
            Console.WriteLine("4. DELETE ITEM BY ID");
            Console.WriteLine("0. Exit");
            Console.Write("INPUT: ");
            var result = Console.ReadLine();
            return Convert.ToInt32(result);
        }
    }
}
