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
                    ChoseUserMenu();
                if (userInput == 2)
                    ChoseUserMenu();
                if (userInput == 3)
                    ChoseUserMenu();
                if (userInput == 4)
                {
                    var iteminterface = new ItemHandler();
                    iteminterface.MenuPicker();
                }
            } while (userInput != 0);
        }

        static public int ChoseUserMenu()
        {
            Console.WriteLine("1. COSTUMER");
            Console.WriteLine("2. DISPATCHER");
            Console.WriteLine("3. WH PERSON");
            Console.Write("INPUT: ");
            var result = Console.ReadLine();
            return Convert.ToInt32(result);
        }
    }
}
