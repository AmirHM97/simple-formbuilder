using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_Formbuilder.Entities
{
    public class RecordsCollection
    {
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string TenantId { get; set; }
        public string ServiceId { get; set; }
        public string UserId { get; set; }
        public string FormId { get; set; }
        public string ObjectId { get; set; }
        public bool IsDelete { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public List<RecordsData> RecordsData { get; set; } = new();
    }
}
