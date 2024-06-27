using CalenderApp.Domain.Enums;
using MediatR;

namespace CalenderApp.Application.Features.Etkinlikler.Commands.EtkinlikOlustur
{
    public class EtkinlikOlusturRequest : IRequest
    {
        public required string Baslik { get; set; }
        public string? Aciklama { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public TekrarEnum TekrarDurumu { get; set; }
    }
}
