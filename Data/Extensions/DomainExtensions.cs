using Data.Domain;
using System;
using System.Linq;
using Utility.Extensions;

namespace Data.Extensions
{
    public static class DomainExtensions
    {
        public static bool IsDeleted(this IDomain domain) => !domain.DeletedAt.IsDefaultDateTime();

        public static IQueryable<T> NoDeleteds<T>(this IQueryable<T> query) where T : IDomain => query.Where(a => a.DeletedAt.Equals(default(DateTime)));

        public static IQueryable<T> PaginateQuery<T>(this IQueryable<T> query, int page, int limit) => query.Skip(page * limit).Take(limit);
    }
}
