﻿using AutoMapper;
using CalenderApp.Application.Bases;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Commands.EtkinliktenDavetliKullanicilariSil
{
    public class EtkinliktenDavetliKullanicilariSilHandler(
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(mapper, httpContextAccessor, calenderAppDbContext), IRequestHandler<EtkinliktenDavetliKullanicilariSilRequest>
    {
        public async Task Handle(EtkinliktenDavetliKullanicilariSilRequest request, CancellationToken cancellationToken)
        {
            if (!await _calenderAppDbContext.Etkinliks.AnyAsync(e => e.Id == request.EtkinlikId && e.OlusturanKullaniciId == mevcutKullaniciId, cancellationToken)) throw new Exception("Kullanıcını Kayıtlı Etkinliği Bulunamadı.");
            List<KullaniciEtkinlik> kullaniciEtkinlikListesi = new();

            foreach (var kullaniciId in request.KullaniciIds)
            {
                KullaniciEtkinlik? kullaniciEtkinlik = await _calenderAppDbContext.KullaniciEtkinliks.Where(e => e.EtkinlikId == request.EtkinlikId && e.KullaniciId == kullaniciId).FirstOrDefaultAsync(cancellationToken);
                if (kullaniciEtkinlik != null)
                {
                    kullaniciEtkinlikListesi.Add(kullaniciEtkinlik);
                }
            }
            if (kullaniciEtkinlikListesi.Count == 0) throw new Exception("Etkinlikten Silinecek Kullanıcı Bulunamadı.");
            _calenderAppDbContext.KullaniciEtkinliks.RemoveRange(kullaniciEtkinlikListesi);
            await _calenderAppDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}