using MediatR;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.EtkinligeDavetliKullanicilariGetir
{
    public class EtkinligeDavetliKullanicilariGetirRequest : IRequest<IList<EtkinligeDavetliKullanicilariGetirResponse>>
    {
        public int EtkinlikId { get; set; }
    }
}
