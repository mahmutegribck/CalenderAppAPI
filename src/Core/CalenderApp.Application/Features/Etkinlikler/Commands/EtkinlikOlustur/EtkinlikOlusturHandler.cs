using CalenderApp.Application.Bases;
using CalenderApp.Application.Exceptions;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Commands.EtkinlikOlustur
{
    public class EtkinlikOlusturHandler(
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(httpContextAccessor, calenderAppDbContext), IRequestHandler<EtkinlikOlusturRequest>
    {
        public async Task Handle(EtkinlikOlusturRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new NotFoundException("Mevcut Kullanıcı Bulunamadı.");

            if (request.BitisTarihi < request.BaslangicTarihi) throw new Exception("Tarih Doğrulanamdı.");

            var exist = await _calenderAppDbContext.Etkinliks
                .Where(e => e.OlusturanKullaniciId == mevcutKullaniciId)
                .AnyAsync(e =>
                (e.BaslangicTarihi >= request.BaslangicTarihi && (e.BitisTarihi <= request.BitisTarihi || request.BitisTarihi < e.BitisTarihi) && e.BaslangicTarihi <= request.BitisTarihi) ||
                (e.BaslangicTarihi <= request.BaslangicTarihi && (e.BitisTarihi < request.BitisTarihi || request.BitisTarihi <= e.BitisTarihi) && request.BaslangicTarihi <= e.BitisTarihi), cancellationToken);

            if (exist) throw new Exception("Girilen Tarih Aralığında Etkinlik Kaydı Bulunmaktadır.");

            Etkinlik etkinlikOlustur = new()
            {
                Baslik = request.Baslik,
                Aciklama = request.Aciklama,
                BaslangicTarihi = request.BaslangicTarihi,
                BitisTarihi = request.BitisTarihi,
                TekrarDurumu = request.TekrarDurumu,
                OlusturanKullaniciId = mevcutKullaniciId
            };
            await _calenderAppDbContext.Etkinliks.AddAsync(etkinlikOlustur, cancellationToken);
            await _calenderAppDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
