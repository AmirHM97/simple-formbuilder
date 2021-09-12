using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Simple_Formbuilder.Entities
{
    public class FormsCollection
    {
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Icon { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; }
        public string Description { get; set; }
        public string Conditions { get; set; }
        public int Order { get; set; }
        public string AdditionalData { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public string TenantId { get; set; }
        public string? UserId { get; set; }
        public string? ObjectId { get; set; }
        public string? ServiceId { get; set; }

        public List<Group>  Groups { get; set; }
    }

    

}
