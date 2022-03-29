using EighthGenerationCompetitive.Business.Entities.NatureAggregate;
using EighthGenerationCompetitive.Business.Errors;
using EighthGenerationCompetitive.Business.Errors.Factory;
using EighthGenerationCompetitive.Business.Interfaces;
using EighthGenerationCompetitive.Business.Monads;
using EighthGenerationCompetitive.Business.Repositories;
using System;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Business.Services
{
    public class NatureService : BaseService, INatureService
    {
        private readonly INatureRepository _natureRepository;

        private bool _disposed = false;

        public NatureService(INatureRepository natureRepository, INotifier notifier)
            : base(notifier)
        {
            _natureRepository = natureRepository;
        }

        public async Task<Maybe<Nature>> SearchNatureByNameAsync(string natureName)
        {
            Maybe<Nature> nature = await _natureRepository.FindNatureByNameAsync(natureName);

            if (nature.HasNoValue)
            {
                Notify(appError: AppErrorFactory.Make(AppErrorType.NatureNotFound));
            }

            return nature;
        }

        public async Task<Maybe<Nature>> SearchNatureOnlyWithOurMonstersAsync(string natureName)
        {
            Maybe<Nature> nature = await _natureRepository.FindNatureByNameOnlyWithOurMonstersAsync(natureName);

            if (nature.HasNoValue)
            {
                Notify(appError: AppErrorFactory.Make(AppErrorType.NatureNotFound));
            }

            return nature;
        }

        public async Task<Maybe<Nature>> SearchNatureOnlyWithOurMonstersFormsAsync(string natureName)
        {
            Maybe<Nature> nature = await _natureRepository.FindNatureByNameOnlyWithOurMonstersFormsAsync(natureName);

            if (nature.HasNoValue)
            {
                Notify(appError: AppErrorFactory.Make(AppErrorType.NatureNotFound));
            }

            return nature;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _natureRepository?.Dispose();
                _disposed = true;
            }
        }
    }
}