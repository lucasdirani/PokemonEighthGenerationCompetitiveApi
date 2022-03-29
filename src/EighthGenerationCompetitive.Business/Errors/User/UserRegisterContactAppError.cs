using EighthGenerationCompetitive.Business.Errors.Codes.User;

namespace EighthGenerationCompetitive.Business.Errors.User
{
    public class UserRegisterContactAppError : AppError
    {
        public override string ErrorTitle { get; protected set; } = "Register contact for user";
        public override string ErrorMessage { get; protected set; } = "An unexpected error occurred while inserting contact data";
        public override string ErrorCode { get; protected set; } = UserErrorCode.UserRegisterContact;
    }
}