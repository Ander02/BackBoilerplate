using AutoMapper;
using Business.Exceptions;
using Business.Features.Results;
using Data.Database;
using Data.Domain;
using Data.Domain.Identitty;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Business.Features.Users
{
    public class Register
    {
        public class Command : IRequest<UserResult.Full>
        {
            public string UserName { get; set; }
            public string Name { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public int Age { get; set; }
            public string Role { get; set; }
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
            private readonly UserManager<User> _userManager;
            private readonly RoleManager<Role> _roleManager;

            public Handler(IMapper mapper, Db db, UserManager<User> userManager, RoleManager<Role> roleManager)
            {
                _mapper = mapper;
                _db = db;
                _userManager = userManager;
                _roleManager = roleManager;
            }

            protected override async Task<UserResult.Full> HandleCore(Command command)
            {
                var user = new User
                {
                    Age = command.Age,
                    Email = command.Email,
                    Name = command.Name,
                    UserName = command.UserName,
                    CreatedAt = DateTime.Now
                };

                var userResult = await _userManager.CreateAsync(user, command.Password);

                if (!userResult.Succeeded) throw new BadRequestException(userResult.Errors);

                //Add Role Claim
                userResult = await _userManager.AddClaimsAsync(user, new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, command.Role)
                });

                if (!userResult.Succeeded) throw new BadRequestException(userResult.Errors);

                await _db.SaveChangesAsync();

                return _mapper.Map<UserResult.Full>(user);
            }
        }
    }
}