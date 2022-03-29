using AutoMapper;
using EighthGenerationCompetitive.Api.V1.ViewModels.Types;
using EighthGenerationCompetitive.Business.Entities.TypeAggregate;
using EighthGenerationCompetitive.Business.Projections.TypeAggregate;

namespace EighthGenerationCompetitive.Api.Profiles.V1
{
    public class PokemonTypeProfile : Profile
    {
        public PokemonTypeProfile()
        {
            CreateMapForViewModelsOfTypesController();
        }

        private void CreateMapForViewModelsOfTypesController()
        {
            CreateMap<PokemonType, PokemonTypeCreatureTypeViewModel>();
            CreateMap<TypeRelations, PokemonTypeRelationsViewModel>();
            CreateMap<PokemonMove, PokemonTypeMoveViewModel>();
            CreateMap<PokemonAbility, PokemonTypeCreatureAbilityViewModel>();
            CreateMap<PokemonStat, PokemonTypeCreatureStatViewModel>();
            CreateMap<PokemonBaseStat, PokemonTypeCreatureBaseStatViewModel>();
            CreateMap<PokemonTier, PokemonTypeCreatureTierViewModel>();
            CreateMap<Pokemon, PokemonTypeCreatureViewModel>();
            CreateMap<PokemonForm, PokemonTypeCreatureFormViewModel>();
            CreateMap<Type, GetPokemonTypeViewModel>();
            CreateMap<Type, GetPokemonTypeMovesByNameViewModel>();
            CreateMap<Type, GetPokemonTypeMonstersByNameViewModel>();
            CreateMap<Type, GetPokemonTypeMonstersFormsByNameViewModel>();
            CreateMap<TypeNameAndRelationsProjection, GetPokemonTypesViewModel>();
        }
    }
}