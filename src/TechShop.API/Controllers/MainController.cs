using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using TechShop.Domain.Notifications;
using TechShop.Infrasctructure.Notifications;

namespace TechShop.API.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INotificator _notificador;
        protected MainController(INotificator notificador) => _notificador = notificador;
        protected ActionResult CustomResponse(object result = null!)
        {
            if (ValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notificador.GetNotifications().Select(n => n.Message)
            });
        }
        protected bool ValidOperation() => !_notificador.HasNotification();
        protected void NotifyError(string mensagem) => _notificador.Notify(new Notification(mensagem));
    }
}
