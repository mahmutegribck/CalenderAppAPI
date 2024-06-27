using MediatR;

namespace CalenderApp.Application.Features.Etkinlikler.Commands.EtkinliktenDavetliKullanicilariSil
{
    public class EtkinliktenDavetliKullanicilariSilRequest : IRequest
    {
        public int EtkinlikId { get; set; }
        public required List<string> KullaniciIds { get; set; }
    }
}
