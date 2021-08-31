using Simple_Formbuilder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_Formbuilder.Dtos
{
    public class EditFormDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string GuidName { get; set; }
        public string Icon { get; set; }
        public bool IsAvailable { get; set; }
        public string Description { get; set; }
        public string Conditions { get; set; }
        public string AdditionalData { get; set; }
        public int Order { get; set; }
      

        public List<EditGroupDto> Groups { get; set; }
    }
    public class EditGroupDto
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool IsAvailable { get; set; }
        public List<EditFormFieldDto> FormFields { get; set; } 
    }
    public class EditFormFieldDto
    {
        public Guid Id { get; set; } 
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
        public List<EditSelectionRowDto>? SelectionRow { get; set; } = new();
    }
    public class EditSelectionRowDto
    {
        public Guid Id { get; set; } 
        public int Order { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public string MediaUrl { get; set; }
        public double Rate { get; set; }
        public bool IsAvailable { get; set; }
        public bool DefaultSelected { get; set; }
    }
}
