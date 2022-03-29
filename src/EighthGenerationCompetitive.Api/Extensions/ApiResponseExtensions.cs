using EighthGenerationCompetitive.Api.Response;

namespace EighthGenerationCompetitive.Api.Extensions
{
    public static class ApiResponseExtensions
    {
        public static ApiResponse<T> AsApiResponse<T>(this T result)
            where T : class =>
            new ApiResponse<T>(result);
    }
}