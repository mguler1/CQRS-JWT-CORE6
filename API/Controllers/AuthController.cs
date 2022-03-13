using API.Core.Application.Features.CQRS.Commands;
using API.Core.Application.Features.CQRS.Queries;
using API.Infrastructure.Tools;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult>Register(RegisterUserCommandRequest request)
        {
            await _mediator.Send(request);
            return Created("", request);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> SignIn(CheckUserQueryRequest request)
        {
           var userDto= await _mediator.Send(request);
            if (userDto.IsExist)
            {
                var token=JwtTokenGenerator.GenerateToken(userDto);
                
                return Created("", token);
            }
            return BadRequest("UserName veya PAssword Hatalı");
        }
    }
}
