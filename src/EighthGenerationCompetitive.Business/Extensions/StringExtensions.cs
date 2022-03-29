using System;
using System.Linq;

namespace EighthGenerationCompetitive.Business.Extensions
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentNullException(nameof(input), $"{nameof(input)} cannot be empty"),
                _ => input.First().ToString().ToUpper() + input[1..]
            };

        public static string AllLettersAfterFirstCharToLower(this string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentNullException(nameof(input), $"{nameof(input)} cannot be empty"),
                _ => input.First().ToString() + input[1..].ToLower()
            };

        public static bool IsNumeric(this string input) =>
            long.TryParse(input, out _);
    }
}