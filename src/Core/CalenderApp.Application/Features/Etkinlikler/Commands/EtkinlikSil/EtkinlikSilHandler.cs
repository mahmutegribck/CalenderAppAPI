using AutoMapper;
using CalenderApp.Application.Bases;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Commands.EtkinlikSil
{
    public class EtkinlikSilHandler(
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(mapper, httpContextAccessor, calenderAppDbContext), IRequestHandler<EtkinlikSilRequest>
    {
        public async Task Handle(EtkinlikSilRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new Exception("Mevcut Kullanici Bulunamadi.");

            Etkinlik? etkinlikSil = await _calenderAppDbContext.Etkinliks.Where(e => e.Id == request.EtkinlikId && e.OlusturanKullaniciId == mevcutKullaniciId).FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("Silinmek İstenen Etkinlik Kaydı Bulunamadı.");

            _calenderAppDbContext.Etkinliks.Remove(etkinlikSil);
            await _calenderAppDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
