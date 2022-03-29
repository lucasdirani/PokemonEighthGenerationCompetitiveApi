using EighthGenerationCompetitive.Business.Errors.Codes.User;

namespace EighthGenerationCompetitive.Business.Errors.User
{
    public class UserLockedOutAppError : AppError
    {
        public override string ErrorTitle { get; protected set; } = "User locked out";
        public override string ErrorMessage { get; protected set; } = "User blocked by invalid attempts";
        public override string ErrorCode { get; protected set; } = UserErrorCode.UserLockedOut;
    }
}