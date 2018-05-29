using AutoMapper;
using Business.Exceptions;
using Business.Features.Results;
using Data.Database;
using Data.Domain;
using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;
using Utility.Extensions;

namespace Business.Features.Users
{
    public class Register
    {
        public class Command : IRequest<UserResult.Full>
        {
            public string Name { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public int Age { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(u => u.Name).NotEmpty().NotNull();
                RuleFor(u => u.Email).NotEmpty().NotNull().EmailAddress();
                RuleFor(u => u.Password).NotEmpty().NotNull().MinimumLength(8);
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
                var user = new User
                {
                    Age = command.Age,
                    Email = command.Email,
                    Name = command.Name,
                    CreatedAt = DateTime.Now
                };
                user.SetPassword(command.Password);

                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();

                return _mapper.Map<UserResult.Full>(user);
            }
        }
    }
}