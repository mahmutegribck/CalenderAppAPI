using CalenderApp.Application.Features.Etkinlikler.Queries.Bases;
using MediatR;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciHaftalıkEtkinlikGetir
{
    public class KullaniciHaftalikEtkinlikGetirRequest : IRequest<IList<KullaniciEtkinligiGetirResponse>>
    {
        public DateTime Tarih { get; set; }
    }
}
