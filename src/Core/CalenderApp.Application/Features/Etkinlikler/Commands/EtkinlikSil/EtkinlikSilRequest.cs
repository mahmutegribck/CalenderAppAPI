using MediatR;

namespace CalenderApp.Application.Features.Etkinlikler.Commands.EtkinlikSil
{
    public class EtkinlikSilRequest : IRequest
    {
        public int EtkinlikId { get; set; }
    }
}
