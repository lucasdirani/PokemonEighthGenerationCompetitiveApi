namespace EighthGenerationCompetitive.Business.Errors
{
    public abstract class AppError
    {
        public abstract string ErrorTitle { get; protected set; }
        public abstract string ErrorMessage { get; protected set; }
        public abstract string ErrorCode { get; protected set; }
    }
}