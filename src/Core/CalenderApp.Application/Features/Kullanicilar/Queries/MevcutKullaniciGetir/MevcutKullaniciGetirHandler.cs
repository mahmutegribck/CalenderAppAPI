using AutoMapper;
using CalenderApp.Application.Bases;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Kullanicilar.Queries.MevcutKullaniciGetir
{
    public class MevcutKullaniciGetirHandler(
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        CalenderAppDbContext calenderAppDbContext) : BaseHandler(mapper, httpContextAccessor, calenderAppDbContext), IRequestHandler<MevcutKullaniciGetirRequest, MevcutKullaniciGetirResponse>
    {
        public async Task<MevcutKullaniciGetirResponse> Handle(MevcutKullaniciGetirRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new Exception("Mevcut Kullanici Bulunamadi.");

            Kullanici? mevcutKullanici = await _calenderAppDbContext.Kullanicis.Where(k => k.Id == mevcutKullaniciId).FirstOrDefaultAsync(cancellationToken);

            if (mevcutKullanici == null) throw new Exception("Kullanici Bulunamadi.");

            return _mapper.Map<MevcutKullaniciGetirResponse>(mevcutKullanici);
        }
    }
}
