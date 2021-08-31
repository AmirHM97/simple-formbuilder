using Simple_Formbuilder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_Formbuilder.Dtos
{
    public class CreateFormDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Icon { get; set; }
        public bool IsAvailable { get; set; }
        public string Description { get; set; }
        public string Conditions { get; set; }
        public int Order { get; set; }
        public string tenantId { get; set; }
        public string? UserId { get; set; }
        public string? ObjectId { get; set; }
        public string? ServiceId { get; set; }
        public string AdditionalData { get; set; }

      

        public List<GroupDto> Groups { get; set; } = new();

    }
    public class GroupDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool IsAvailable { get; set; }
        public List<FormFieldDto> FormFields { get; set; } = new();
    }
    public class FormFieldDto
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string DisplayName { get; set; }
        public bool IsAvailable { get; set; }

        public string CssClass { get; set; }
        public string DefaultValue { get; set; }
        public string AdditionalData { get; set; }
        public string AllowExtensions { get; set; }
        public int FileSizeLimit { get; set; }
        public int FilesLimit { get; set; }

        public int? MinRepeatTimes { get; set; }
        public int? MaxRepeatTimes { get; set; }
        public float? MinValueNumber { get; set; }
        public float? MaxValueNumber { get; set; }
        public DateTimeOffset? MinValueDateTimeOffset { get; set; }
        public DateTimeOffset? MaxValueDateTimeOffset { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public bool Hidden { get; set; }
        public bool Required { get; set; }
        public FieldType FieldType { get; set; }
        public List<SelectionRowDto>? SelectionRow { get; set; } = new();
    }
    public class SelectionRowDto
    {
        public int Order { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public string MediaUrl { get; set; }
        public double Rate { get; set; }
        public bool IsAvailable { get; set; }
        public bool DefaultSelected { get; set; }
    }


}
