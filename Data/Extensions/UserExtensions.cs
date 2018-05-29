using Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Extensions
{
    public static class UserExtensions
    {
        public static IQueryable<User> WhereContainsTask(this IQueryable<User> query, Task task) => query.Where(u => u.Tasks.Contains(task));

        public static IQueryable<User> WhereContainsTask(this IQueryable<User> query, Guid taskId) => query.Where(u => u.Tasks.Select(t => t.Id).Contains(taskId));
    }
}
