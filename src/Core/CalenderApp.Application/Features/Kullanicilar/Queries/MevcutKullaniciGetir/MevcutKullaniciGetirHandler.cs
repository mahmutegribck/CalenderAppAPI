using CalenderApp.Application.Bases;
using CalenderApp.Application.Exceptions;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Kullanicilar.Queries.MevcutKullaniciGetir
{
    public class MevcutKullaniciGetirHandler(
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(httpContextAccessor, calenderAppDbContext), IRequestHandler<MevcutKullaniciGetirRequest, MevcutKullaniciGetirResponse>
    {
        public async Task<MevcutKullaniciGetirResponse> Handle(MevcutKullaniciGetirRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new NotFoundException("Mevcut Kullanıcı Bulunamadı.");

            Kullanici? mevcutKullanici = await _calenderAppDbContext.Kullanicis
                .Where(k => k.Id == mevcutKullaniciId)
                .FirstOrDefaultAsync(cancellationToken);

            MevcutKullaniciGetirResponse response = new()
            {
                Id = mevcutKullanici.Id,
                KullaniciAdi = mevcutKullanici.KullaniciAdi,
                Isim = mevcutKullanici.Isim,
                Soyisim = mevcutKullanici.Soyisim,
            };

            return response;
        }
    }
}
