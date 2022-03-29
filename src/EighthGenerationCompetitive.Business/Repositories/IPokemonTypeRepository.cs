using EighthGenerationCompetitive.Business.Entities.TypeAggregate;
using EighthGenerationCompetitive.Business.Parameters.TypeAggregate;
using EighthGenerationCompetitive.Business.Utils;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Business.Repositories
{
    public interface IPokemonTypeRepository : IRepository<Type>
    {
        Task<PagedList<TProjection>> FindPokemonTypesAsync<TProjection>(FindPokemonTypesParameters<TProjection> queryParameters);
        Task<Type> FindPokemonTypeByNameAsync(string typeName);
        Task<Type> FindPokemonTypeByNameOnlyWithOurMovesAsync(string typeName);
        Task<Type> FindPokemonTypeByNameOnlyWithOurMonstersAsync(string typeName);
        Task<Type> FindPokemonTypeByNameOnlyWithOurMonstersFormsAsync(string typeName);
    }
}