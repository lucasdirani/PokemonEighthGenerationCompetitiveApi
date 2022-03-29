using MongoDB.Driver.Linq;
using System.ComponentModel;

namespace EighthGenerationCompetitive.Data.Sorting.Interfaces
{
    internal interface ISortProperty<TEntity> : ISort
    {
        new ListSortDirection SortDirection { get; set; }
        IMongoQueryable<TEntity> Apply(IMongoQueryable<TEntity> mongoQueryable);
    }
}