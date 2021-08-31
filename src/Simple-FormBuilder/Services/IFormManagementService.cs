using Simple_Formbuilder.Dtos;
using Simple_Formbuilder.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simple_Formbuilder.Services
{
    public interface IFormManagementService
    {
        Task<bool> ActiveForm(string formId);
        Task<bool> ChangeFormAvailability(string formId, bool disable);
        Task<string> CreateForm(CreateFormDto createFormDto);
        Task<bool> DeleteForm(string formId);
        Task<bool> DisableForm(string formId);
        Task<bool> EditForm(EditFormDto editFormDto);
        Task<List<FormsCollection>> GetAllForms(int pageNumber = 1, int pageSize = 10);
        Task<FormsCollection> GetForm(string formId);
        Task<FormsCollection> GetForm(string formId, string tenantId);
        Task<List<FormsCollection>> GetFormsByIdList(List<string> idList, int pageNumber = 1, int pageSize = 10);
        Task<List<FormsCollection>> GetAllFormsOftenant(string tenantId, int pageNumber = 1, int pageSize = 10);
        Task<List<FormsCollection>> GetAvailableFormsOftenant(string tenantId, int pageNumber = 1, int pageSize = 10);
        Task<List<FormsCollection>> GetUserForms(string userId, int pageNumber = 1, int pageSize = 10);
    }
}