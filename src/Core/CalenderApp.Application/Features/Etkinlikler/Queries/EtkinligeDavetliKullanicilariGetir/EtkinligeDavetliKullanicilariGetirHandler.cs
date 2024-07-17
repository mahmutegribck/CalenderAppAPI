using CalenderApp.Application.Bases;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.EtkinligeDavetliKullanicilariGetir
{
    public class EtkinligeDavetliKullanicilariGetirHandler(
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(httpContextAccessor, calenderAppDbContext), IRequestHandler<EtkinligeDavetliKullanicilariGetirRequest, IList<EtkinligeDavetliKullanicilariGetirResponse>>
    {
        public async Task<IList<EtkinligeDavetliKullanicilariGetirResponse>> Handle(EtkinligeDavetliKullanicilariGetirRequest request, CancellationToken cancellationToken)
        {
            if (!await _calenderAppDbContext.Etkinliks.AnyAsync(e => e.Id == request.EtkinlikId && e.OlusturanKullaniciId == mevcutKullaniciId, cancellationToken)) throw new Exception("Kullanıcını Kayıtlı Etkinliği Bulunamadı.");

            IList<Kullanici> kullanicilar = await _calenderAppDbContext.KullaniciEtkinliks
                .Where(e => e.EtkinlikId == request.EtkinlikId)
                .Select(e => e.Kullanici)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (kullanicilar.Count == 0) throw new Exception("Etkinliğe Davetli Kullanıcı Bulunamadı.");


            IList<EtkinligeDavetliKullanicilariGetirResponse> response = kullanicilar.Select(e => new EtkinligeDavetliKullanicilariGetirResponse
            {
                Id = e.Id,
                KullaniciAdi = e.KullaniciAdi,
                Isim = e.Isim,
                Soyisim= e.Soyisim,
            }).ToList();

            return response;
        }
    }
}
