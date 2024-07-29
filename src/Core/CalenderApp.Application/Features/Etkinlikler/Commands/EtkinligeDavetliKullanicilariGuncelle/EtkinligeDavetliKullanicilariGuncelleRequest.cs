using FluentValidation;
using MediatR;

namespace CalenderApp.Application.Features.Etkinlikler.Commands.EtkinligeDavetliKullanicilariGuncelle
{
    public class EtkinligeDavetliKullanicilariGuncelleRequest :IRequest
    {
        public int EtkinlikId { get; set; }
        public required List<string> KullaniciIds { get; set; }
    }

    public class EtkinligeDavetliKullanicilariGuncelleRequestValidator : AbstractValidator<EtkinligeDavetliKullanicilariGuncelleRequest>
    {
        public EtkinligeDavetliKullanicilariGuncelleRequestValidator()
        {
            RuleFor(e => e.EtkinlikId)
                .NotEmpty().WithMessage("Etkinlik Id Boş Olamaz.")
                .GreaterThanOrEqualTo(0).WithMessage("Etkinlik Id 0'dan küçük olamaz.");

            RuleFor(e => e.KullaniciIds)
                .NotEmpty().WithMessage("KullaniciIds Boş Olamaz.")
                .Must(ids => ids.All(id => !string.IsNullOrWhiteSpace(id)))
                .WithMessage("Kullanici Id içinde boş veya sadece boşluk karakterleri içeremez.");

        }
    }
}
