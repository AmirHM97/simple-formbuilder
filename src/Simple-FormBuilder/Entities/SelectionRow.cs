using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Simple_Formbuilder.Entities
{
    public class SelectionRow
    {
        // [BsonIgnoreIfDefault]
        // [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }  = Guid.NewGuid().ToString();
        // public Guid FormFieldId { get; set; }
        public int Order { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public string MediaUrl { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public double Rate { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; }
        public bool DefaultSelected { get; set; }
    }
}
