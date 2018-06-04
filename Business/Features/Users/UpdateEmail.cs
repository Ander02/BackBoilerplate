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
    public class UpdateEmail
    {
        public class Command : IRequest<UserResult.Full>
        {
            public Guid Id { get; set; }
            public string Email { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                //RuleFor(u => u.Id).NotEmpty().NotNull();
                RuleFor(u => u.Email).NotEmpty().NotNull().EmailAddress();
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

                //Change email
                var changeResult = await _userManager.ChangeEmailAsync(user, command.Email, await _userManager.GenerateChangeEmailTokenAsync(user, command.Email));

                if (!changeResult.Succeeded) throw new BadRequestException(changeResult.Errors);

                await _db.SaveChangesAsync();

                return _mapper.Map<UserResult.Full>(user);
            }
        }
    }
}
