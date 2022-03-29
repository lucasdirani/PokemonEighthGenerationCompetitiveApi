using EighthGenerationCompetitive.Business.Utils;
using RiskFirst.Hateoas;
using RiskFirst.Hateoas.Models;

namespace EighthGenerationCompetitive.Api.Extensions
{
    public static class PagedListExtensions
    {
        public static PagedList<TEntity> AddHateoasWith<TEntity>(
            this PagedList<TEntity> pagedList, 
            ILinksService linksService)
            where TEntity : LinkContainer
        {
            pagedList.Items.ForEach(async x => await linksService.AddLinksAsync(x));

            return pagedList;
        }
    }
}