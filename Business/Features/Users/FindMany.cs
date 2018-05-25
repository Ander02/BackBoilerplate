using Business.Exceptions;
using Business.Features.Results;
using Business.Util.Extensions;
using Data.Database;
using Data.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Features.Users
{
    public class FindMany
    {
        public class Query : BaseFindManyQuery, IRequest<List<UserResult.Full>>
        {
            public string Name { get; set; }
            public Guid? TaskId { get; set; }
            public string Email { get; set; }
            public int? Age { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                //Query Validations
            }
        }

        public class Handler : AsyncRequestHandler<Query, List<UserResult.Full>>
        {
            private readonly Db _db;

            public Handler(Db db)
            {
                _db = db;
            }

            protected override async Task<List<UserResult.Full>> HandleCore(Query query)
            {
                var dbQuery = _db.Users.Include(u => u.Tasks).AsQueryable();

                if (!query.ShowDeleteds) dbQuery = dbQuery.ExcludeDeleteds();

                if (query.Name != null) dbQuery = dbQuery.WhereNameContains(query.Name);

                if (query.Email != null) dbQuery = dbQuery.WhereEmailEquals(query.Email);

                if (query.Age.HasValue) dbQuery = dbQuery.WhereAgeEquals(query.Age.Value);

                if (query.TaskId.HasValue) dbQuery = dbQuery.WhereContainsTask(query.TaskId.Value);

                dbQuery = dbQuery.OrderBy(u => u.Name);
                dbQuery = dbQuery.PaginateQuery(query.Page, query.Limit);

                return (await dbQuery.ToListAsync()).Select(u => new UserResult.Full(u)).ToList();
            }
        }
    }
}
