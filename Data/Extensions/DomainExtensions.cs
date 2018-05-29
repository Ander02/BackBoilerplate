using Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Extensions
{
    public static class DomainExtensions
    {
        public static bool IsDeleted(this IDomain domain) => !domain.DeletedAt.Equals(default(DateTime));

        public static IQueryable<T> NoDeleteds<T>(this IQueryable<T> query) where T : IDomain => query.Where(a => a.DeletedAt.Equals(default(DateTime)));

        public static IQueryable<T> PaginateQuery<T>(this IQueryable<T> query, int page, int limit) => query.Skip(page * limit).Take(limit);
    }
}
