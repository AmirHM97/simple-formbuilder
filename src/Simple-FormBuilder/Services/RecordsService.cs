using Simple_Formbuilder.Context;
using Simple_Formbuilder.Dtos;
using Simple_Formbuilder.Entities;
using Simple_Formbuilder.Extension;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_Formbuilder.Services
{
    public class RecordsService : IRecordsService
    {
        private readonly IDbContext _dbContext;

        public RecordsService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<string> AddRecord(AddRecordDto addRecordDto)
        {
            var collection = _dbContext.GetCollection<RecordsCollection>(addRecordDto.ServiceId);
            var record = new RecordsCollection
            {
                FormId = addRecordDto.FormId,
                CreatedTime = DateTimeOffset.UtcNow,
                LastUpdatedTime = DateTimeOffset.UtcNow,
                ObjectId = addRecordDto.ObjectId,
                ServiceId = addRecordDto.ServiceId,
                TenantId = addRecordDto.TenantId,
                UserId = addRecordDto.UserId,
                RecordsData = MappingExtensions.MapAddRecordDataDtoToRecordData(addRecordDto.RecordsData)
            };
            await collection.InsertOneAsync(record);
            return record.Id;
        }
        public async Task<RecordsCollection> GetRecordById(string recordId, string serviceId)
        {
            var collection = _dbContext.GetCollection<RecordsCollection>(serviceId);
            var record = await collection.Find(f => f.Id == recordId).FirstOrDefaultAsync();
            return record;
        }
        public async Task<RecordsCollection> GetRecordByServiceId(string serviceId, string objectId, string formId)
        {
            var collection = _dbContext.GetCollection<RecordsCollection>(serviceId);
            var record = await collection.Find(f => f.ServiceId == serviceId && f.ObjectId == objectId && f.FormId == formId).FirstOrDefaultAsync();
            return record;
        }

//         public ResultGetForm getFormDetail(int formId, int? recordId)
//         {
//             #region get form
//             var form = _FormService.Find(formId);
//             var groups = form.Groups.Where(n => !n.Disable);

//             List<FormField> formFields = new List<FormField>();

//             foreach (var group in groups)
//                 formFields.AddRange(group.FormFields.Where(n => !n.Disable));

            
//             #endregion

//             List<FieldValueModel> valueList = new List<FieldValueModel>();

//             if (recordId.HasValue)
//             {
//                 DynamicTables_BL dynamicTableService = new DynamicTables_BL();

//                 var result = dynamicTableService.GetTableRecord(form, recordId.Value, addedFieldIds);

//                 result.groups.ForEach(n => valueList.AddRange(n.fields));
//             }
//             var selection = _formBuilderContext.SelectionRecords.Where(x => x.RecordId == recordId.Value);
//             if (selection != null && selection.Count() > 0)
//             {
//                 foreach (var item in selection)
//                 {
//                     valueList.Add(new FieldValueModel { DefaultSelected = item.SelectionRow.DefaultSelected, SelectionId = item.SelectionRow.Id, Order = item.SelectionRow.Order, fieldId = item.SelectionRow.FormFieldId, groupId = item.SelectionRow.FormField.GroupId, name = item.SelectionRow.Text, value = item.SelectionRow.Value, description = item.SelectionRowId.ToString() });
//                 }
//             }
//             List<dynamic> fields = new List<dynamic>();


//             foreach (var item in formFields)
//             {
//                 dynamic Matrix = "";
//                 dynamic Selection = "";
//                 dynamic Subform = "";

//                 var columnValue = valueList.FirstOrDefault(n => n.fieldId == item.Id);
// #endregion

//                 #region simple fields
//                 // if (item.SubFormId.HasValue)
//                 // {
//                 //     //getFormDetail(CurrentSession.SiteId, formId, projectRecords?.RecordId, (int)BaseInformation.ThisServiceEnum);
//                 //     Subform = getFormDetail(item.SubFormId.Value, null);
//                 //     fields.Add(new
//                 //     {
//                 //         description = item.Description,
//                 //         Hidden = item.Hidden,
//                 //         name = item.DisplayName,
//                 //         fieldName = item.FieldName,
//                 //         groupOrder = item.Group.Order,
//                 //         order = item.Order,
//                 //         Required = item.Required,
//                 //         ICD10 = item.ICD10,
//                 //         id = AcronymFieldId + item.Id,
//                 //         groupId = item.GroupId,
//                 //         fieldId = item.Id,
//                 //         fieldType = item.FieldType.ToString(),
//                 //         subForm = Subform,
//                 //         value = (recordId == null || columnValue == null) ? null : columnValue.value,
//                 //     });

//                 // }
//                 // else if (item.FieldType != FieldType.ForeignKeySubForm)
//                 // {
//                 int? minLength = null;
//                 int? maxLength = null;

//                 dynamic maxValue = null;
//                 dynamic minValue = null;

//                 #region length

//                 if (item.MinLength != null)
//                     minLength = item.MinLength.Value;
//                 if (item.MaxLength != null)
//                     maxLength = item.MaxLength.Value;

//                 #endregion

//                 #region value

//                 if (item.FieldType == FieldType.Price || item.FieldType == FieldType.Number || item.FieldType == FieldType.Text)
//                 {
//                     if (item.MinValueNumber != null)//number and price
//                         minValue = item.MinValueNumber;
//                     if (item.MaxValueNumber != null)
//                         maxValue = item.MaxValueNumber;
//                 }
//                 else if (item.FieldType == FieldType.Date)
//                 {
//                     if (item.MinValueDateTime != null)//date
//                         minValue = item.MinValueDateTime.Value;
//                     if (item.MaxValueDateTime != null)
//                         maxValue = item.MaxValueDateTime.Value;
//                 }

//                 else if (item.FieldType == FieldType.DateTime)
//                 {
//                     #region min

//                     if (item.MinValueDateTime != null)//dateTime 
//                     {
//                         minValue = item.MinValueDateTime.Value +
//                             " " + getTime(item.MinValueDateTime.Value);
//                     }

//                     #endregion

//                     #region max

//                     if (item.MaxValueDateTime != null)//dateTime
//                     {
//                         maxValue = item.MaxValueDateTime.Value +
//                             " " + getTime(item.MaxValueDateTime.Value);
//                     }

//                     #endregion

//                 }

//                 #endregion

//                 fields.Add(new
//                 {
//                     description = item.Description,
//                     Hidden = item.Hidden,
//                     name = item.DisplayName,
//                     fieldName = item.FieldName,
//                     groupOrder = item.Group.Order,
//                     order = item.Order,
//                     Required = item.Required,
//                     ICD10 = item.ICD10,
//                     id = AcronymFieldId + item.Id,
//                     groupId = item.GroupId,
//                     fieldType = item.FieldType,

//                     Free = string.IsNullOrEmpty(item.Free) ? "" : item.Free,
//                     validation = item.Validation == null ? null : new
//                     {
//                         Id = item.ValidationId,
//                         Name = item.Validation.Name,
//                         ErrorMessage = item.Validation.ErrorMessage,
//                         Expression = item.Validation.Expression
//                     },
//                     MinValue = minValue,
//                     MaxValue = maxValue,
//                     MinLength = minLength,
//                     MaxLength = maxLength,
//                     IsNumber = item.IsNumber,
//                     IsIdInDataCollector = item.IsIdInDataCollector,
//                     IsCollection = item.IsCollection,
//                     value = (recordId == null || columnValue == null) ? null : columnValue.value
//                 });
//             }

//             #endregion

//             // }
//             #region prepare jsonresult

//             var resultmodel = new ResultGetForm
//             {
//                 Id = form.Id,
//                 createdTime = form.CreatedTime,
//                 Description = form.Description,
//                 DisplayName = form.DisplayName,
//                 Name = form.Name,
//                 Icon = form.FormIcon,
//                 conditions = form.Conditions,

//                 Groups = groups.OrderBy(c => c.Order).Select(b => new ResultGetGroup
//                 {
//                     Description = b.Description,
//                     Id = b.Id,
//                     Name = b.Name,
//                     Order = b.Order,
//                     RoleId = b.RoleId,
//                     formFields = b.FormFields.OrderBy(c => c.Order).Where(x => (!x.Disable) || (addedFieldIds != null && addedFieldIds.Contains(x.Id))).Select(x => new ResultGetField
//                     {
//                         ICD10 = x.ICD10,
//                         DisplayName = x.DisplayName,
//                         Description = x.Description,
//                         Order = x.Order,
//                         Type = x.FieldType.ToString(),
//                         Hidden = x.Hidden,
//                         IsNumber = x.IsNumber,
//                         IsCollection = x.IsCollection,
//                         Required = x.Required,
//                         IsUnique = x.IsUnique,
//                         Id = x.Id,
//                         MaxLength = x.MaxLength,
//                         MinLength = x.MinLength,
//                         Min = x.MaxValueNumber,
//                         Max = x.MinValueNumber,
//                         Message = x.Message,
//                         DefaultValue = x.DefaultValue,
//                         IsSnedAuto = x.IsSnedAuto,
//                         ReseanSend = x.ReseanSend,
//                         AdditionalData = x.AdditionalData,
//                         ValueSelectionData = valueList.Where(r => r.fieldId == x.Id).ToList(),
//                         Value = getValue(valueList, x),
//                         ValueSelection = getValueSelection(valueList, x),
//                         valueFields =
//                         x.FieldType == FieldType.CheckBoxList || x.FieldType == FieldType.ComboBox || x.FieldType == FieldType.Radio ?
//                           x.SelectionRow.OrderBy(c => c.Order).Where(c => !c.Disable).Select(c => new ResultGetFieldSelection
//                           {
//                               FormFieldId = c.FormFieldId,
//                               Id = c.Id,
//                               MediaUrl = c.MediaUrl,
//                               Rate = c.Rate,
//                               Text = c.Text,
//                               Value = c.Value,
//                               Order = c.Order,
//                           }).ToList() : null
//                     }).ToList()
//                 }).ToList()
//             };

//             return resultmodel;
//             #endregion
//         }

    }
}
