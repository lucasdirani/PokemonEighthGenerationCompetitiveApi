using EighthGenerationCompetitive.Business.Errors.Codes.General;

namespace EighthGenerationCompetitive.Business.Errors.General
{
    public class InternalServerAppError : AppError
    {
        public override string ErrorTitle { get; protected set; } = "Unexpected error in request processing";
        public override string ErrorMessage { get; protected set; } = "Go to the bug report page available in the official API documentation to describe the issue";
        public override string ErrorCode { get; protected set; } = GeneralErrorCode.InternalServer;
    }
}