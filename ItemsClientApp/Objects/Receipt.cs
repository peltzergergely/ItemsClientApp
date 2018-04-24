using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using RestSharp;
using System.Configuration;

namespace WarehouseClient
{
    class Receipt
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "CostumerId")]
        public int CostumerId { get; set; }

        [DataMember(Name = "TransactionId")]
        public int TransactionId { get; set; }

        [DataMember(Name = "TimeStamp")]
        public string TimeStamp { get; set; }

        [DataMember(Name = "StorekeeperId")]
        public int StorekeeperId { get; set; }

        [DataMember(Name = "Comment")]
        public string Comment { get; set; }


        //nyugta létrehozása
        public void AddReceipt(int storekID)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var receipt = new Receipt();
            var transaction = new Transaction();
            var order = new Order();
            var customer = new Customer();

            

            Console.WriteLine("\n\n** CREATE RECEIPT **\n");

            Console.Write("        TRANSACTION ID: ");
            int TransactionId = int.Parse(Console.ReadLine());
            //get transaction details
            transaction = transaction.GetTransactionById(TransactionId);
            receipt.TransactionId = transaction.Id;

            //order lekérése transID alapján
            order = order.GetOrderById(transaction.OrderId);

            Console.Write("        CUSTOMER ID: " + order.CostumerId + "\n");
            receipt.CostumerId = order.CostumerId;

            DateTime myDateTime = DateTime.Now;
            receipt.TimeStamp = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            Console.Write("        TIMESTAMP: " + receipt.TimeStamp + "\n");
            
            receipt.StorekeeperId = storekID;
            Console.Write("        STOREKEEPER ID: " + receipt.StorekeeperId.ToString() + "\n");
            Console.Write("        COMMENT: ");
            receipt.Comment = Console.ReadLine();

            var request = new RestRequest("api/Receipts/", Method.POST);
            request.AddJsonBody(receipt);
            client.Execute(request);
            
            //change the status of the transaction
            transaction.UpdateTransactionStatus(transaction.Id, "completed");
            customer = customer.GetCustomerById(order.CostumerId);

            int newStorage = 0;
            if (order.Direction == "Deposit")
            {
                newStorage = Convert.ToInt32(customer.FreeStorage) - order.Quantity;
            }
            else
            {
                newStorage = Convert.ToInt32(customer.FreeStorage) + order.Quantity;
            }
            //Console.Write(newStorage + "\n");

            customer.FreeStorage = Convert.ToString(newStorage);
            //Console.Write(customer.FreeStorage + "\n");
            customer.UpdateCustomerFreeStorage(customer);

        }


        //összes nyugta lekérése
        public List<Receipt> ListCustomerReceipts(int custId, bool onlyData)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["serverConn"]);
            var request = new RestRequest(Method.GET)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; },
                Resource = "api/Receipts/"
            };

            var ListofReceipts = new List<Receipt>();
            var ListofCustomerReceipts = new List<Receipt>();

            try
            {
                ListofReceipts = client.Execute<List<Receipt>>(request).Data;
                //nyugták kiválogatása ügyfélID alapján
                foreach (var item in ListofReceipts.Where(R => R.CostumerId == custId))
                {
                    ListofCustomerReceipts.Add(item);
                }

                if (!onlyData)
                {
                    Console.WriteLine();
                    Console.WriteLine("===========================");
                    foreach (var item in ListofCustomerReceipts)
                    {
                        Console.WriteLine("            ID:  " + item.Id);
                        Console.WriteLine("    CostumerId:  " + item.CostumerId);
                        Console.WriteLine(" TransactionId:  " + item.TransactionId);
                        Console.WriteLine("     TimeStamp:  " + item.TimeStamp);
                        Console.WriteLine(" StorekeeperId:  " + item.StorekeeperId);
                        Console.WriteLine("       Comment:  " + item.Comment);
                        Console.WriteLine("===========================");
                    }
                }
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg.Message);
            }
            return ListofCustomerReceipts;
        }
    }
}