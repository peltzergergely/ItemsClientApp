using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace WarehouseClient
{
    class Costumer
    {
        [DataMember(Name = "Id")]
        public int Id { get; }

        [DataMember(Name = "Name")]
        public string Name { get; } = "user1";
    }
}
