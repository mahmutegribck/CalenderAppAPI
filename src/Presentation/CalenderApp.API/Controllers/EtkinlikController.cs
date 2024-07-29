using CalenderApp.Application.Features.Etkinlikler.Commands.EtkinligeDavetliKullanicilariGuncelle;
using CalenderApp.Application.Features.Etkinlikler.Commands.EtkinligeKullaniciEkle;
using CalenderApp.Application.Features.Etkinlikler.Commands.EtkinlikGuncelle;
using CalenderApp.Application.Features.Etkinlikler.Commands.EtkinlikOlustur;
using CalenderApp.Application.Features.Etkinlikler.Commands.EtkinlikSil;
using CalenderApp.Application.Features.Etkinlikler.Commands.EtkinliktenDavetliKullanicilariSil;
using CalenderApp.Application.Features.Etkinlikler.Queries.EklenilenEtkinlikleriGetir;
using CalenderApp.Application.Features.Etkinlikler.Queries.EtkinligeDavetliKullanicilariGetir;
using CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciAylikEtkinlikGetir;
using CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciEtkinligiGetir;
using CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciEtkinlikleriGetir;
using CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciGunlukEtkinlikGetir;
using CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciHaftalıkEtkinlikGetir;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CalenderApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EtkinlikController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> EtkinlikOlustur([FromBody] EtkinlikOlusturRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest();

            await mediator.Send(request, cancellationToken);
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> EtkinlikGuncelle([FromBody] EtkinlikGuncelleRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest();

            await mediator.Send(request, cancellationToken);
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> EtkinlikSil([FromQuery] EtkinlikSilRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest();

            await mediator.Send(request, cancellationToken);
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> EtkinligeKullaniciEkle([FromBody] EtkinligeKullaniciEkleRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest();

            await mediator.Send(request, cancellationToken);
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> EtkinliktenDavetliKullanicilariSil([FromBody] EtkinliktenDavetliKullanicilariSilRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest();

            await mediator.Send(request, cancellationToken);
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> EtkinligeDavetliKullanicilariGuncelle([FromBody] EtkinligeDavetliKullanicilariGuncelleRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest();

            await mediator.Send(request, cancellationToken);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> EtkinligeDavetliKullanicilariGetir([FromQuery] int etkinlikId, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new EtkinligeDavetliKullanicilariGetirRequest { EtkinlikId = etkinlikId }, cancellationToken);
            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> KullaniciEtkinligiGetir([FromQuery] int etkinlikId, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new KullaniciEtkinligiGetirRequest { EtkinlikId = etkinlikId }, cancellationToken);
            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> KullaniciEtkinlikleriGetir(CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new KullaniciEtkinlikleriGetirRequest(), cancellationToken);
            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> EklenilenEtkinlikleriGetir(CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new EklenilenEtkinlikleriGetirRequest(), cancellationToken);
            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> KullaniciAylikEtkinlikGetir(DateTime tarih, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new KullaniciAylikEtkinlikGetirRequest { Tarih = tarih }, cancellationToken);
            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> KullaniciHaftalikEtkinlikGetir(DateTime tarih, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new KullaniciHaftalikEtkinlikGetirRequest { Tarih = tarih }, cancellationToken);
            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> KullaniciGunlukEtkinlikGetir(DateTime tarih, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new KullaniciGunlukEtkinlikGetirRequest { Tarih = tarih }, cancellationToken);
            return Ok(response);
        }
    }
}
