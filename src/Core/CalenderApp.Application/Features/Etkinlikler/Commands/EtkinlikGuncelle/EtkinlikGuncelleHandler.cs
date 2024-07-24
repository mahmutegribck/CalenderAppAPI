using CalenderApp.Application.Bases;
using CalenderApp.Application.Exceptions;
using CalenderApp.Domain.Entities;
using CalenderApp.Domain.Enums;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Commands.EtkinlikGuncelle
{
    public class EtkinlikGuncelleHandler(
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(httpContextAccessor, calenderAppDbContext), IRequestHandler<EtkinlikGuncelleRequest>
    {
        public async Task Handle(EtkinlikGuncelleRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new NotFoundException("Mevcut Kullanici Bulunamadi.");

            if (!await _calenderAppDbContext.Etkinliks.AnyAsync(e => e.Id == request.Id, cancellationToken)) throw new NotFoundException("Güncellenmek İstenene Etkinlik Bulunamadı.");

            if (request.BitisTarihi < request.BaslangicTarihi) throw new Exception("Tarih Doğrulanamdı.");

            var exist = await _calenderAppDbContext.Etkinliks
                .Where(e => e.OlusturanKullaniciId == mevcutKullaniciId && e.Id != request.Id)
                .AnyAsync(e =>
                (e.BaslangicTarihi >= request.BaslangicTarihi && (e.BitisTarihi <= request.BitisTarihi || request.BitisTarihi < e.BitisTarihi) && e.BaslangicTarihi <= request.BitisTarihi) ||
                (e.BaslangicTarihi <= request.BaslangicTarihi && (e.BitisTarihi < request.BitisTarihi || request.BitisTarihi <= e.BitisTarihi) && request.BaslangicTarihi <= e.BitisTarihi), cancellationToken);

            if (exist) throw new Exception("Girilen Tarih Araliginda Etkinlik Kaydi Bulunmaktadir.");

            Etkinlik etkinlikGuncelle = new()
            {
                Id = request.Id,
                Baslik = request.Baslik,
                Aciklama = request.Aciklama,
                BaslangicTarihi = request.BaslangicTarihi,
                BitisTarihi = request.BitisTarihi,
                TekrarDurumu = request.TekrarDurumu,
                OlusturanKullaniciId = mevcutKullaniciId
            };

            if (!await _calenderAppDbContext.Etkinliks.AnyAsync(e => e.Id == request.Id && e.OlusturanKullaniciId == mevcutKullaniciId, cancellationToken)) throw new NotFoundException("Guncellenecek Etkinlik Kaydi Bulunamadi.");

            _calenderAppDbContext.Update(etkinlikGuncelle);
            await _calenderAppDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
