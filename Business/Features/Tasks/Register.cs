﻿using Business.Exceptions;
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
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(u => u.UserId).NotEmpty().NotNull();
                RuleFor(u => u.Name).NotEmpty().NotNull();
                RuleFor(u => u.Description).NotEmpty().NotNull();
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

                var user = await _db.Users.FindAsync(command.UserId);

                if (user == null) throw new NotFoundException("The " + nameof(user) + " with id: " + command.UserId + " doesn't exist");

                var task = new Data.Domain.Task()
                {
                    User = user,
                    Name = command.Name,
                    Description = command.Description,
                    CreatedAt = DateTime.Now
                };

                await _db.Tasks.AddAsync(task);
                await _db.SaveChangesAsync();

                return new TaskResult.Full(task);
            }
        }
    }
}