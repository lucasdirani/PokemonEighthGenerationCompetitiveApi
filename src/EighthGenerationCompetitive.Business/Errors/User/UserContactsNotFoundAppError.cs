using EighthGenerationCompetitive.Business.Errors.Codes.User;

namespace EighthGenerationCompetitive.Business.Errors.User
{
    public class UserContactsNotFoundAppError : AppError
    {
        public override string ErrorTitle { get; protected set; } = "User contacts not found";
        public override string ErrorMessage { get; protected set; } = "User contacts are not registered";
        public override string ErrorCode { get; protected set; } = UserErrorCode.UserContactsNotFound;
    }
}