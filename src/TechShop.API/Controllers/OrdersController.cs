using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechShop.Application.Commands;
using TechShop.Application.Queries;
using TechShop.Domain.Entities;
using TechShop.Domain.Notifications;

namespace TechShop.API.Controllers
{
    [Route("api/orders")]
    public class OrdersController : MainController
    {
        private readonly IMediator _mediator;
        public OrdersController(IMediator mediator, INotificator notificator) : base(notificator) => _mediator = mediator;

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetOrderByIdAsync(Guid id)
        {
            var result = await _mediator.Send(new GetOrderById(id));
            if(result is null) return CustomResponse();
            return CustomResponse(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrderAsync(CreateOrder command)
        {
            var orderId = await _mediator.Send(command);
            return CustomResponse(orderId);
        }
    }
}
