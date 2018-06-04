using AutoMapper;
using Business.Exceptions;
using Business.Features.Results;
using Data.Database;
using Data.Domain;
using Data.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Utility.Extensions;

namespace Business.Features.Users
{
    public class UpdatePassword
    {
        public class Command : IRequest<UserResult.Full>
        {
            public Guid Id { get; set; }
            public string Password { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                //RuleFor(u => u.Id).NotEmpty().NotNull();
                RuleFor(u => u.Password).NotEmpty().NotNull().EmailAddress();
            }
        }

        public class Handler : AsyncRequestHandler<Command, UserResult.Full>
        {
            private readonly IMapper _mapper;
            private readonly Db _db;
            private readonly UserManager<User> _userManager;

            public Handler(IMapper mapper, Db db, UserManager<User> userManager)
            {
                _mapper = mapper;
                _db = db;
                _userManager = userManager;
            }

            protected override async Task<UserResult.Full> HandleCore(Command command)
            {
                //Find user
                var user = await _db.Users.FindAsync(command.Id);

                //Verify user
                if (user is null) throw new NotFoundException("The " + nameof(user) + " with id: " + command.Id + " doesn't exist");

                if (user.IsDeleted()) throw new BadRequestException("The " + nameof(user) + " is deleted");

                //Change password
                var changeResult = await _userManager.ChangePasswordAsync(user, user.PasswordHash, command.Password);

                if (!changeResult.Succeeded) throw new BadRequestException(changeResult.Errors);

                await _db.SaveChangesAsync();

                return _mapper.Map<UserResult.Full>(user);
            }
        }
    }
}
