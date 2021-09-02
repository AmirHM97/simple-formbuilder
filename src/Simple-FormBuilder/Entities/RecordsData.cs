using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Simple_Formbuilder.Entities
{
    public class RecordsData
    {
        // [BsonIgnoreIfDefault]
        // [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        // [BsonIgnoreIfDefault]
        // [BsonRepresentation(BsonType.ObjectId)]
        public string FieldId { get; set; }  = Guid.NewGuid().ToString();
        public FieldType FieldType { get; set; }
        public string Value { get; set; }
        public List<RecordsCollection> SubRecords { get; set; }
        public decimal? NumberValue { get; set; }
        public DateTimeOffset? DateValue { get; set; }
        public List<string> SelectionIds { get; set; }
    }
}
