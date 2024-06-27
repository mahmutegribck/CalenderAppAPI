using CalenderApp.Application.Features.Etkinlikler.Queries.Bases;
using MediatR;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciAylikEtkinlikGetir
{
    public class KullaniciAylikEtkinlikGetirRequest : IRequest<IList<KullaniciEtkinligiGetirResponse>>
    {
        public DateTime Tarih { get; set; }
    }
}
