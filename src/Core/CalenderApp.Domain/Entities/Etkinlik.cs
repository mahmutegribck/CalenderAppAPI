using CalenderApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CalenderApp.Domain.Entities
{
    public sealed class Etkinlik
    {
        public Etkinlik()
        {
            KatilanKullanicilar = new HashSet<KullaniciEtkinlik>();
        }

        [Key]
        public int Id { get; set; }
        public required string Baslik { get; set; }
        public string? Aciklama { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public TekrarEnum TekrarDurumu { get; set; }

        public required Kullanici OlusturanKullanici { get; set; }
        public required string OlusturanKullaniciId { get; set; }

        public ICollection<KullaniciEtkinlik> KatilanKullanicilar { get; set; }

    }
}
