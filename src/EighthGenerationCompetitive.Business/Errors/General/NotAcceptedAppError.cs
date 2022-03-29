using EighthGenerationCompetitive.Business.Errors.Codes.General;

namespace EighthGenerationCompetitive.Business.Errors.General
{
    public class NotAcceptedAppError : AppError
    {
        public override string ErrorTitle { get; protected set; } = "Unsupported content-type";
        public override string ErrorMessage { get; protected set; } = "The accept header must contains application/json";
        public override string ErrorCode { get; protected set; } = GeneralErrorCode.NotAccepted;
    }
}