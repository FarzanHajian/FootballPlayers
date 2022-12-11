using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FootballPlayers.Api.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is ApplicationException appException)
                return Problem(appException.Message, statusCode: 400);

            return Problem(detail: "Sorry but there is a problem in our side. Please have a cup of coffee while we are resolving the issue :)", statusCode: 500);
        }
    }
}