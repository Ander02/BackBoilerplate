using Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Extensions
{
    public static class UserExtensions
    {

        public static IQueryable<User> WhereNameContains(this IQueryable<User> query, string name) => query.Where(u => u.Name.Contains(name));

        public static IQueryable<User> WhereEmailEquals(this IQueryable<User> query, string email) => query.Where(u => u.Email.Equals(email));

        public static IQueryable<User> WhereAgeEquals(this IQueryable<User> query, int age) => query.Where(u => u.Age == age);

        public static IQueryable<User> WhereContainsTask(this IQueryable<User> query, Task task) => query.Where(u => u.Tasks.Contains(task));

        public static IQueryable<User> WhereContainsTask(this IQueryable<User> query, Guid taskId) => query.Where(u => u.Tasks.Select(t => t.Id).Contains(taskId));

    }
}
