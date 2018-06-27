using AutoMapper;
using Business.Exceptions;
using Business.Features.Results;
using Data.Database;
using Data.Extensions;
using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Business.Features.Users
{
    public class Update
    {
        public class Command : IRequest<UserResult.Full>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int? Age { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                //RuleFor(u => u.Id).NotEmpty().NotNull();
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
                //Find
                var user = await _db.Users.FindAsync(command.Id);

                //Verify
                if (user is null) throw new NotFoundException("The " + nameof(user) + " with id: " + command.Id + " doesn't exist");

                if (user.IsDeleted()) throw new BadRequestException("The " + nameof(user) + " is deleted");

                //Change
                user.Name = command.Name ?? user.Name;
                user.Age = command.Age ?? user.Age;

                await _db.SaveChangesAsync();

                return _mapper.Map<UserResult.Full>(user);
            }
        }
    }
}
