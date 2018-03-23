using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WarehouseClient.Objects
{
    class Dispatcher
    {
        [DataMember(Name = "Id")]
        public int Id { get; } = 1;

        [DataMember(Name = "Name")]
        public string Name { get; } = "dispatcher1";
    }
}
