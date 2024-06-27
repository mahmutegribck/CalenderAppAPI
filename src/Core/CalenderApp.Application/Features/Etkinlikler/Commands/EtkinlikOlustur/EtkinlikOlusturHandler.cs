using AutoMapper;
using CalenderApp.Application.Bases;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Commands.EtkinlikOlustur
{
    public class EtkinlikOlusturHandler(
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(mapper, httpContextAccessor, calenderAppDbContext), IRequestHandler<EtkinlikOlusturRequest>
    {
        public async Task Handle(EtkinlikOlusturRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new Exception("Mevcut Kullanici Bulunamadi.");

            Etkinlik etkinlikOlustur = _mapper.Map<Etkinlik>(request);
            etkinlikOlustur.OlusturanKullaniciId = mevcutKullaniciId;

            if (request.BitisTarihi < request.BaslangicTarihi) throw new Exception("Tarih Doğrulanamdı.");

            var exist = await _calenderAppDbContext.Etkinliks
                .Where(e => e.OlusturanKullaniciId == mevcutKullaniciId)
                .AnyAsync(e =>
                (e.BaslangicTarihi >= request.BaslangicTarihi && (e.BitisTarihi <= request.BitisTarihi || request.BitisTarihi < e.BitisTarihi) && e.BaslangicTarihi <= request.BitisTarihi) ||
                (e.BaslangicTarihi <= request.BaslangicTarihi && (e.BitisTarihi < request.BitisTarihi || request.BitisTarihi <= e.BitisTarihi) && request.BaslangicTarihi <= e.BitisTarihi), cancellationToken);

            if (exist) throw new Exception("Girilen Tarih Araliginda Etkinlik Kaydi Bulunmaktadir.");

            await _calenderAppDbContext.Etkinliks.AddAsync(etkinlikOlustur, cancellationToken);
            await _calenderAppDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
