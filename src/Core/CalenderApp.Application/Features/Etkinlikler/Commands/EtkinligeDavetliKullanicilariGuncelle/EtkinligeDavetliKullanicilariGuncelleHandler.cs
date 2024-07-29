using CalenderApp.Application.Bases;
using CalenderApp.Application.Exceptions;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Commands.EtkinligeDavetliKullanicilariGuncelle
{
    public class EtkinligeDavetliKullanicilariGuncelleHandler(
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(httpContextAccessor, calenderAppDbContext), IRequestHandler<EtkinligeDavetliKullanicilariGuncelleRequest>
    {
        public async Task Handle(EtkinligeDavetliKullanicilariGuncelleRequest request, CancellationToken cancellationToken)
        {
            if (!await _calenderAppDbContext.Etkinliks.AnyAsync(e => e.OlusturanKullaniciId == mevcutKullaniciId && e.Id == request.EtkinlikId, cancellationToken)) throw new NotFoundException("Kullanıcının Kayıtlı Etkinliği Bulunamadı.");

            var currentAttendees = await _calenderAppDbContext.KullaniciEtkinliks
                .Where(ke => ke.EtkinlikId == request.EtkinlikId)
                .ToListAsync(cancellationToken);

            var mevcutDavetliIds = currentAttendees.Select(ke => ke.KullaniciId).ToList();
            var yeniDavetliIds = request.KullaniciIds;

            var silinecekDavetliler = mevcutDavetliIds.Except(yeniDavetliIds).ToList();
            var eklenecekDavetliler = yeniDavetliIds.Except(mevcutDavetliIds).ToList();

            _calenderAppDbContext.KullaniciEtkinliks.RemoveRange(currentAttendees.Where(ke => silinecekDavetliler.Contains(ke.KullaniciId)));

            var yeniDavetliler = eklenecekDavetliler.Select(userId => new KullaniciEtkinlik
            {
                EtkinlikId = request.EtkinlikId,
                KullaniciId = userId
            });

            await _calenderAppDbContext.KullaniciEtkinliks.AddRangeAsync(yeniDavetliler, cancellationToken);

            await _calenderAppDbContext.SaveChangesAsync(cancellationToken);

        }
    }
}
