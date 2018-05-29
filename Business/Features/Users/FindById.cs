using AutoMapper;
using Business.Exceptions;
using Business.Features.Results;
using Data.Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Features.Users
{
    public class FindById
    {
        public class Query : IRequest<UserResult.Full>
        {
            public Guid Id { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                //Query Validations
            }
        }

        public class Handler : AsyncRequestHandler<Query, UserResult.Full>
        {
            private readonly IMapper _mapper;
            private readonly Db _db;

            public Handler(IMapper mapper, Db db)
            {
                _mapper = mapper;
                _db = db;
            }

            protected override async Task<UserResult.Full> HandleCore(Query query)
            {
                var user = await _db.Users.Include(u => u.Tasks).Where(u => u.Id.Equals(query.Id)).FirstOrDefaultAsync();

                if (user == null) throw new NotFoundException("The " + nameof(user) + " with id: " + query.Id + " doesn't exist");

                return _mapper.Map<UserResult.Full>(user);
            }
        }
    }
}
