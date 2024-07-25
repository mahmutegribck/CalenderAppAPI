using FluentValidation;
using MediatR;

namespace CalenderApp.Application.Features.Etkinlikler.Commands.EtkinligeKullaniciEkle
{
    public class EtkinligeKullaniciEkleRequest : IRequest
    {
        public int EtkinlikId { get; set; }
        public required List<string> KullaniciAdlari { get; set; }
    }


    public class EtkinligeKullaniciEkleRequestValidator : AbstractValidator<EtkinligeKullaniciEkleRequest>
    {
        public EtkinligeKullaniciEkleRequestValidator()
        {
            RuleFor(e => e.EtkinlikId)
                .NotEmpty().WithMessage("Etkinlik Id Boş Olamaz.")
                .GreaterThanOrEqualTo(0).WithMessage("Etkinlik Id 0'dan küçük olamaz.");

            RuleFor(e => e.KullaniciAdlari)
                .NotEmpty().WithMessage("Kullanici Adlari Boş Olamaz.")
                .Must(ids => ids.All(id => !string.IsNullOrWhiteSpace(id)))
                .WithMessage("Kullanici adlari içinde boş veya sadece boşluk karakterleri içeremez.");

        }
    }
}
