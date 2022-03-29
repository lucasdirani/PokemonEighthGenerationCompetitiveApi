namespace EighthGenerationCompetitive.Api.Parameters.TypesController
{
    public interface IFilterPokemonTypesParameters
    {
        string NoDamageTo { get; set; }
        string HalfDamageTo { get; set; }
        string DoubleDamageTo { get; set; }
        string NoDamageFrom { get; set; }
        string HalfDamageFrom { get; set; }
        string DoubleDamageFrom { get; set; }
    }
}