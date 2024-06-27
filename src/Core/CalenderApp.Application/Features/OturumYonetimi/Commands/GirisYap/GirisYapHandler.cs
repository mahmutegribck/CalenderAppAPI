using AutoMapper;
using CalenderApp.Application.Bases;
using CalenderApp.Application.Interfaces.Tokens;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CalenderApp.Application.Features.OturumYonetimi.Commands.GirisYap
{
    public class GirisYapHandler(
        IJwtServisi jwtServisi,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(mapper, httpContextAccessor, calenderAppDbContext), IRequestHandler<GirisYapRequest, GirisYapResponse>
    {
        private readonly IJwtServisi _jwtServisi = jwtServisi;

        public async Task<GirisYapResponse> Handle(GirisYapRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var byteArray = Encoding.Default.GetBytes(request.KullaniciSifresi);
                var hashedSifre = Convert.ToBase64String(SHA256.HashData(byteArray));

                request.KullaniciSifresi = hashedSifre;
                request.KullaniciAdi = request.KullaniciAdi.Trim().ToLower();

                Kullanici? kullanici = await _calenderAppDbContext.Kullanicis.Where(k => k.KullaniciAdi == request.KullaniciAdi && k.KullaniciSifresi == request.KullaniciSifresi).FirstOrDefaultAsync(cancellationToken);
                if (kullanici == null)
                {
                    return new GirisYapResponse
                    {
                        AccessToken = null
                    };
                }

                string token = _jwtServisi.JwtTokenOlustur(kullanici);

                return new GirisYapResponse
                {
                    AccessToken = token
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
