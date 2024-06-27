using CalenderApp.Application.Features.Etkinlikler.Queries.Bases;
using MediatR;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciEtkinlikleriGetir
{
    public class KullaniciEtkinlikleriGetirRequest : IRequest<IList<KullaniciEtkinligiGetirResponse>>
    {
    }
}
