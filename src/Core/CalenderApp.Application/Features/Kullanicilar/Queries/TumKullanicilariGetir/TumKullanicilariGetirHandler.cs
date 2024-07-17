using CalenderApp.Application.Bases;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Kullanicilar.Queries.TumKullanicilariGetir
{
    public class TumKullanicilariGetirHandler(
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(httpContextAccessor, calenderAppDbContext), IRequestHandler<TumKullanicilariGetirRequest, IList<TumKullanicilariGetirResponse>>
    {
        public async Task<IList<TumKullanicilariGetirResponse>> Handle(TumKullanicilariGetirRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new Exception("Mevcut Kullanici Bulunamadi.");

            var kullanicilar = await _calenderAppDbContext.Kullanicis.Where(k => k.Id != mevcutKullaniciId).ToListAsync(cancellationToken);

            if (kullanicilar.Count == 0) throw new Exception("Kullanici Bulunamadi.");

            IList<TumKullanicilariGetirResponse> response = kullanicilar.Select(k => new TumKullanicilariGetirResponse
            {
                Id = k.Id,
                KullaniciAdi = k.KullaniciAdi,
                Isim = k.Isim,
                Soyisim = k.Soyisim,
            }).ToList();

            return response;
        }
    }
}
