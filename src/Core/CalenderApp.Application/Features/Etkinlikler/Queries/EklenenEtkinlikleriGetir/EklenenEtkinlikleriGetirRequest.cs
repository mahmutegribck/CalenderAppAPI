using MediatR;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.EklenenEtkinlikleriGetir
{
    public class EklenenEtkinlikleriGetirRequest : IRequest<IList<EklenenEtkinlikleriGetirResponse>>
    {
    }
}
