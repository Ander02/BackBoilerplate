using Business.Exceptions;
using Data.Database;
using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Business.Features.Users
{
    public class Remove
    {
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                //Validations
            }
        }

        public class Handler : AsyncRequestHandler<Command, bool>
        {
            private readonly Db _db;

            public Handler(Db db)
            {
                _db = db;
            }

            protected override async Task<bool> HandleCore(Command command)
            {
                var user = await _db.Users.FindAsync(command.Id);

                if (user is null) throw new NotFoundException("The " + nameof(user) + " with Id: " + command.Id + " doesn't exist");

                _db.Users.Remove(user);
                await _db.SaveChangesAsync();

                return true;
            }
        }
    }
}
