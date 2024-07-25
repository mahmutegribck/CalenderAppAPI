using CalenderApp.Application.Bases;
using CalenderApp.Application.Exceptions;
using CalenderApp.Application.Features.Etkinlikler.Queries.Bases;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciAylikEtkinlikGetir
{
    public class KullaniciAylikEtkinlikGetirHandler(
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(httpContextAccessor, calenderAppDbContext), IRequestHandler<KullaniciAylikEtkinlikGetirRequest, IList<KullaniciEtkinligiGetirResponse>>
    {
        public async Task<IList<KullaniciEtkinligiGetirResponse>> Handle(KullaniciAylikEtkinlikGetirRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new NotFoundException("Mevcut Kullanıcı Bulunamadı.");

            List<Etkinlik>? etkinlikler = await _calenderAppDbContext.Etkinliks
                .Where(e => e.OlusturanKullaniciId == mevcutKullaniciId &&
                            e.BaslangicTarihi.Month <= request.Tarih.Month &&
                            e.BitisTarihi.Month >= request.Tarih.Month &&
                            e.BaslangicTarihi.Year <= request.Tarih.Year &&
                            e.BitisTarihi.Year >= request.Tarih.Year)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            IList<KullaniciEtkinligiGetirResponse> response = etkinlikler.Select(e => new KullaniciEtkinligiGetirResponse
            {
                Id = e.Id,
                Baslik = e.Baslik,
                Aciklama = e.Aciklama,
                BaslangicTarihi = e.BaslangicTarihi,
                BitisTarihi = e.BitisTarihi,
                TekrarDurumu = e.TekrarDurumu,
            }).ToList();

            return response;
        }
    }

}
