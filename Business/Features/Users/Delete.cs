using Business.Exceptions;
using Business.Features.Results;
using Business.Util.Extensions;
using Data.Database;
using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Business.Features.Users
{
    public class Delete
    {
        public class Command : IRequest<UserResult.Full>
        {
            public Guid Id { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.Id).NotNull().NotEmpty();
            }
        }

        public class Handler : AsyncRequestHandler<Command, UserResult.Full>
        {
            private readonly Db _db;

            public Handler(Db db)
            {
                _db = db;
            }

            protected override async Task<UserResult.Full> HandleCore(Command command)
            {
                if (command == null) throw new BadRequestException("The " + nameof(command) + " is null");

                var user = await _db.Users.FindAsync(command.Id);

                if (user == null) throw new NotFoundException("The user with Id: " + command.Id + " doesn't exist");

                if (!user.DeletedAt.IsDefaultDateTime()) throw new BadRequestException("The " + nameof(user) + " has already been deleted");

                user.DeletedAt = DateTime.Now;
                await _db.SaveChangesAsync();

                return new UserResult.Full(user);
            }
        }
    }
}
