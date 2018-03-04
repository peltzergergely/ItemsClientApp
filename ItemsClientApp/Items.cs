﻿using System.Runtime.Serialization;

namespace ItemsClientApp
{
    public class Item
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Owner")]
        public string Owner { get; set; }

        [DataMember(Name = "Pos")]
        public int Pos { get; set; }
    }
}
