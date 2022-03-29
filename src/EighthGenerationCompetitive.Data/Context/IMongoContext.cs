using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Data.Context
{
    public interface IMongoContext : IDisposable
    {
        IMongoCollection<T> GetCollection<T>(string name);
        Task<int> SaveChangesAsync();
        void AddCommand(Func<Task> command);
    }
}