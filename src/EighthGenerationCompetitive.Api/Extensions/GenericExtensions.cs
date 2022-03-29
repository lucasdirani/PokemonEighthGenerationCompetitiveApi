using RiskFirst.Hateoas;
using RiskFirst.Hateoas.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Api.Extensions
{
    public static class GenericExtensions
    {
        public static string ToJson<T>(this T entity) => JsonSerializer.Serialize(entity);

        public static async Task<IEnumerable<TEntity>> AddHateoasWithAsync<TEntity>(
            this IEnumerable<TEntity> enumerable,
            ILinksService linksService)
            where TEntity : LinkContainer
        {
            foreach (TEntity entity in enumerable)
            {
                await linksService.AddLinksAsync(entity);
            }

            return enumerable;
        }
    }
}