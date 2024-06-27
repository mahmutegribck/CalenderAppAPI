using MediatR;

namespace CalenderApp.Application.Features.Etkinlikler.Commands.EtkinligeKullaniciEkle
{
    public class EtkinligeKullaniciEkleRequest : IRequest
    {
        public int EtkinlikId { get; set; }
        public required List<string> KullaniciIds { get; set; }
    }
}
