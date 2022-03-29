namespace EighthGenerationCompetitive.Api.Response
{
    public class ApiResponse<T> where T : class
    {
        public ApiResponse(T data)
        {
            Data = data;
        }

        public T Data { get; private set; }
        public bool Success => Data is not null;
    }
}