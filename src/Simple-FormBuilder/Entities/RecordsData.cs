using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_Formbuilder.Entities
{
    public class RecordsData
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid FieldId { get; set; } = Guid.NewGuid();
        public FieldType FieldType { get; set; }
        public string Value { get; set; }
        public List<RecordsCollection> SubRecords { get; set; }
        public decimal? NumberValue { get; set; }
        public DateTimeOffset? DateValue { get; set; }
        public List<Guid> SelectionIds { get; set; }
    }
}
