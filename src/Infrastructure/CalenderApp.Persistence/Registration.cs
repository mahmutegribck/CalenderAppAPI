using CalenderApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CalenderApp.Persistence
{
    public static class Registration
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CalenderAppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));
        }
    }
}
