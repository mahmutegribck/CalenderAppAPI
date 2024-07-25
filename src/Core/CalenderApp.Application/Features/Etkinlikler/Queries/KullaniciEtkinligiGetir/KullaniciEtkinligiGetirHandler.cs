using CalenderApp.Application.Bases;
using CalenderApp.Application.Exceptions;
using CalenderApp.Application.Features.Etkinlikler.Queries.Bases;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciEtkinligiGetir
{
    public class KullaniciEtkinligiGetirHandler(
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(httpContextAccessor, calenderAppDbContext), IRequestHandler<KullaniciEtkinligiGetirRequest, KullaniciEtkinligiGetirResponse?>
    {
        public async Task<KullaniciEtkinligiGetirResponse?> Handle(KullaniciEtkinligiGetirRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new NotFoundException("Mevcut Kullanıcı Bulunamadı.");

            Etkinlik? kullaniciEtkinligi = await _calenderAppDbContext.Etkinliks
                .Where(e => e.OlusturanKullaniciId == mevcutKullaniciId && e.Id == request.EtkinlikId)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (kullaniciEtkinligi == null)
                return null;

            KullaniciEtkinligiGetirResponse response = new()
            {
                Id = kullaniciEtkinligi.Id,
                Baslik = kullaniciEtkinligi.Baslik,
                Aciklama = kullaniciEtkinligi.Aciklama,
                BaslangicTarihi = kullaniciEtkinligi.BaslangicTarihi,
                BitisTarihi = kullaniciEtkinligi.BitisTarihi,
                TekrarDurumu = kullaniciEtkinligi.TekrarDurumu
            };

            return response;
        }
    }
}
