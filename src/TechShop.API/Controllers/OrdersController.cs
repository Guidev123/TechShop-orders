using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechShop.Application.Queries;

namespace TechShop.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var result = await _mediator.Send(new GetOrderById(id));
            if(result is null) return NotFound();

            return Ok(result);
        }
    }
}
