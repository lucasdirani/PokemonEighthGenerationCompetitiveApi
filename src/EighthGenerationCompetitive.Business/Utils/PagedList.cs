using System;
using System.Collections;
using System.Collections.Generic;

namespace EighthGenerationCompetitive.Business.Utils
{
    public abstract class PagedList
    {
        public int CurrentPage { get; protected set; }
        public int TotalPages { get; protected set; }
        public int PageSize { get; protected set; }
        public long TotalCount { get; protected set; }

        public bool HasItems => TotalCount > 0;
        public bool IsEmpty => !HasItems;

        public abstract IList GetListItems();

        public static PagedList<TEntity> Create<TEntity>(
            List<TEntity> items,
            long count,
            int pageNumber,
            int pageSize)
        {
            return new PagedList<TEntity>(items, count, pageNumber, pageSize);
        }
    }

    public class PagedList<TEntity> : PagedList, IList<TEntity>
    {
        public List<TEntity> Items { get; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public int Count => Items.Count;

        public bool IsReadOnly => false;

        public TEntity this[int index] { get => Items[index]; set => Items[index] = value; }

        public PagedList(List<TEntity> items, long count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }

        public int IndexOf(TEntity item) => Items.IndexOf(item);

        public void Insert(int index, TEntity item) => Items.Insert(index, item);

        public void RemoveAt(int index) => Items.RemoveAt(index);

        public void Add(TEntity item) => Items.Add(item);

        public void Clear() => Items.Clear();

        public bool Contains(TEntity item) => Items.Contains(item);

        public void CopyTo(TEntity[] array, int arrayIndex) => Items.CopyTo(array, arrayIndex);

        public bool Remove(TEntity item) => Items.Remove(item);

        public IEnumerator<TEntity> GetEnumerator() => Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();

        public override IList GetListItems() => Items;
    }
}