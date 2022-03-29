using EighthGenerationCompetitive.Business.Errors.Codes.Type;

namespace EighthGenerationCompetitive.Business.Errors.Type
{
    public class PokemonTypeNotFoundAppError : AppError
    {
        private static readonly string[] _pokemonTypes = new string[] 
        { 
            "Normal", "Fire", "Water", "Grass", "Electric", "Ice",
            "Fighting", "Poison", "Ground", "Flying", "Psychic",
            "Bug", "Rock", "Ghost", "Dark", "Dragon", "Steel", "Fairy"
        };

        public override string ErrorTitle { get; protected set; } = "Invalid pokemon type name";
        public override string ErrorMessage { get; protected set; } = string.Format("The searched pokemon type must be present in one of the following values: {0}", string.Join(',', _pokemonTypes));
        public override string ErrorCode { get; protected set; } = TypeErrorCode.PokemonTypeNotFound;
    }
}