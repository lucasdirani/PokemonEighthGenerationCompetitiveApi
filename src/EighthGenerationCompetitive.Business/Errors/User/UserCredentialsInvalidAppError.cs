using EighthGenerationCompetitive.Business.Errors.Codes.User;

namespace EighthGenerationCompetitive.Business.Errors.User
{
    public class UserCredentialsInvalidAppError : AppError
    {
        public override string ErrorTitle { get; protected set; } = "User credentials invalid";
        public override string ErrorMessage { get; protected set; } = "Incorrect username and/or passwords";
        public override string ErrorCode { get; protected set; } = UserErrorCode.UserCredentialsInvalid;
    }
}