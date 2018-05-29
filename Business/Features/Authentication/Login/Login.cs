using Business.Exceptions;
using Data.Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Features.Authentication.Login
{
    public class Login
    {
        public class Command : IRequest<Result>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.Email).NotNull().NotEmpty();
                RuleFor(c => c.Password).NotNull().NotEmpty();
            }
        }

        public class Result
        {
            public string Token { get; set; }
            public DateTime Expiration { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command, Result>
        {
            private readonly Db _db;

            public Handler(Db db)
            {
                _db = db;
            }

            private const string _tokenKey = "66B6EF09E0CBE5DCEDE4BD5069C5A41D156D2E786A73EB1EB997D46C488B0DF9C9481655004712047AA11323292D1455C5A7AAF9EDD03B6CBA76B6300160A5DC";
            private const string _tokenIssuer = "Backboilerplate";
            private const string _tokenAudience = "WEB";

            protected override async Task<Result> HandleCore(Command command)
            {
                var user = await _db.Users.SingleOrDefaultAsync(u => u.Email.Equals(command.Email));

                if (user == null || !user.IsPasswordEqualsTo(command.Password)) throw new BaseHttpException(401, "Unauthorized");

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_tokenIssuer,
                                                 _tokenAudience,
                                                 claims,
                                                 expires: DateTime.Now.AddHours(3),
                                                 signingCredentials: creds);

                return new Result
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };
            }
        }
    }
}
