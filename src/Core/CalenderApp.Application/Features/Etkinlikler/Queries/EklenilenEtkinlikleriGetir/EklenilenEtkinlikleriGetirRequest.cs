using MediatR;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.EklenilenEtkinlikleriGetir
{
    public class EklenilenEtkinlikleriGetirRequest : IRequest<IList<EklenilenEtkinlikleriGetirResponse>>
    {
    }
}
