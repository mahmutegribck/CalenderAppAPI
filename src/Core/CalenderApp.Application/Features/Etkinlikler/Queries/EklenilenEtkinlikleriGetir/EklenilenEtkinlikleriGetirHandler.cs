using CalenderApp.Application.Bases;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.EklenilenEtkinlikleriGetir
{
    public class EklenilenEtkinlikleriGetirHandler(
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(httpContextAccessor, calenderAppDbContext), IRequestHandler<EklenilenEtkinlikleriGetirRequest, IList<EklenilenEtkinlikleriGetirResponse>>
    {
        public async Task<IList<EklenilenEtkinlikleriGetirResponse>> Handle(EklenilenEtkinlikleriGetirRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new Exception("Mevcut Kullanıcı Bulunamadı.");

            IList<Etkinlik> eklenenEtkinlikler = await _calenderAppDbContext.KullaniciEtkinliks
                .Where(e => e.KullaniciId == mevcutKullaniciId)
                .Include(e => e.Etkinlik.OlusturanKullanici)
                .Select(e => e.Etkinlik)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (!eklenenEtkinlikler.Any()) throw new Exception("Eklenen Etkinlik Bulunamadı");

            IList<EklenilenEtkinlikleriGetirResponse> response = eklenenEtkinlikler.Select(e => new EklenilenEtkinlikleriGetirResponse
            {
                Id = e.Id,
                Baslik = e.Baslik,
                Aciklama = e.Aciklama,
                BaslangicTarihi = e.BaslangicTarihi,
                BitisTarihi = e.BitisTarihi,
                TekrarDurumu = e.TekrarDurumu,
                EkleyenKullaniciId = e.OlusturanKullaniciId,
                EkleyenKullaniciAdi = e.OlusturanKullanici.KullaniciAdi
            }).ToList();

            return response;
        }
    }
}
