using AutoMapper;
using Business.Exceptions;
using Business.Features.Results;
using Data.Database;
using Data.Extensions;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.Extensions;

namespace Business.Features.Tasks
{
    public class ToogleComplete

    {
        public class Command : IRequest<TaskResult.Full>
        {
            public Guid Id { get; set; }
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

                if (task is null) throw new NotFoundException("The " + nameof(task) + " with id: " + command.Id + " doesn't exist");

                if (task.IsDeleted()) throw new BadRequestException("The " + nameof(task) + " is deleted");
                
                if (task.CompletedAt.IsDefaultDateTime()) task.CompletedAt = DateTime.Now;
                else task.CompletedAt = default(DateTime);

                await _db.SaveChangesAsync();

                return _mapper.Map<TaskResult.Full>(task);
            }
        }
    }
}
