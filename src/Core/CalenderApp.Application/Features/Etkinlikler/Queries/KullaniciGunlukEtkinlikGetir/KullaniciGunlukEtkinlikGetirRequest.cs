using CalenderApp.Application.Features.Etkinlikler.Queries.Bases;
using MediatR;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciGunlukEtkinlikGetir
{
    public class KullaniciGunlukEtkinlikGetirRequest : IRequest<IList<KullaniciEtkinligiGetirResponse>>
    {
        public DateTime Tarih { get; set; }

    }
}
