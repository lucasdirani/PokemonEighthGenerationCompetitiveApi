namespace EighthGenerationCompetitive.Business.Notifications
{
    public class Notification
    {
        public Notification(string message, string code, string title)
        {
            Message = message;
            Code = code;
            Title = title;
        }

        public string Message { get; }
        public string Code { get; }
        public string Title { get; }
    }
}