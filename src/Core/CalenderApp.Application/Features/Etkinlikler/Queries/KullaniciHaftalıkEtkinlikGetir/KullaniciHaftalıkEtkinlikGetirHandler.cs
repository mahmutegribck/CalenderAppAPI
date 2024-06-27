using AutoMapper;
using CalenderApp.Application.Bases;
using CalenderApp.Application.Features.Etkinlikler.Queries.Bases;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciHaftalıkEtkinlikGetir
{
    public class KullaniciHaftalıkEtkinlikGetirHandler(
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(mapper, httpContextAccessor, calenderAppDbContext), IRequestHandler<KullaniciHaftalikEtkinlikGetirRequest, IList<KullaniciEtkinligiGetirResponse>>
    {
        public async Task<IList<KullaniciEtkinligiGetirResponse>> Handle(KullaniciHaftalikEtkinlikGetirRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new Exception("Mevcut Kullanıcı Bulunamadı.");

            var culture = CultureInfo.InvariantCulture;
            Calendar calendar = culture.Calendar;
            CalendarWeekRule calendarWeekRule = culture.DateTimeFormat.CalendarWeekRule;
            DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;

            var haftaNumarasi = calendar.GetWeekOfYear(request.Tarih, calendarWeekRule, firstDayOfWeek);

            List<Etkinlik>? etkinlikler = await _calenderAppDbContext.Etkinliks
                .Where(e => e.OlusturanKullaniciId == mevcutKullaniciId &&
                            e.BaslangicTarihi.Year == request.Tarih.Year)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (etkinlikler.Count == 0) throw new Exception("İstenen Haftaya Ait Kullanici Etkinliği Bulunamadı.");

            List<Etkinlik> filteredEtkinlikler = etkinlikler
                .Where(e => calendar.GetWeekOfYear(e.BaslangicTarihi, calendarWeekRule, firstDayOfWeek) == haftaNumarasi)
                .ToList();

            if (filteredEtkinlikler.Count == 0) throw new Exception("İstenen Haftaya Ait Kullanici Etkinliği Bulunamadı.");

            return _mapper.Map<IList<KullaniciEtkinligiGetirResponse>>(filteredEtkinlikler);
        }
    }
}
