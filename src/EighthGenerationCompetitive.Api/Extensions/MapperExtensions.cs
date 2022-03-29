using AutoMapper;
using EighthGenerationCompetitive.Business.Utils;
using System.Collections.Generic;

namespace EighthGenerationCompetitive.Api.Extensions
{
    public static class MapperExtensions
    {
        public static PagedList<TConversion> MapPagedList<TConversion>(this IMapper mapper, PagedList pagedList)
        {
            var convertedPagedList = mapper.Map<List<TConversion>>(pagedList.GetListItems());

            return PagedList.Create(convertedPagedList, pagedList.TotalCount, pagedList.CurrentPage, pagedList.PageSize);
        }
    }
}