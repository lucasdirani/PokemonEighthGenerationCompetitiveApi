using AutoMapper;
using EighthGenerationCompetitive.Api.V1.ViewModels.Natures;
using EighthGenerationCompetitive.Business.Entities.NatureAggregate;
using EighthGenerationCompetitive.Business.Projections.NatureAggregate;

namespace EighthGenerationCompetitive.Api.Profiles.V1
{
    public class NatureProfile : Profile
    {
        public NatureProfile()
        {
            CreateMapForViewModelsOfNaturesController();
        }

        private void CreateMapForViewModelsOfNaturesController()
        {
            CreateMap<DecreasedNature, DecreasedNatureViewModel>();
            CreateMap<IncreasedNature, IncreasedNatureViewModel>();
            CreateMap<PokemonAbility, NaturePokemonAbilityViewModel>();
            CreateMap<PokemonStat, NaturePokemonStatViewModel>();
            CreateMap<PokemonTier, NaturePokemonTierViewModel>();
            CreateMap<PokemonType, NaturePokemonTypeViewModel>();
            CreateMap<NatureStat, NatureStatViewModel>();
            CreateMap<PokemonBaseStat, NaturePokemonBaseStatViewModel>();
            CreateMap<PokemonForm, NaturePokemonFormViewModel>();
            CreateMap<Pokemon, NaturePokemonViewModel>();
            CreateMap<Nature, GetNatureViewModel>();
            CreateMap<Nature, GetNatureMonstersByNameViewModel>();
            CreateMap<Nature, GetNatureMonstersFormsByNameViewModel>();
            CreateMap<NatureNameAndStatsProjection, GetNaturesViewModel>();
        }
    }
}