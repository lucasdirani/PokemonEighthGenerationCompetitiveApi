using EighthGenerationCompetitive.Business.Errors.Codes.User;

namespace EighthGenerationCompetitive.Business.Errors.User
{
    public class UserUnsharedProfileAppError : AppError
    {
        public override string ErrorTitle { get; protected set; } = "User unshared profile";
        public override string ErrorMessage { get; protected set; } = "User does not exist or has not shared his profile";
        public override string ErrorCode { get; protected set; } = UserErrorCode.UserUnsharedProfile;
    }
}