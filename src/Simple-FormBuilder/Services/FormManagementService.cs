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
    public class FormManagementService : IFormManagementService
    {
        private readonly IDbContext _dbContext;

        public FormManagementService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //============CREATE===============
        public async Task<string> CreateForm(CreateFormDto createFormDto)
        {
            var formCollection = _dbContext.FormsCollection;
            var form = MappingExtensions.MapCreateFormDtoToFormCollection(createFormDto);
            await formCollection.InsertOneAsync(form);
            return form.Id;
        }
        //=============READ================
        public async Task<FormsCollection> GetForm(string formId)
        {
            var formCollection = _dbContext.FormsCollection;
            var form = await formCollection.Find(f => f.Id == formId).FirstOrDefaultAsync();
            return form;
        }
        public async Task<FormsCollection> GetForm(string formId, string tenantId)
        {
            var formCollection = _dbContext.FormsCollection;
            var form = await formCollection.Find(f => f.Id == formId && f.TenantId == tenantId).FirstOrDefaultAsync();
            return form;
        }
        public async Task<List<FormsCollection>> GetAllForms(int pageNumber = 1, int pageSize = 10)
        {
            int skip = (pageNumber - 1) * pageSize;
            var formCollection = _dbContext.FormsCollection;
            var forms = await formCollection.Find(_ => true).Skip(skip).Limit(pageSize).ToListAsync();
            if (forms.Count == 0)
            {
                return new List<FormsCollection>();
            }
            return forms;
        }
        public async Task<List<FormsCollection>> GetUserForms(string userId, int pageNumber = 1, int pageSize = 10)
        {
            int skip = (pageNumber - 1) * pageSize;
            var formCollection = _dbContext.FormsCollection;
            var forms = await formCollection.Find(f => f.UserId == userId).Skip(skip).Limit(pageSize).ToListAsync();
            if (forms.Count == 0)
            {
                return new List<FormsCollection>();
            }
            return forms;
        }
        public async Task<List<FormsCollection>> GetAvailableFormsOftenant(string tenantId, int pageNumber = 1, int pageSize = 10)
        {
            int skip = (pageNumber - 1) * pageSize;
            var formCollection = _dbContext.FormsCollection;
            var forms = await formCollection.Find(f => f.TenantId == tenantId && f.IsAvailable).Skip(skip).Limit(pageSize).ToListAsync();
            if (forms.Count == 0)
            {
                return new List<FormsCollection>();
            }
            return forms;
        }
        public async Task<List<FormsCollection>> GetAllFormsOftenant(string tenantId, int pageNumber = 1, int pageSize = 10)
        {
            int skip = (pageNumber - 1) * pageSize;
            var formCollection = _dbContext.FormsCollection;
            var forms = await formCollection.Find(f => f.TenantId == tenantId).Skip(skip).Limit(pageSize).ToListAsync();
            if (forms.Count == 0)
            {
                return new List<FormsCollection>();
            }
            return forms;
        }
        public async Task<List<FormsCollection>> GetFormsByIdList(List<string> idList, int pageNumber = 1, int pageSize = 10)
        {
            int skip = (pageNumber - 1) * pageSize;
            var formCollection = _dbContext.FormsCollection;
            var forms = await formCollection.Find(f => idList.Contains(f.Id)).Skip(skip).Limit(pageSize).ToListAsync();
            if (forms.Count == 0)
            {
                return new List<FormsCollection>();
            }
            return forms;
        }
        //===========EDIT=================
        public async Task<bool> EditForm(EditFormDto editFormDto)
        {
            var collection = _dbContext.FormsCollection;
            var form = await GetForm(editFormDto.Id);
            form.Name = editFormDto.Name;
            form.DisplayName = editFormDto.DisplayName;
            form.Icon = editFormDto.Icon;
            form.Description = editFormDto.Description;
            form.Conditions = editFormDto.Conditions;
            form.Order = editFormDto.Order;
            form.LastUpdatedTime = DateTimeOffset.UtcNow;
            form.AdditionalData = editFormDto.AdditionalData;
            form.Groups = form.Groups.Select(s => { s.IsDeleted = true; return s; }).ToList();
            foreach (var item in editFormDto.Groups)
            {
                var group = form.Groups.FirstOrDefault(f => f.Id == item.Id);
                if (group is null)
                {
                    //! add new
                    var newGroup = new Group
                    {
                        IsAvailable = item.IsAvailable,
                        IsDeleted = false,
                        Name = item.Name,
                        Order = item.Order,
                        Description = item.Description,
                        CreatedTime = DateTimeOffset.UtcNow,
                        LastUpdatedTime = DateTimeOffset.UtcNow,
                        FormFields = item.FormFields.Select(s => new FormField
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
                            Hidden = s.Hidden ?? false,
                            Required = s.Required ?? false,
                            FieldType = s.FieldType,
                            FieldTypeName = s.FieldType.ToString(),
                            SelectionRow = s.SelectionRow.Select(ss => new SelectionRow
                            {
                                Text = ss.Text,
                                Value = ss.Value,
                                Rate = ss.Rate,
                                IsAvailable = ss.IsAvailable ?? true,
                                IsDeleted = false,
                                CreatedTime = DateTimeOffset.UtcNow,
                                LastUpdatedTime = DateTimeOffset.UtcNow,
                                DefaultSelected = ss.DefaultSelected ?? false,
                                MediaUrl = ss.MediaUrl,
                                Order = s.Order
                            }).ToList()
                        }).ToList()
                    };
                    form.Groups.Add(newGroup);
                }
                else
                {
                    //!edit
                    group.Name = item.Name;
                    group.Order = item.Order;
                    group.Description = item.Description;
                    group.IsAvailable = item.IsAvailable;
                    group.IsDeleted = false;
                    group.LastUpdatedTime = DateTimeOffset.UtcNow;

                    group.FormFields = group.FormFields.Select(s => { s.IsDeleted = true; return s; }).ToList();
                    foreach (var fieldItem in item.FormFields)
                    {
                        var field = group.FormFields.FirstOrDefault(f => f.Id == fieldItem.Id);
                        if (field == null)
                        {
                            //!add new
                            var newField = new FormField
                            {
                                Description = fieldItem.Description,
                                Name = fieldItem.Name,
                                Order = fieldItem.Order,
                                DisplayName = fieldItem.DisplayName,
                                IsAvailable = fieldItem.IsAvailable,
                                IsDeleted = false,
                                CreatedTime = DateTimeOffset.UtcNow,
                                LastUpdatedTime = DateTimeOffset.UtcNow,
                                CssClass = fieldItem.CssClass,
                                DefaultValue = fieldItem.DefaultValue,
                                AdditionalData = fieldItem.AdditionalData,
                                MinValueNumber = fieldItem.MinValueNumber,
                                MaxValueNumber = fieldItem.MaxValueNumber,
                                MinValueDateTimeOffset = fieldItem.MinValueDateTimeOffset,
                                MaxValueDateTimeOffset = fieldItem.MaxValueDateTimeOffset,
                                MinLength = fieldItem.MinLength,
                                MaxLength = fieldItem.MaxLength,
                                Hidden = fieldItem.Hidden ?? false,
                                Required = fieldItem.Required ?? false,
                                FieldType = fieldItem.FieldType,
                                FieldTypeName = fieldItem.FieldType.ToString(),
                                SelectionRow = fieldItem.SelectionRow.Select(s => new SelectionRow
                                {
                                    Text = s.Text,
                                    Value = s.Value,
                                    Rate = s.Rate ?? 0,
                                    IsAvailable = s.IsAvailable ?? true,
                                    IsDeleted = false,
                                    CreatedTime = DateTimeOffset.UtcNow,
                                    LastUpdatedTime = DateTimeOffset.UtcNow,
                                    DefaultSelected = s.DefaultSelected ?? false,
                                    MediaUrl = s.MediaUrl,
                                    Order = s.Order
                                }).ToList()
                            };
                            group.FormFields.Add(newField);
                        }
                        else
                        {
                            //!edit
                            field.IsDeleted = false;
                            field.Description = fieldItem.Description ?? "";
                            field.Name = fieldItem.Name;
                            field.Order = fieldItem.Order;
                            field.DisplayName = fieldItem.DisplayName ?? "";
                            field.IsAvailable = fieldItem.IsAvailable;
                            field.CssClass = fieldItem.CssClass ?? "";
                            field.LastUpdatedTime = DateTimeOffset.UtcNow;
                            field.DefaultValue = fieldItem.DefaultValue ?? "";
                            field.AdditionalData = fieldItem.AdditionalData ?? "";
                            field.MinValueNumber = fieldItem.MinValueNumber ?? 0;
                            field.MaxValueNumber = fieldItem.MaxValueNumber ?? 0;
                            field.MinValueDateTimeOffset = fieldItem.MinValueDateTimeOffset;
                            field.MaxValueDateTimeOffset = fieldItem.MaxValueDateTimeOffset;
                            field.MinLength = fieldItem.MinLength ?? 0;
                            field.MaxLength = fieldItem.MaxLength ?? 0;
                            field.Hidden = fieldItem.Hidden ?? false;
                            field.Required = fieldItem.Required ?? false;

                            field.SelectionRow = field.SelectionRow.Select(s => { s.IsDeleted = true; return s; }).ToList();
                            foreach (var selectionItem in fieldItem.SelectionRow)
                            {
                                var selection = field.SelectionRow.FirstOrDefault(f => f.Id == selectionItem.Id);
                                if (selection is null)
                                {
                                    var newSelection = new SelectionRow
                                    {
                                        Text = selectionItem.Text,
                                        Value = selectionItem.Value,
                                        Rate = selectionItem.Rate ?? 0,
                                        IsAvailable = selectionItem.IsAvailable ?? true,
                                        IsDeleted = false,
                                        CreatedTime = DateTimeOffset.UtcNow,
                                        LastUpdatedTime = DateTimeOffset.UtcNow,
                                        DefaultSelected = selectionItem.DefaultSelected ?? false,
                                        MediaUrl = selectionItem.MediaUrl,
                                        Order = selectionItem.Order
                                    };
                                    field.SelectionRow.Add(newSelection);
                                }
                                else
                                {
                                    selection.IsDeleted = false;
                                    selection.Order = selectionItem.Order;
                                    selection.Value = selectionItem.Value;
                                    selection.Text = selectionItem.Text;
                                    selection.MediaUrl = selectionItem.MediaUrl;
                                    selection.LastUpdatedTime = DateTimeOffset.UtcNow;
                                    selection.Rate = selectionItem.Rate ?? 0;
                                    selection.IsAvailable = selectionItem.IsAvailable ?? true;
                                    selection.DefaultSelected = selectionItem.DefaultSelected ?? false;
                                }
                            }
                            field.SelectionRow = field.SelectionRow.Where(w => !w.IsDeleted).ToList();
                        }
                        group.FormFields = group.FormFields.Where(w => !w.IsDeleted).ToList();
                    }
                }
            }
            var filter = Builders<FormsCollection>.Filter.Eq(e => e.Id, editFormDto.Id);
            var result = await collection.ReplaceOneAsync(filter, form);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> ChangeFormAvailability(string formId, bool IsAvailable)
        {
            var formCollection = _dbContext.FormsCollection;
            var result = await formCollection
                .UpdateOneAsync(u => u.Id == formId, Builders<FormsCollection>.Update.Set(s => s.IsAvailable, IsAvailable));
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
        public async Task<bool> DisableForm(string formId)
        {
            //var formCollection = _dbContext.FormsCollection;
            //var result = await formCollection
            //    .UpdateOneAsync(u => u.Id == formId, Builders<FormsCollection>.Update.Set(s => s.Disable, true));
            return await ChangeFormAvailability(formId, false);
        }
        public async Task<bool> ActiveForm(string formId)
        {
            //var formCollection = _dbContext.FormsCollection;
            //var result = await formCollection
            //    .UpdateOneAsync(u => u.Id == formId, Builders<FormsCollection>.Update.Set(s => s.Disable, false));
            return await ChangeFormAvailability(formId, true);

        }
        //============DELETE================
        public async Task<bool> DeleteForm(string formId)
        {
            var formCollection = _dbContext.FormsCollection;
            var result = await formCollection.UpdateOneAsync(d => d.Id == formId, Builders<FormsCollection>.Update.Set(s => s.IsDeleted, true));
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }

}
