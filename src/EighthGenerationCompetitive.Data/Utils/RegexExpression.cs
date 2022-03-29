namespace EighthGenerationCompetitive.Data.Utils
{
    internal static class RegexExpression
    {
        public static string ApplyIgnoreCaseExpressionTo(string value) =>
            "/^" + value + "$/i";
    }
}