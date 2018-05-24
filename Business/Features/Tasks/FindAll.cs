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

namespace Business.Features.Tasks
{
    public class FindAll
    {
        public class Query : BaseFindManyQuery, IRequest<List<TaskResult.Full>>
        {

        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                //Query Validations
            }
        }

        public class Handler : AsyncRequestHandler<Query, List<TaskResult.Full>>
        {
            private readonly Db _db;

            public Handler(Db db)
            {
                _db = db;
            }

            protected override async Task<List<TaskResult.Full>> HandleCore(Query query)
            {
                if (query == null) throw new BadRequestException("The argument is null");

                var dbQuery = _db.Tasks.Include(u => u.User).AsQueryable();

                if (!query.ShowDeleteds) dbQuery = dbQuery.Where(u => u.DeletedAt.IsDefaultDateTime()).AsQueryable();

                dbQuery = dbQuery.OrderBy(u => u.Name);
                dbQuery = dbQuery.Skip(query.Page * query.Limit).Take(query.Limit);

                return (await dbQuery.ToListAsync()).Select(t => new TaskResult.Full(t)).ToList();
            }
        }
    }
}
