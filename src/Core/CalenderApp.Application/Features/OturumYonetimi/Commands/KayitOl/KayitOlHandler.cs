using CalenderApp.Application.Bases;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CalenderApp.Application.Features.OturumYonetimi.Commands.KayitOl
{
    public class KayitOlHandler(
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(httpContextAccessor, calenderAppDbContext), IRequestHandler<KayitOlRequest>
    {
        public async Task Handle(KayitOlRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "Model boş olamaz.");
                }

                var mevcutKullanici = await _calenderAppDbContext.Kullanicis.Where(k => k.KullaniciAdi == request.KullaniciAdi).FirstOrDefaultAsync(cancellationToken);


                if (mevcutKullanici != null)
                {
                    throw new ArgumentException("Bu kullanıcı adı mevcut.", request.KullaniciAdi);
                }

                request.KullaniciAdi = request.KullaniciAdi.Trim().ToLower();

                Kullanici yeniKullanici = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    KullaniciAdi = request.KullaniciAdi,
                    Isim = request.Isim,
                    Soyisim = request.Soyisim,
                    KullaniciSifresi = request.KullaniciSifresi

                };

                var byteArray = Encoding.Default.GetBytes(yeniKullanici.KullaniciSifresi);
                var hashedSifre = Convert.ToBase64String(SHA256.HashData(byteArray));

                yeniKullanici.KullaniciSifresi = hashedSifre;

                await _calenderAppDbContext.Kullanicis.AddAsync(yeniKullanici, cancellationToken);
                await _calenderAppDbContext.SaveChangesAsync(cancellationToken);

            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }
    }
}
