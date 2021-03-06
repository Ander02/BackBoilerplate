﻿using AutoMapper;
using Business.Exceptions;
using Data.Database;
using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Business.Features.Tasks
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
            private readonly IMapper _mapper;
            private readonly Db _db;

            public Handler(IMapper mapper, Db db)
            {
                _mapper = mapper;
                _db = db;
            }

            protected override async Task<bool> HandleCore(Command command)
            {
                var task = await _db.Tasks.FindAsync(command.Id);

                if (task is null) throw new NotFoundException("The " + nameof(task) + " with Id: " + command.Id + " doesn't exist");

                _db.Tasks.Remove(task);
                await _db.SaveChangesAsync();

                return true;
            }
        }
    }
}
