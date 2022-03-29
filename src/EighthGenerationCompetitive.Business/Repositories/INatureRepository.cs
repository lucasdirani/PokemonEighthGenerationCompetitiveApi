using EighthGenerationCompetitive.Business.Entities.NatureAggregate;
using EighthGenerationCompetitive.Business.Parameters.NatureAggregate;
using EighthGenerationCompetitive.Business.Utils;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Business.Repositories
{
    public interface INatureRepository : IRepository<Nature>
    {
        Task<PagedList<TProjection>> FindNaturesAsync<TProjection>(FindNaturesParameters<TProjection> queryParameters);
        Task<Nature> FindNatureByNameAsync(string natureName);
        Task<Nature> FindNatureByNameOnlyWithOurMonstersAsync(string natureName);
        Task<Nature> FindNatureByNameOnlyWithOurMonstersFormsAsync(string natureName);
    }
}