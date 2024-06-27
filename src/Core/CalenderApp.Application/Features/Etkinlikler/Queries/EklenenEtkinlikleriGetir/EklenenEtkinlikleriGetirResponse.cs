using CalenderApp.Domain.Enums;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.EklenenEtkinlikleriGetir
{
    public class EklenenEtkinlikleriGetirResponse
    {
        public int Id { get; set; }
        public required string Baslik { get; set; }
        public string? Aciklama { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public TekrarEnum TekrarDurumu { get; set; }
        public required string EkleyenKullaniciId { get; set; }
        public required string EkleyenKullaniciAdi { get; set; }
    }
}
