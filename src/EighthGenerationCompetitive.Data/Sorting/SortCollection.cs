using EighthGenerationCompetitive.Data.Sorting.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EighthGenerationCompetitive.Data.Sorting
{
    internal class SortCollection<TSource>
    {
        private List<ISortProperty<TSource>> Properties { get; set; } 
            = new List<ISortProperty<TSource>>();

        public IReadOnlyList<ISort> Sorts => Properties.AsReadOnly();

        public SortCollection() {}

        public SortCollection(string value)
            : this(value?.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
        {
        }

        public SortCollection(IEnumerable<string> elements)
        {
            if (elements == null) return;

            foreach (var element in elements)
            {
                var sortElement = Parse(element);

                if (sortElement is null) continue;

                Properties.Add(sortElement);
            }
        }

        public IMongoQueryable<TSource> Apply(IMongoQueryable<TSource> queryable)
        {
            var query = queryable;

            foreach (var element in Properties)
            {
                query = element.Apply(query);
            }

            return query;
        }

        public override string ToString()
        {
            return string.Join(",", Properties);
        }

        public string AddOrUpdate(string element)
        {
            var parse = Parse(element);

            if (parse == null) return ToString();

            var sort = new SortCollection<TSource>(ToString());

            var property = sort.Properties.Find(x => x.PropertyName == parse.PropertyName);

            if (property == null)
            {
                sort.Properties.Add(parse);
                return sort.ToString();
            }

            property.SortDirection =
                property.SortDirection == ListSortDirection.Ascending
                    ? ListSortDirection.Descending
                    : ListSortDirection.Ascending;

            return sort.ToString();
        }

        public string Remove(string element)
        {
            var parse = Parse(element);

            if (parse is null) return ToString();

            var sort = new SortCollection<TSource>(ToString());

            var property = sort.Properties.Find(x => x.PropertyName == parse.PropertyName);

            if (property != null)
            {
                sort.Properties.Remove(property);
            }

            return sort.ToString();
        }

        private static ISortProperty<TSource> Parse(string element)
        {
            var name = element.Trim();
            var properties = typeof(TSource).GetProperties().ToList();
            var direction = ListSortDirection.Ascending;

            if (element.StartsWith("-"))
            {
                direction = ListSortDirection.Descending;
                name = name[1..];
            }

            var propertyInfo = properties.Find(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (propertyInfo is null) return null;

            var type = typeof(SortProperty<>);
            var sortedElementType = type.MakeGenericType(typeof(TSource), propertyInfo.PropertyType);
            var ctor = sortedElementType.GetConstructor(new[] { typeof(PropertyInfo), typeof(ListSortDirection) });

            return ctor.Invoke(new object[] { propertyInfo, direction }) as ISortProperty<TSource>;
        }

        private class SortProperty<TKey> : ISortProperty<TSource>
        {
            public SortProperty(PropertyInfo propertyInfo, ListSortDirection direction)
            {
                PropertyName = propertyInfo.Name;
                SortDirection = direction;

                var source = Expression.Parameter(typeof(TSource), "x");
                var member = Expression.Property(source, propertyInfo);

                Filter = Expression.Lambda<Func<TSource, TKey>>(member, source);
            }

            public ListSortDirection SortDirection { get; set; }

            public Expression<Func<TSource, TKey>> Filter { get; private set; }

            public string PropertyName { get; set; }

            public IMongoQueryable<TSource> Apply(IMongoQueryable<TSource> mongoQueryable)
            {
                var visitor = new OrderingMethodFinder();

                visitor.Visit(mongoQueryable.Expression);

                if (visitor.OrderingMethodFound)
                {
                    mongoQueryable = SortDirection == ListSortDirection.Ascending
                        ? ((IOrderedMongoQueryable<TSource>) mongoQueryable).ThenBy(Filter)
                        : ((IOrderedMongoQueryable<TSource>) mongoQueryable).ThenByDescending(Filter);
                }
                else
                {
                    mongoQueryable = SortDirection == ListSortDirection.Ascending
                        ? mongoQueryable.OrderBy(Filter)
                        : mongoQueryable.OrderByDescending(Filter);
                }

                return mongoQueryable;
            }

            public override string ToString()
            {
                return SortDirection == ListSortDirection.Ascending
                    ? PropertyName
                    : $"-{PropertyName}";
            }

            private class OrderingMethodFinder : ExpressionVisitor
            {
                public bool OrderingMethodFound { get; set; }

                protected override Expression VisitMethodCall(MethodCallExpression node)
                {
                    var name = node.Method.Name;

                    if (name.StartsWith("OrderBy", StringComparison.Ordinal) ||
                        name.StartsWith("ThenBy", StringComparison.Ordinal))
                    {
                        OrderingMethodFound = true;
                    }

                    return base.VisitMethodCall(node);
                }
            }
        }
    }
}