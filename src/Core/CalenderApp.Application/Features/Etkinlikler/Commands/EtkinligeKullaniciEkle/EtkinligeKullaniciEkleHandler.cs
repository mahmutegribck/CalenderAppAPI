using CalenderApp.Application.Bases;
using CalenderApp.Application.Exceptions;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Commands.EtkinligeKullaniciEkle
{
    public class EtkinligeKullaniciEkleHandler(
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(httpContextAccessor, calenderAppDbContext), IRequestHandler<EtkinligeKullaniciEkleRequest>
    {
        public async Task Handle(EtkinligeKullaniciEkleRequest request, CancellationToken cancellationToken)
        {
            if (!await _calenderAppDbContext.Etkinliks.AnyAsync(e => e.OlusturanKullaniciId == mevcutKullaniciId && e.Id == request.EtkinlikId, cancellationToken)) throw new NotFoundException("Kullanıcının Kayıtlı Etkinliği Bulunamadı.");
            List<KullaniciEtkinlik> kullaniciEtkinlikListesi = new();

            foreach (var kullaniciId in request.KullaniciAdlari)
            {
                var kullanici = await _calenderAppDbContext.Kullanicis.Where(k => k.KullaniciAdi == kullaniciId).FirstAsync(cancellationToken);
                if (kullanici != null)
                {
                    KullaniciEtkinlik kullaniciEtkinlik = new()
                    {
                        KullaniciId = kullanici.Id,
                        EtkinlikId = request.EtkinlikId
                    };
                    kullaniciEtkinlikListesi.Add(kullaniciEtkinlik);
                }
            }
            await _calenderAppDbContext.KullaniciEtkinliks.AddRangeAsync(kullaniciEtkinlikListesi, cancellationToken);
            await _calenderAppDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
