using AutoMapper;
using CalenderApp.Application.Bases;
using CalenderApp.Application.Features.Etkinlikler.Queries.Bases;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciEtkinligiGetir
{
    public class KullaniciEtkinligiGetirHandler(
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(mapper, httpContextAccessor, calenderAppDbContext), IRequestHandler<KullaniciEtkinligiGetirRequest, KullaniciEtkinligiGetirResponse>
    {
        public async Task<KullaniciEtkinligiGetirResponse> Handle(KullaniciEtkinligiGetirRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new Exception("Mevcut Kullanıcı Bulunamadı.");

            Etkinlik? kullaniciEtkinligi = await _calenderAppDbContext.Etkinliks
                .Where(e => e.OlusturanKullaniciId == mevcutKullaniciId && e.Id == request.EtkinlikId)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (kullaniciEtkinligi == null) throw new Exception("Kullanıcı Etkinlikleri Bulunumadı.");

            return _mapper.Map<KullaniciEtkinligiGetirResponse>(kullaniciEtkinligi);
        }
    }
}
