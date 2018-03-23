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
            int userInput = 0;
            do
            {
                userInput = DisplayMenu();
                if (userInput == 1)
                    GetItems();
                if (userInput == 2)
                    AddItem();
                if (userInput == 3)
                    PutItem();
                if (userInput == 4)
                    DeleteItem();
                if (userInput == 5)
                    PatchItem();
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
            Console.WriteLine("5. PATCH ITEM");
            Console.WriteLine("0. Exit");
            Console.Write("INPUT: ");
            var result = Console.ReadLine();
            return Convert.ToInt32(result);
        }

        //GET ALL OR BY ID
        private static void GetItems()
        {
            Console.Write("ADD ID OR LEAVE EMPTY: ");
            var id = Console.ReadLine();
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var request = new RestRequest(Method.GET)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; },
                Resource = "api/items/"
            };

            //Get by Id
            if (id != "")
            {
                request.Resource += id;
            }

            try
            {
                var restResult = client.Execute<List<Item>>(request).Data;

                Console.WriteLine();
                Console.WriteLine("===========================");
                foreach (var item in restResult)
                {
                    Console.WriteLine("       ID:  " + item.Id);
                    Console.WriteLine("ITEM NAME:  " + item.Name);
                    Console.WriteLine("  OWNERID:  " + item.OwnerId);
                    Console.WriteLine(" LOCATION:  " + item.Location);
                    Console.WriteLine("   STATUS:  " + item.Status);
                    Console.WriteLine("===========================");
                }
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg.Message);
            }
        }

        //POST
        private static void AddItem()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var itemToAdd = new Item();

            Console.Write("ITEM NAME: ");
            itemToAdd.Name = Console.ReadLine();
            Console.Write(" OWNER ID: ");
            itemToAdd.OwnerId = int.Parse(Console.ReadLine());
            Console.Write(" LOCATION: ");
            itemToAdd.Location = int.Parse(Console.ReadLine());
            Console.Write("   STATUS: ");
            itemToAdd.Status = Console.ReadLine();

            var request = new RestRequest("api/items/", Method.POST);
            request.AddJsonBody(itemToAdd);
            client.Execute(request);
        }

        //DELETE
        private static void DeleteItem()
        {
            Console.Write("ID: ");
            var id = Console.ReadLine();
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var request = new RestRequest("api/items/{id}", Method.DELETE);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            client.Execute(request);
        }

        //PUT
        private static void PutItem()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var itemToAdd = new Item();

            Console.Write("  ITEM ID: ");
            itemToAdd.Id = int.Parse(Console.ReadLine());
            Console.Write("ITEM NAME: ");
            itemToAdd.Name = Console.ReadLine();
            Console.Write("    OWNER: ");
            itemToAdd.OwnerId = int.Parse(Console.ReadLine());
            Console.Write(" POSITION: ");
            itemToAdd.Location = int.Parse(Console.ReadLine());
            Console.Write("   STATUS: ");
            itemToAdd.Status = Console.ReadLine();

            var request = new RestRequest(Method.PUT)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; },
                Resource = "api/items/" + itemToAdd.Id
            };
            request.AddJsonBody(itemToAdd);
            client.Execute(request);
        }

        //PATCH
        private static void PatchItem()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var itemToAdd = new Item();

            Console.Write("  ITEM ID: ");
            itemToAdd.Id = int.Parse(Console.ReadLine());
            Console.Write("ITEM NAME: ");
            itemToAdd.Name = Console.ReadLine();
            //Console.Write("    OWNER: ");
            //itemToAdd.OwnerId = int.Parse(Console.ReadLine());
            //Console.Write(" POSITION: ");
            //itemToAdd.Location = int.Parse(Console.ReadLine());
            //Console.Write("   STATUS: ");
            //itemToAdd.Status = Console.ReadLine();

            var request = new RestRequest(Method.PATCH)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; },
                Resource = "api/items/" + itemToAdd.Id
            };
            request.AddJsonBody(itemToAdd);
            client.Execute(request);
        }
    }
}
