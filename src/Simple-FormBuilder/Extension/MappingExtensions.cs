using Simple_Formbuilder.Dtos;
using Simple_Formbuilder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_Formbuilder.Extension
{
    public static class MappingExtensions
    {
        public static FormsCollection MapCreateFormDtoToFormCollection(CreateFormDto createFormDto)
        {
            var form = new FormsCollection
            {
                Name = createFormDto.Name,
                DisplayName = createFormDto.DisplayName,
                Icon = createFormDto.Icon,
                ServiceId = createFormDto.ServiceId,
                IsAvailable = createFormDto.IsAvailable,
                IsDeleted = false,
                Description = createFormDto.Description,
                Conditions = createFormDto.Conditions,
                Order = createFormDto.Order,
                CreatedTime = DateTimeOffset.UtcNow,
                LastUpdatedTime = DateTimeOffset.UtcNow,
                tenantId = createFormDto.tenantId,
                UserId = createFormDto.UserId,
                ObjectId = createFormDto.ObjectId,
                Groups = MapGroupDtoToGroup(createFormDto.Groups)
            };
            return form;
        }

        public static List<Group> MapGroupDtoToGroup(List<GroupDto> groupDto)
        {
            var groups = groupDto.Select(s => new Group
            {
                Name = s.Name,
                Description = s.Description,
                IsAvailable = s.IsAvailable,
                IsDeleted = false,
                CreatedTime = DateTimeOffset.UtcNow,
                LastUpdatedTime = DateTimeOffset.UtcNow,
                Order = s.Order,
                FormFields = MapFormFieldDtoToField(s.FormFields)
            }).ToList();

            return groups;
        }
        public static List<FormField> MapFormFieldDtoToField(List<FormFieldDto> formFieldDto)
        {
            var formFields = formFieldDto.Select(s => new FormField
            {
                Description = s.Description,
                Name = s.Name,
                Order = s.Order,
                DisplayName = s.DisplayName,
                IsAvailable = s.IsAvailable,
                IsDeleted = false,
                CreatedTime = DateTimeOffset.UtcNow,
                LastUpdatedTime = DateTimeOffset.UtcNow,
                CssClass = s.CssClass,
                DefaultValue = s.DefaultValue,
                AdditionalData = s.AdditionalData,
                MinValueNumber = s.MinValueNumber,
                MaxValueNumber = s.MaxValueNumber,
                MinValueDateTimeOffset = s.MinValueDateTimeOffset,
                MaxValueDateTimeOffset = s.MaxValueDateTimeOffset,
                MinLength = s.MinLength,
                MaxLength = s.MaxLength,
                Hidden = s.Hidden,
                Required = s.Required,
                FieldType = s.FieldType,
                SelectionRow = MapSelectionRowDtoToSelectionRow(s.SelectionRow)
            }).ToList();
            return formFields;
        }
        public static List<SelectionRow> MapSelectionRowDtoToSelectionRow(List<SelectionRowDto> selectionRowDto)
        {
            var selectionRows = selectionRowDto.Select(s => new SelectionRow
            {
                Text = s.Text,
                Value = s.Value,
                Rate = s.Rate,
                IsAvailable = s.IsAvailable,
                IsDeleted = false,
                CreatedTime = DateTimeOffset.UtcNow,
                LastUpdatedTime = DateTimeOffset.UtcNow,
                DefaultSelected = s.DefaultSelected,
                MediaUrl = s.MediaUrl,
                Order = s.Order
            }).ToList();
            return selectionRows;
        }
        public static List<RecordsData> MapAddRecordDataDtoToRecordData(List<AddRecordsDataDto> addRecordsDataDto)
        {
            return addRecordsDataDto.Select(s => new RecordsData
            {
                FieldId = s.FieldId,
                FieldType = s.FieldType,
                NumberValue = s.NumberValue,
                DateValue = s.DateValue,
                SelectionIds = s.SelectionIds,
                Value = s.Value,
                SubRecords = s.SubRecords.Select(w => new RecordsCollection { RecordsData = MapAddRecordDataDtoToRecordData(w.RecordsData) }).ToList()
            }).ToList();
        }
    }
}
