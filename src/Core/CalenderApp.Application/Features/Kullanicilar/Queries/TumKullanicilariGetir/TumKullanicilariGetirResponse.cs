namespace CalenderApp.Application.Features.Kullanicilar.Queries.TumKullanicilariGetir
{
    public class TumKullanicilariGetirResponse
    {
        public required string Id { get; set; }
        public required string KullaniciAdi { get; set; }
        public required string Isim { get; set; }
        public required string Soyisim { get; set; }
    }
}
