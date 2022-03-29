using System;
using System.Collections.Generic;

namespace EighthGenerationCompetitive.Business.Extensions
{
    public static class GenericExtensions
    {
        public static IEnumerable<T> GetUniqueItems<T>(this IEnumerable<T> items)
        {
            var uniqueItems = new HashSet<T>(items);

            return uniqueItems;
        }

        public static int GetIdentification<T>(this T enumMember) where T : Enum
        {
            return Convert.ToInt32(enumMember);
        }
    }
}