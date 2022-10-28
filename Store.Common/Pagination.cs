using System.Data.SqlTypes;

namespace Store.Common
{
    public static class Pagination
    {
        public static IEnumerable<TSource> ToPaged<TSource>(this IEnumerable<TSource> Source, int page, int PageSize, out int RowsCount)
        {
            RowsCount = Source.Count();
            return Source.Skip((page - 1) * PageSize).Take(PageSize);
        }
    }
}