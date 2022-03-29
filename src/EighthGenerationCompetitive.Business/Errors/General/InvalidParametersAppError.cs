using EighthGenerationCompetitive.Business.Errors.Codes.General;

namespace EighthGenerationCompetitive.Business.Errors.General
{
    public class InvalidParametersAppError : AppError
    {
        public override string ErrorTitle { get; protected set; } = "Invalid parameters";
        public override string ErrorMessage { get; protected set; } = "Check the documentation to verify the expected format of the parameters.";
        public override string ErrorCode { get; protected set; } = GeneralErrorCode.InvalidParameters;
    }
}