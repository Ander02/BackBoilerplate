using Data.Database;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Features.Authentication.Logout
{
    public class Logout
    {
        public class Command : IRequest<Result>
        {
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {

            }
        }

        public class Result
        {

        }

        public class Handler : AsyncRequestHandler<Command, Result>
        {
            private readonly Db _db;

            public Handler(Db db)
            {
                _db = db;
            }

            protected override async Task<Result> HandleCore(Command command)
            {
                return null;
            }
        }
    }

}