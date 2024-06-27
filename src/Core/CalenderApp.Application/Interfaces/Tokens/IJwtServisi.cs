using CalenderApp.Domain.Entities;

namespace CalenderApp.Application.Interfaces.Tokens
{
    public interface IJwtServisi
    {
        string JwtTokenOlustur(Kullanici kullanici);

    }
}
