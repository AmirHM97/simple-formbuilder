using Simple_Formbuilder.Dtos;
using Simple_Formbuilder.Entities;
using System.Threading.Tasks;

namespace Simple_Formbuilder.Services
{
    public interface IRecordsService
    {
        Task<string> AddRecord(AddRecordDto addRecordDto);
        Task<RecordsCollection> GetRecordById(string recordId, string serviceId);
        Task<RecordsCollection> GetRecordByServiceId(string serviceId, string objectId, string formId);
    }
}