using CalenderApp.Application.Features.Kullanicilar.Queries.MevcutKullaniciGetir;
using CalenderApp.Application.Features.Kullanicilar.Queries.TumKullanicilariGetir;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalenderApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class KullaniciController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> MevcutKullaniciGetir(CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new MevcutKullaniciGetirRequest(), cancellationToken);

            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> TumKullanicilariGetir(CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new TumKullanicilariGetirRequest(), cancellationToken);

            return Ok(response);
        }
    }
}
