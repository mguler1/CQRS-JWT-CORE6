using API.Core.Application.Features.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

       [HttpGet]
       public async Task<IActionResult> List()
        {
          var result=  await _mediator.Send(new GetAllProductsQueryRequest());
            return Ok(result);
        }
    }
}
