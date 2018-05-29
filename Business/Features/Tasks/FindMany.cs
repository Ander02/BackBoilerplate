using AutoMapper;
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

namespace Business.Features.Tasks
{
    public class FindMany
    {
        public class Query : BaseFindManyQuery, IRequest<List<TaskResult.Full>>
        {
            public string Title { get; set; }
            public Guid? UserId { get; set; }
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
            private readonly IMapper _mapper;
            private readonly Db _db;

            public Handler(IMapper mapper, Db db)
            {
                _mapper = mapper;
                _db = db;
            }


            protected override async Task<List<TaskResult.Full>> HandleCore(Query query)
            {
                var dbQuery = _db.Tasks.Include(u => u.User).AsQueryable();

                if (!query.ShowDeleteds) dbQuery = dbQuery.NoDeleteds();

                if (query.Title != null) dbQuery = dbQuery.Where(t => t.Title.Contains(query.Title));

                if (query.UserId.HasValue) dbQuery = dbQuery.Where(t => t.User.Id.Equals(query.UserId));

                dbQuery = dbQuery.OrderBy(u => u.Title);
                dbQuery = dbQuery.PaginateQuery(query.Page, query.Limit);

                return (await dbQuery.ToListAsync()).Select(task => _mapper.Map<TaskResult.Full>(task)).ToList();
            }
        }
    }
}
