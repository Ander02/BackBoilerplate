﻿using AutoMapper;
using Business.Exceptions;
using Business.Features.Results;
using Data.Database;
using Data.Extensions;
using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;
using Utility.Extensions;

namespace Business.Features.Users
{
    public class Update
    {
        public class Command : IRequest<UserResult.Full>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public int? Age { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                //RuleFor(u => u.Id).NotEmpty().NotNull();
                RuleFor(u => u.Name).NotEmpty().NotNull();
                RuleFor(u => u.Email).NotEmpty().NotNull().EmailAddress();
                RuleFor(u => u.Age).NotEmpty().NotNull().GreaterThan(0);
            }
        }

        public class Handler : AsyncRequestHandler<Command, UserResult.Full>
        {
            private readonly IMapper _mapper;
            private readonly Db _db;

            public Handler(IMapper mapper, Db db)
            {
                _mapper = mapper;
                _db = db;
            }

            protected override async Task<UserResult.Full> HandleCore(Command command)
            {
                var user = await _db.Users.FindAsync(command.Id);

                if (user is null) throw new NotFoundException("The " + nameof(user) + " with id: " + command.Id + " doesn't exist");

                if (user.IsDeleted()) throw new BadRequestException("The " + nameof(user) + " is deleted");

                //user.Name = command.Name ?? user.Name;
                //user.Email = command.Email ?? user.Email;
                //user.Age = command.Age ?? user.Age;

                user.UpdatePropsByReflection(command);

                await _db.SaveChangesAsync();

                //return new UserResult.Full(user);
                return _mapper.Map<UserResult.Full>(user);
            }
        }
    }
}
