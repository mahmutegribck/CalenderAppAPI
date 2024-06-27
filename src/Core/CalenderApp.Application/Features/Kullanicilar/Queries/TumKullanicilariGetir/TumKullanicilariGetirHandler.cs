using AutoMapper;
using CalenderApp.Application.Bases;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Kullanicilar.Queries.TumKullanicilariGetir
{
    public class TumKullanicilariGetirHandler(
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(mapper, httpContextAccessor, calenderAppDbContext), IRequestHandler<TumKullanicilariGetirRequest, IList<TumKullanicilariGetirResponse>>
    {
        public async Task<IList<TumKullanicilariGetirResponse>> Handle(TumKullanicilariGetirRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new Exception("Mevcut Kullanici Bulunamadi.");

            var kullanicilar = await _calenderAppDbContext.Kullanicis.Where(k => k.Id != mevcutKullaniciId).ToListAsync(cancellationToken);

            if (kullanicilar.Count == 0) throw new Exception("Kullanici Bulunamadi.");

            return _mapper.Map<IList<TumKullanicilariGetirResponse>>(kullanicilar);

        }
    }
}
