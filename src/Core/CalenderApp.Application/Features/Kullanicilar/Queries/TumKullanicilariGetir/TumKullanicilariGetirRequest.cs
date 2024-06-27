using MediatR;

namespace CalenderApp.Application.Features.Kullanicilar.Queries.TumKullanicilariGetir
{
    public class TumKullanicilariGetirRequest : IRequest<IList<TumKullanicilariGetirResponse>>
    {
    }
}
