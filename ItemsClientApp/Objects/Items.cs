using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization;

namespace WarehouseClient
{
    class Item
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "OwnerId")]
        public int OwnerId { get; set; }

        [DataMember(Name = "Location")]
        public int Location { get; set; }

        [DataMember(Name = "Status")]
        public string Status { get; set; }

        //GET ALL OR BY ID
        public void GetItems()
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
        public void AddItem()
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
        public void DeleteItem()
        {
            Console.Write("ID: ");
            var id = Console.ReadLine();
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var request = new RestRequest("api/items/{id}", Method.DELETE);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            client.Execute(request);
        }

        //PUT
        public void PutItem()
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
    }
}
