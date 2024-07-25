using CalenderApp.Application.Bases;
using CalenderApp.Application.Exceptions;
using CalenderApp.Application.Features.Etkinlikler.Queries.Bases;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciEtkinlikleriGetir
{
    public class KullaniciEtkinlikleriGetirHandler(
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(httpContextAccessor, calenderAppDbContext), IRequestHandler<KullaniciEtkinlikleriGetirRequest, IList<KullaniciEtkinligiGetirResponse>>
    {
        public async Task<IList<KullaniciEtkinligiGetirResponse>> Handle(KullaniciEtkinlikleriGetirRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new NotFoundException("Mevcut Kullanıcı Bulunamadı.");

            IList<Etkinlik> kullaniciEtkinlikleri = await _calenderAppDbContext.Etkinliks
                .Where(e => e.OlusturanKullaniciId == mevcutKullaniciId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

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
