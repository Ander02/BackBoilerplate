using AutoMapper;
using Business.Exceptions;
using Business.Features.Results;
using Business.Util.Extensions;
using Data.Database;
using Data.Extensions;
using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;

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
            private readonly IMapper _mapper;
            private readonly Db _db;

            public Handler(IMapper mapper, Db db)
            {
                _mapper = mapper;
                _db = db;
            }

            protected override async Task<TaskResult.Full> HandleCore(Command command)
            {
                var task = await _db.Tasks.FindAsync(command.Id);

                if (task == null) throw new NotFoundException("The " + nameof(task) + " with Id: " + command.Id + " doesn't exist");

                if (task.IsDeleted()) throw new BadRequestException("The " + nameof(task) + " has already been deleted");

                task.DeletedAt = DateTime.Now;
                await _db.SaveChangesAsync();

                return _mapper.Map<TaskResult.Full>(task);
            }
        }
    }
}
