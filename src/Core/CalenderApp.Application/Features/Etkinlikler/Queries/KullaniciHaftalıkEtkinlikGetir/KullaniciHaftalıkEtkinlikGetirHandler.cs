using CalenderApp.Application.Bases;
using CalenderApp.Application.Exceptions;
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
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(httpContextAccessor, calenderAppDbContext), IRequestHandler<KullaniciHaftalikEtkinlikGetirRequest, IList<KullaniciEtkinligiGetirResponse>>
    {
        public async Task<IList<KullaniciEtkinligiGetirResponse>> Handle(KullaniciHaftalikEtkinlikGetirRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new NotFoundException("Mevcut Kullanıcı Bulunamadı.");

            var culture = CultureInfo.InvariantCulture;
            Calendar calendar = culture.Calendar;
            CalendarWeekRule calendarWeekRule = culture.DateTimeFormat.CalendarWeekRule;
            DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;

            int haftaNo = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(request.Tarih, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            List<Etkinlik>? etkinlikler = await _calenderAppDbContext.Etkinliks
                .Where(e => e.OlusturanKullaniciId == mevcutKullaniciId &&
                            e.BaslangicTarihi.Year <= request.Tarih.Year &&
                            e.BitisTarihi.Year >= request.Tarih.Year)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            List<Etkinlik> filteredEtkinlikler = [];
            if (etkinlikler.Count != 0)
            {
                filteredEtkinlikler = etkinlikler
                .Where(e => calendar.GetWeekOfYear(e.BaslangicTarihi, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) == haftaNo ||
                            calendar.GetWeekOfYear(e.BitisTarihi, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) == haftaNo ||
                            (e.BaslangicTarihi <= request.Tarih && e.BitisTarihi >= request.Tarih))
                .ToList();
            }

            IList<KullaniciEtkinligiGetirResponse> response = filteredEtkinlikler.Select(e => new KullaniciEtkinligiGetirResponse
            {
                Id = e.Id,
                Baslik = e.Baslik,
                Aciklama = e.Aciklama,
                BaslangicTarihi = e.BaslangicTarihi,
                BitisTarihi = e.BitisTarihi,
                TekrarDurumu = e.TekrarDurumu
            }).ToList();

            return response;
        }
    }
}
