using EighthGenerationCompetitive.Business.Errors.Codes.User;

namespace EighthGenerationCompetitive.Business.Errors.User
{
    public class UserNonExistingContactAppError : AppError
    {
        public override string ErrorTitle { get; protected set; } = "User does not have the contact";
        public override string ErrorMessage { get; protected set; } = "The user does not have the contact sought for update";
        public override string ErrorCode { get; protected set; } = UserErrorCode.UserNonExistingContact;
    }
}