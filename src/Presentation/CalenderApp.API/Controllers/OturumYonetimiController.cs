using CalenderApp.Application.Features.OturumYonetimi.Commands.GirisYap;
using CalenderApp.Application.Features.OturumYonetimi.Commands.KayitOl;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalenderApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class OturumYonetimiController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> KayitOl([FromBody] KayitOlRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> GirisYap([FromBody] GirisYapRequest request, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);

        }
    }
}
