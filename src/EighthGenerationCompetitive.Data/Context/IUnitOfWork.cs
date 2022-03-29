using System;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Data.Context
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();
    }
}