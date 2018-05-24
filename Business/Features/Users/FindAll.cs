using Business.Exceptions;
using Business.Features.Results;
using Business.Util.Extensions;
using Data.Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Features.Users
{
    public class FindAll
    {
        public class Query : BaseFindManyQuery, IRequest<List<UserResult.Full>>
        {

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
                if (query == null) throw new BadRequestException("The argument is null");

                var dbQuery = _db.Users.Include(u => u.Tasks).AsQueryable();

                if (!query.ShowDeleteds) dbQuery = dbQuery.Where(u => u.DeletedAt.IsDefaultDateTime()).AsQueryable();

                dbQuery = dbQuery.OrderBy(u => u.Name);
                dbQuery = dbQuery.Skip(query.Page * query.Limit).Take(query.Limit);

                return (await dbQuery.ToListAsync()).Select(u => new UserResult.Full(u)).ToList();
            }
        }
    }
}
