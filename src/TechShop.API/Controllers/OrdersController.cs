using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechShop.Application.Commands;
using TechShop.Application.Queries;
using TechShop.Domain.Entities;

namespace TechShop.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetOrderByIdAsync(Guid id)
        {
            var result = await _mediator.Send(new GetOrderById(id));
            if(result is null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrderAsync(CreateOrder command)
        {
            var orderId = await _mediator.Send(command);

            return Ok(orderId);
        }
    }
}
