using eAppointmentAppServer.Application.Features.Auth.Login;
using eAppointmentAppServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eAppointmentAppServer.WebAPI.Controllers
{
    [AllowAnonymous]  //burada authorize attributunun çalışmamasını sağladık
    public sealed class AuthController : ApiController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginCommand req, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(req, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
