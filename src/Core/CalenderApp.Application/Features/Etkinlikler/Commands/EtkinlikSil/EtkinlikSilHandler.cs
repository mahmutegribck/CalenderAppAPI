﻿using CalenderApp.Application.Bases;
using CalenderApp.Application.Exceptions;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Commands.EtkinlikSil
{
    public class EtkinlikSilHandler(
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(httpContextAccessor, calenderAppDbContext), IRequestHandler<EtkinlikSilRequest>
    {
        public async Task Handle(EtkinlikSilRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new NotFoundException("Mevcut Kullanici Bulunamadı.");

            Etkinlik? etkinlikSil = await _calenderAppDbContext.Etkinliks.Where(e => e.Id == request.EtkinlikId && e.OlusturanKullaniciId == mevcutKullaniciId).FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException("Silinmek İstenen Etkinlik Kaydı Bulunamadı.");

            _calenderAppDbContext.Etkinliks.Remove(etkinlikSil);
            await _calenderAppDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
