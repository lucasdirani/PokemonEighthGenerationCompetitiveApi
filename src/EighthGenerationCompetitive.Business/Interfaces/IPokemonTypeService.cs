using EighthGenerationCompetitive.Business.Entities.TypeAggregate;
using EighthGenerationCompetitive.Business.Monads;
using System;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Business.Interfaces
{
    public interface IPokemonTypeService : IDisposable
    {
        Task<Maybe<Entities.TypeAggregate.Type>> SearchPokemonTypeByNameAsync(string typeName);
        Task<Maybe<Entities.TypeAggregate.Type>> SearchPokemonTypeOnlyWithOurMovesAsync(string typeName);
        Task<Maybe<Entities.TypeAggregate.Type>> SearchPokemonTypeOnlyWithOurMonstersAsync(string typeName);
        Task<Maybe<Entities.TypeAggregate.Type>> SearchPokemonTypeOnlyWithOurMonstersFormsAsync(string typeName);
    }
}