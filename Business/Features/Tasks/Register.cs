﻿using AutoMapper;
using Business.Exceptions;
using Business.Features.Results;
using Data.Database;
using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Business.Features.Tasks
{
    public class Register
    {
        public class Command : IRequest<TaskResult.Full>
        {
            public Guid UserId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(u => u.UserId).NotEmpty().NotNull();
                RuleFor(u => u.Title).NotEmpty().NotNull();
                RuleFor(u => u.Description).NotEmpty().NotNull();
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
                var user = await _db.Users.FindAsync(command.UserId);

                var task = _mapper.Map<Data.Domain.Task>(command);

                task.User = user ?? throw new NotFoundException("The " + nameof(user) + " with id: " + command.UserId + " doesn't exist");
                task.CreatedAt = DateTime.Now;

                await _db.Tasks.AddAsync(task);
                await _db.SaveChangesAsync();

                return _mapper.Map<TaskResult.Full>(task);
            }
        }
    }
}
