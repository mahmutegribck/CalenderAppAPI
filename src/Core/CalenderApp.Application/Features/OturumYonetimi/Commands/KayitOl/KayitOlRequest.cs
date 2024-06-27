using MediatR;

namespace CalenderApp.Application.Features.OturumYonetimi.Commands.KayitOl
{
    public class KayitOlRequest : IRequest
    {
        public required string KullaniciAdi { get; set; }
        public required string Isim { get; set; }
        public required string Soyisim { get; set; }
        public required string KullaniciSifresi { get; set; }
        public required string KullaniciSifresiTekrar { get; set; }
    }
}
