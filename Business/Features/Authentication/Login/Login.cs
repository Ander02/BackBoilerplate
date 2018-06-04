using Business.Exceptions;
using Data.Database;
using Data.Domain;
using Data.Domain.Identitty;
using Data.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
            public DateTime CreatedAt { get; set; }
            public string Token { get; set; }
            public DateTime Expiration { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command, Result>
        {
            private readonly Db _db;
            private readonly IPasswordHasher<User> _hasher;
            private readonly UserManager<User> _userManager;
            private readonly RoleManager<Role> _roleManager;

            public Handler(Db db, IPasswordHasher<User> hasher, UserManager<User> userManager, RoleManager<Role> roleManager)
            {
                _db = db;
                _hasher = hasher;
                _userManager = userManager;
                _roleManager = roleManager;
            }

            //Move to settings.json after
            private const string _tokenKey = "66B6EF09E0CBE5DCEDE4BD5069C5A41D156D2E786A73EB1EB997D46C488B0DF9C9481655004712047AA11323292D1455C5A7AAF9EDD03B6CBA76B6300160A5DC";
            private const string _tokenIssuer = "Backboilerplate";
            private const string _tokenAudience = "WEB";

            protected override async Task<Result> HandleCore(Command command)
            {
                //find user
                var user = await _userManager.FindByEmailAsync(command.Email);

                //verify if user is valid
                if (user is null || user.IsDeleted()) throw new UnauthorizedException("This user hasn't acess");

                if (_hasher.VerifyHashedPassword(user, user.PasswordHash, command.Password) != PasswordVerificationResult.Success) throw new UnauthorizedException("This user hasn't acess");

                //Get Roles and Claims
                var userRoles = await _userManager.GetRolesAsync(user);
                var userClaims = await _userManager.GetClaimsAsync(user);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }.Union(userClaims);

                //Create Token
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenKey));

                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var securityToken = new JwtSecurityToken(_tokenIssuer,
                                                 _tokenAudience,
                                                 claims,
                                                 expires: DateTime.Now.AddHours(3),
                                                 signingCredentials: credentials);

                return new Result
                {
                    CreatedAt = DateTime.Now,
                    Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                    Expiration = securityToken.ValidTo
                };
            }
        }
    }
}
