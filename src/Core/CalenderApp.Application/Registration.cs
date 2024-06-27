using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CalenderApp.Application
{
    public static class Registration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            //Features içerisindeki tüm işlemler MediatR Dependency Injection a eklendi.
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());

        }
    }
}
