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
        public string Location { get; set; }

        [DataMember(Name = "Status")]
        public string Status { get; set; }

        //GET ALL OR BY ID
        public List<Item> GetItems(bool onlyData, string id = "")
        {
            if (!onlyData)
            {
                Console.Write("ADD ID OR LEAVE EMPTY: ");
                id = Console.ReadLine();
            }

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

            var itemList = new List<Item>();

            try
            {
                var restResult = client.Execute<List<Item>>(request).Data;

                if (!onlyData)
                {
                    Console.WriteLine();
                    Console.WriteLine("===========================");
                }
                foreach (var item in restResult)
                {
                    if (!onlyData)
                    {
                        Console.WriteLine("       ID:  " + item.Id);
                        Console.WriteLine("ITEM NAME:  " + item.Name);
                        Console.WriteLine("  OWNERID:  " + item.OwnerId);
                        Console.WriteLine(" LOCATION:  " + item.Location);
                        Console.WriteLine("   STATUS:  " + item.Status);
                        Console.WriteLine("===========================");
                    }
                    itemList.Add(item);
                }
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg.Message);
            }

            return itemList;
        }

        //POST
        public void AddItem(bool onlyData)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);

            if (!onlyData)
            {
                Console.Write("ITEM NAME: ");
                this.Name = Console.ReadLine();
                Console.Write(" OWNER ID: ");
                this.OwnerId = int.Parse(Console.ReadLine());
                Console.Write(" LOCATION: ");
                this.Location = Console.ReadLine();
                Console.Write("   STATUS: ");
                this.Status = Console.ReadLine();
            }

            var request = new RestRequest("api/items/", Method.POST);
            request.AddJsonBody(this);
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
        public void PutItem(bool onlyData, int id = 0)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);

            this.Id = id;

            if (!onlyData)
            {
                Console.Write("  ITEM ID: ");
                this.Id = int.Parse(Console.ReadLine());
                Console.Write("ITEM NAME: ");
                this.Name = Console.ReadLine();
                Console.Write("    OWNER: ");
                this.OwnerId = int.Parse(Console.ReadLine());
                Console.Write(" POSITION: ");
                this.Location = Console.ReadLine();
                Console.Write("   STATUS: ");
                this.Status = Console.ReadLine();
            }

            var request = new RestRequest(Method.PUT)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; },
                Resource = "api/items/" + this.Id
            };
            request.AddJsonBody(this);
            client.Execute(request);
        }
    }
}
