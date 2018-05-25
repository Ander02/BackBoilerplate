using Business.Exceptions;
using Business.Features.Results;
using Business.Util.Extensions;
using Data.Database;
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

                if (!query.ShowDeleteds) dbQuery = dbQuery.Where(u => u.DeletedAt.IsDefaultDateTime()).AsQueryable();

                if (query.Name != null) dbQuery = dbQuery.Where(u => u.Name.Contains(query.Name.RemoveAccentuation()));

                if (query.Email != null) dbQuery = dbQuery.Where(u => u.Email.Contains(query.Email.RemoveAccentuation()));

                if (query.Age.HasValue) dbQuery = dbQuery.Where(u => u.Age == query.Age.Value);

                if (query.TaskId.HasValue) dbQuery = dbQuery.Where(u => u.Tasks.Select(t => t.Id).Contains(query.TaskId.Value));

                dbQuery = dbQuery.OrderBy(u => u.Name);
                dbQuery = dbQuery.Skip(query.Page * query.Limit).Take(query.Limit);

                return (await dbQuery.ToListAsync()).Select(u => new UserResult.Full(u)).ToList();
            }
        }
    }
}
