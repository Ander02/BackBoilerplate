using FluentValidation;
using Business.Features.Results;
using MediatR;
using System;
using System.Threading.Tasks;
using Data.Database;
using Business.Exceptions;
using Business.Util.Extensions;

namespace Business.Features.Tasks
{
    public class Delete
    {
        public class Command : IRequest<TaskResult.Full>
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

        public class Handler : AsyncRequestHandler<Command, TaskResult.Full>
        {
            private readonly Db _db;

            public Handler(Db db)
            {
                _db = db;
            }

            protected override async Task<TaskResult.Full> HandleCore(Command command)
            {
                if (command == null) throw new BadRequestException("The argument is null");

                var task = await _db.Tasks.FindAsync(command.Id);

                if (task == null) throw new NotFoundException("The " + nameof(task) + " with Id: " + command.Id + " doesn't exist");

                if (!task.DeletedAt.IsDefaultDateTime()) throw new BadRequestException("The " + nameof(task) + " has already been deleted");

                task.DeletedAt = DateTime.Now;
                await _db.SaveChangesAsync();

                return new TaskResult.Full(task);
            }
        }
    }
}
