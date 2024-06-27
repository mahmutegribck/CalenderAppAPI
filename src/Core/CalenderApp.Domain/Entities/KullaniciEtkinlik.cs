namespace CalenderApp.Domain.Entities
{
    public sealed class KullaniciEtkinlik
    {
        public int EtkinlikId { get; set; }
        public Etkinlik Etkinlik { get; set; }

        public required string KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; }
    }
}
