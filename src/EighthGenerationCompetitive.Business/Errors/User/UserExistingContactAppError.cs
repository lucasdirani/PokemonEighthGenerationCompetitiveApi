using EighthGenerationCompetitive.Business.Errors.Codes.User;

namespace EighthGenerationCompetitive.Business.Errors.User
{
    public class UserExistingContactAppError : AppError
    {
        public override string ErrorTitle { get; protected set; } = "User Already Has The Contact";
        public override string ErrorMessage { get; protected set; } = "The user has already created this type of contact, please consider updating it";
        public override string ErrorCode { get; protected set; } = UserErrorCode.UserExistingContact;
    }
}