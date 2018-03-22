using System.Runtime.Serialization;

namespace ItemsClientApp
{
    public class Item
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
    }
}
