using EighthGenerationCompetitive.Business.Entities.NatureAggregate;
using EighthGenerationCompetitive.Business.Monads;
using System;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Business.Interfaces
{
    public interface INatureService : IDisposable
    {
        Task<Maybe<Nature>> SearchNatureByNameAsync(string natureName);
        Task<Maybe<Nature>> SearchNatureOnlyWithOurMonstersAsync(string natureName);
        Task<Maybe<Nature>> SearchNatureOnlyWithOurMonstersFormsAsync(string natureName);
    }
}