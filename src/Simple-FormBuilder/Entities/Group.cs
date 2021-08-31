using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_Formbuilder.Entities
{
    public class Group
    {
       
        public Guid Id { get; set; }= Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; }
        public List<FormField> FormFields { get; set; } = new();
    }
}
