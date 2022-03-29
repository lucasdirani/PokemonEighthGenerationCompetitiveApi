using EighthGenerationCompetitive.Business.Errors.General;
using EighthGenerationCompetitive.Business.Errors.Nature;
using EighthGenerationCompetitive.Business.Errors.Type;
using EighthGenerationCompetitive.Business.Errors.User;
using System.Collections.Generic;

namespace EighthGenerationCompetitive.Business.Errors.Factory
{
    public static class AppErrorFactory
    {
        private static readonly IDictionary<AppErrorType, AppError> _applicationErrors = SetApplicationErrors();

        public static AppError Make(AppErrorType applicationError)
        {
            return _applicationErrors[applicationError];
        }

        private static IDictionary<AppErrorType, AppError> SetApplicationErrors() =>
            new Dictionary<AppErrorType, AppError>
            {
                { AppErrorType.PokemonTypeNotFound, new PokemonTypeNotFoundAppError() },
                { AppErrorType.InternalServer, new InternalServerAppError() },
                { AppErrorType.InvalidParameters, new InvalidParametersAppError() },
                { AppErrorType.NotAccepted, new NotAcceptedAppError() },
                { AppErrorType.NatureNotFound, new NatureNotFoundAppError() },
                { AppErrorType.UserNotFound, new UserNotFoundAppError() },
                { AppErrorType.UserUnsharedProfile, new UserUnsharedProfileAppError() },
                { AppErrorType.UserLockedOut, new UserLockedOutAppError() },
                { AppErrorType.UserCredentialsInvalid, new UserCredentialsInvalidAppError() },
                { AppErrorType.UserNonExistingContact, new UserNonExistingContactAppError() },
                { AppErrorType.UserContactsNotFound, new UserContactsNotFoundAppError() },
                { AppErrorType.UserExistingContact, new UserExistingContactAppError() },
                { AppErrorType.UserRegisterContact, new UserRegisterContactAppError() },
                { AppErrorType.UserContactNotFound, new UserContactNotFoundAppError() },
            };
    }
}