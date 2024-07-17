using CalenderApp.Persistence.Context;
using Microsoft.AspNetCore.Http;

namespace CalenderApp.Application.Bases
{
    public class BaseHandler
    {
        public readonly IHttpContextAccessor _httpContextAccessor;
        public readonly CalenderAppDbContext _calenderAppDbContext;
        public readonly string? mevcutKullaniciId;

        public BaseHandler(IHttpContextAccessor httpContextAccessor, CalenderAppDbContext calenderAppDbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _calenderAppDbContext = calenderAppDbContext;
            mevcutKullaniciId = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

        }
    }
}
