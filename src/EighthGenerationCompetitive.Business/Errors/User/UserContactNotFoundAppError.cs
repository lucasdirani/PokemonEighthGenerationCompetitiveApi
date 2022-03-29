using EighthGenerationCompetitive.Business.Errors.Codes.User;

namespace EighthGenerationCompetitive.Business.Errors.User
{
    public class UserContactNotFoundAppError : AppError
    {
        public override string ErrorTitle { get; protected set; } = "User contact not found";
        public override string ErrorMessage { get; protected set; } = "User contact is not registered";
        public override string ErrorCode { get; protected set; } = UserErrorCode.UserContactNotFound;
    }
}