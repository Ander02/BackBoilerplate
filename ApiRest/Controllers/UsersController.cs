using ApiRest.Infraestructure.Authentication;
using Business.Features.Results;
using Business.Features.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiRest.Features.Users
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Register.Command command)
        {
            var result = await _mediator.Send(command);

            return Created(this.Request.Path + "/" + result.Id, result);
        }

        [HttpGet]
        public async Task<List<UserResult.Full>> FindMany([FromQuery] FindMany.Query query) => await _mediator.Send(query);

        [HttpGet("{id}")]
        public async Task<UserResult.Full> FindById([FromRoute] FindById.Query query) => await _mediator.Send(query);

        [HttpPatch("email/{id}")]
        public async Task<UserResult.Full> UpdateEmail([FromRoute] Guid id, [FromBody] UpdateEmail.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [HttpPatch("password/{id}")]
        public async Task<UserResult.Full> UpdatePassword([FromRoute] Guid id, [FromBody] UpdatePassword.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [HttpPatch("username/{id}")]
        public async Task<UserResult.Full> UpdateUserName([FromRoute] Guid id, [FromBody] UpdateUserName.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<UserResult.Full> Update([FromRoute] Guid id, [FromBody] Update.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [JwtAuth]
        [Authorize(Policy = "IsAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Delete.Command command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [JwtAuth]
        [Authorize(Policy = "IsAdmin")]
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> Remove([FromRoute] Remove.Command command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
    }
}