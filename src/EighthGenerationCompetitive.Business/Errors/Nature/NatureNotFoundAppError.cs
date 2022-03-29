using EighthGenerationCompetitive.Business.Errors.Codes.Nature;

namespace EighthGenerationCompetitive.Business.Errors.Nature
{
    public class NatureNotFoundAppError : AppError
    {
        private static readonly string[] _natures = new string[]
        {
            "Adamant", "Bashful", "Bold", "Brave", "Calm", "Careful",
            "Docile", "Gentle", "Hardy", "Hasty", "Impish",
            "Jolly", "Lax", "Lonely", "Mild", "Modest", "Naive", "Naughty",
            "Quiet", "Quirky", "Rash", "Relaxed", "Sassy", "Serious", "Timid"
        };

        public override string ErrorTitle { get; protected set; } = "Invalid nature name";
        public override string ErrorMessage { get; protected set; } = string.Format("The searched nature must be present in one of the following values: {0}", string.Join(',', _natures));
        public override string ErrorCode { get; protected set; } = NatureErrorCode.NatureNotFound;
    }
}