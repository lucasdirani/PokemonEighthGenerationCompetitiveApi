using EighthGenerationCompetitive.Business.Entities;
using EighthGenerationCompetitive.Business.Errors;
using EighthGenerationCompetitive.Business.Interfaces;
using EighthGenerationCompetitive.Business.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace EighthGenerationCompetitive.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorCode, error.ErrorMessage, error.PropertyName);
            }
        }

        protected void Notify(string code, string message, string title)
        {
            _notifier.Handle(new Notification(message, code, title));
        }

        protected void Notify(AppError appError)
        {
            _notifier.Handle(new Notification(appError.ErrorMessage, appError.ErrorCode, appError.ErrorTitle));
        }

        protected bool ExecuteValidation<TV, TE>(TV validator, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validation = validator.Validate(entity);

            if (validation.IsValid) return true;

            Notify(validation);

            return false;
        }
    }
}