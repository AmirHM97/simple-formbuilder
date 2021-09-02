using Simple_Formbuilder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_Formbuilder.Dtos
{
    public class AddRecordDto
    {
        public string TenantId { get; set; }
        public string ServiceId { get; set; }
        public string UserId { get; set; }
        public string FormId { get; set; }
        public string ObjectId { get; set; }
        public List<AddRecordsDataDto> RecordsData { get; set; }
    }
    public class AddRecordsDataDto
    {
        public string FieldId { get; set; }
        public FieldType FieldType { get; set; }
        public string Value { get; set; }
        public List<AddRecordDto> SubRecords { get; set; }
        public decimal? NumberValue { get; set; }
        public DateTimeOffset? DateValue { get; set; }
        public List<string> SelectionIds { get; set; }
    }
}
