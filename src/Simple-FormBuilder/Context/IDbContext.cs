using MongoDB.Driver;
using Simple_Formbuilder.Entities;

namespace Simple_Formbuilder.Context
{
    public interface IDbContext
    {
        IMongoCollection<FormsCollection> FormsCollection { get; }
        IMongoCollection<T> GetCollection<T>(string name);
    }
}