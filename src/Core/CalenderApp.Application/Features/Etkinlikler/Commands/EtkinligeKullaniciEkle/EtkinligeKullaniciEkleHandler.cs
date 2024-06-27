using AutoMapper;
using CalenderApp.Application.Bases;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Commands.EtkinligeKullaniciEkle
{
    public class EtkinligeKullaniciEkleHandler(
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(mapper, httpContextAccessor, calenderAppDbContext), IRequestHandler<EtkinligeKullaniciEkleRequest>
    {
        public async Task Handle(EtkinligeKullaniciEkleRequest request, CancellationToken cancellationToken)
        {
            if (!await _calenderAppDbContext.Etkinliks.AnyAsync(e => e.OlusturanKullaniciId == mevcutKullaniciId && e.Id == request.EtkinlikId, cancellationToken)) throw new Exception("Kullanıcını Kayıtlı Etkinliği Bulunamadı.");
            List<KullaniciEtkinlik> kullaniciEtkinlikListesi = new();

            foreach (var kullaniciId in request.KullaniciIds)
            {

                if (await _calenderAppDbContext.Kullanicis.AnyAsync(k => k.Id == kullaniciId, cancellationToken))
                {
                    KullaniciEtkinlik kullaniciEtkinlik = new()
                    {
                        KullaniciId = kullaniciId,
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
