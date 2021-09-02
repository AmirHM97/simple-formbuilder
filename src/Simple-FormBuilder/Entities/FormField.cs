using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Simple_Formbuilder.Entities
{
    public class FormField
    {
        // [BsonIgnoreIfDefault]
        // [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; }
        public bool Hidden { get; set; }
        public bool Required { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public string CssClass { get; set; }
        public string DefaultValue { get; set; }
        public string AdditionalData { get; set; }
        public float? MinValueNumber { get; set; }
        public float? MaxValueNumber { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public FieldType FieldType { get; set; }
        public DateTimeOffset? MinValueDateTimeOffset { get; set; }
        public DateTimeOffset? MaxValueDateTimeOffset { get; set; }
        public List<SelectionRow> SelectionRow { get; set; } = new();

    }
    public enum FieldType
    {
        Text, MediumText, Textarea, Email, WebSite, CheckBox, Password,
        Number, Price,
        Date, DateTimeOffset, Time,
        PageBreak, SectionBreak,
        Radio,
        ComboBox,
        CheckBoxList,
        SubForm, ForeignKeySubForm, Location, UploadBox,
        Description,
        Plaque,
        Star,
        Voice,
        Video,
        ComboBoxList,
        FloatingBar,
        Phone
    }

}
