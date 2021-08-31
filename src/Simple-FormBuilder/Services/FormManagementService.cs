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
            var form = await formCollection.Find(f => f.Id == formId && f.tenantId == tenantId).FirstOrDefaultAsync();
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
            var forms = await formCollection.Find(f => f.tenantId == tenantId && f.IsAvailable).Skip(skip).Limit(pageSize).ToListAsync();
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
            var forms = await formCollection.Find(f => f.tenantId == tenantId).Skip(skip).Limit(pageSize).ToListAsync();
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
            foreach (var item in form.Groups)
            {
                var group = editFormDto.Groups.FirstOrDefault(f => f.Id == item.Id);
                if (group == null)
                {
                    item.IsAvailable = false;
                    item.IsAvailable = true;
                }
                else
                {
                    item.Name = group.Name;
                    item.Order = group.Order;
                    item.Description = group.Description;
                    item.IsAvailable = group.IsAvailable;
                    item.LastUpdatedTime = DateTimeOffset.UtcNow;
                    foreach (var fieldItem in item.FormFields)
                    {
                        var field = group.FormFields.FirstOrDefault(f => f.Id == fieldItem.Id);
                        if (field == null)
                        {
                            fieldItem.IsAvailable = false;
                            fieldItem.IsDeleted = true;
                        }
                        else
                        {
                            fieldItem.Description = field.Description;
                            fieldItem.Name = field.Name;
                            fieldItem.Order = field.Order;
                            fieldItem.DisplayName = field.DisplayName;
                            fieldItem.IsAvailable = field.IsAvailable;
                            fieldItem.CssClass = field.CssClass;
                            fieldItem.LastUpdatedTime = DateTimeOffset.UtcNow;
                            fieldItem.DefaultValue = field.DefaultValue;
                            fieldItem.AdditionalData = field.AdditionalData;
                            fieldItem.MinValueNumber = field.MinValueNumber;
                            fieldItem.MaxValueNumber = field.MaxValueNumber;
                            fieldItem.MinValueDateTimeOffset = field.MinValueDateTimeOffset;
                            fieldItem.MaxValueDateTimeOffset = field.MaxValueDateTimeOffset;
                            fieldItem.MinLength = field.MinLength;
                            fieldItem.MaxLength = field.MaxLength;
                            fieldItem.Hidden = field.Hidden;
                            fieldItem.Required = field.Required;

                            foreach (var selectionItem in fieldItem.SelectionRow)
                            {
                                var selectionRow = field.SelectionRow.FirstOrDefault(f => f.Id == selectionItem.Id);
                                if (selectionItem == null)
                                {
                                    selectionItem.IsAvailable = false;
                                    selectionItem.IsDeleted = true;
                                }
                                else
                                {
                                    selectionItem.Order = selectionRow.Order;
                                    selectionItem.Value = selectionRow.Value;
                                    selectionItem.Text = selectionRow.Text;
                                    selectionItem.MediaUrl = selectionRow.MediaUrl;
                                    selectionItem.LastUpdatedTime = DateTimeOffset.UtcNow;
                                    selectionItem.Rate = selectionRow.Rate;
                                    selectionItem.IsAvailable = selectionRow.IsAvailable;
                                    selectionItem.DefaultSelected = selectionRow.DefaultSelected;
                                }
                            }
                        }
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
