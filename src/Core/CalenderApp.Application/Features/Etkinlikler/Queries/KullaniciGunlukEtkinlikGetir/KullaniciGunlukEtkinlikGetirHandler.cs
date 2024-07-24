using CalenderApp.Application.Bases;
using CalenderApp.Application.Exceptions;
using CalenderApp.Application.Features.Etkinlikler.Queries.Bases;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciGunlukEtkinlikGetir
{
    public class KullaniciGunlukEtkinlikGetirHandler(
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(httpContextAccessor, calenderAppDbContext), IRequestHandler<KullaniciGunlukEtkinlikGetirRequest, IList<KullaniciEtkinligiGetirResponse>>
    {
        public async Task<IList<KullaniciEtkinligiGetirResponse>> Handle(KullaniciGunlukEtkinlikGetirRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new NotFoundException("Mevcut Kullanıcı Bulunamadı.");

            IList<Etkinlik> kullaniciEtkinlikleri = await _calenderAppDbContext.Etkinliks
                .Where(e => e.OlusturanKullaniciId == mevcutKullaniciId &&
                            e.BaslangicTarihi.DayOfYear <= request.Tarih.DayOfYear &&
                            e.BitisTarihi.DayOfYear >= request.Tarih.DayOfYear &&
                            e.BaslangicTarihi.Year <= request.Tarih.Year &&
                            e.BitisTarihi.Year >= request.Tarih.Year)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (!kullaniciEtkinlikleri.Any()) throw new NotFoundException("İstenen Güne Ait Kullanıcı Etkinliği Bulunamadı.");

            IList<KullaniciEtkinligiGetirResponse> response = kullaniciEtkinlikleri.Select(e => new KullaniciEtkinligiGetirResponse
            {
                Id = e.Id,
                Baslik = e.Baslik,
                Aciklama = e.Aciklama,
                BaslangicTarihi = e.BaslangicTarihi,
                BitisTarihi = e.BitisTarihi,
                TekrarDurumu = e.TekrarDurumu
            }).ToList();

            return response;
        }
    }
}
