using System.ComponentModel;

namespace EighthGenerationCompetitive.Data.Sorting.Interfaces
{
    internal interface ISort
    {
        string PropertyName { get; }
        ListSortDirection SortDirection { get; }
    }
}