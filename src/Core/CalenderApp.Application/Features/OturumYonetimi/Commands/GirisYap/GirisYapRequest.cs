using MediatR;

namespace CalenderApp.Application.Features.OturumYonetimi.Commands.GirisYap
{
    public class GirisYapRequest : IRequest<GirisYapResponse>
    {
        public required string KullaniciAdi { get; set; }
        public required string KullaniciSifresi { get; set; }
    }
}
