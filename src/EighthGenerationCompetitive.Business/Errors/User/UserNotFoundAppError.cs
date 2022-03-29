using EighthGenerationCompetitive.Business.Errors.Codes.User;

namespace EighthGenerationCompetitive.Business.Errors.User
{
    public class UserNotFoundAppError : AppError
    {
        public override string ErrorTitle { get; protected set; } = "User not found";
        public override string ErrorMessage { get; protected set; } = "Username is not registered";
        public override string ErrorCode { get; protected set; } = UserErrorCode.UserNotFound;
    }
}