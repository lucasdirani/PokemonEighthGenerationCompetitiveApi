using System;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Data.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoContext _mongoContext;

        private bool _disposed = false;

        public UnitOfWork(IMongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task<bool> CommitAsync() => await _mongoContext.SaveChangesAsync() > 0;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _mongoContext.Dispose();
                _disposed = true;
            }
        }
    }
}