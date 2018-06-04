using Business.Features.Authentication.Login;
using Business.Features.Results;
using Business.Features.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiRest.Features.Tasks
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Login.Result> Login([FromBody] Login.Command command) => await _mediator.Send(command);
    }
}