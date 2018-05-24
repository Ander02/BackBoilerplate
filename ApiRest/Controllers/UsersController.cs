using Business.Features.Results;
using Business.Features.Users;
using MediatR;
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
        public async Task<List<UserResult.Full>> FindAll([FromQuery] FindAll.Query query) => await _mediator.Send(query);

        [HttpGet("{id}")]
        public async Task<UserResult.Full> FindById([FromRoute] FindById.Query query) => await _mediator.Send(query);

        [HttpPut("{id}")]
        public async Task<UserResult.Full> Update([FromRoute] Guid id, [FromBody] Update.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Delete.Command command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> Remove([FromRoute] Remove.Command command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
    }
}