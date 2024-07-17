using CalenderApp.Application.Features.Etkinlikler.Queries.Bases;
using FluentValidation;
using MediatR;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciGunlukEtkinlikGetir
{
    public class KullaniciGunlukEtkinlikGetirRequest : IRequest<IList<KullaniciEtkinligiGetirResponse>>
    {
        public DateTime Tarih { get; set; }
    }

    public class KullaniciGunlukEtkinlikGetirRequestValidator : AbstractValidator<KullaniciGunlukEtkinlikGetirRequest>
    {
        public KullaniciGunlukEtkinlikGetirRequestValidator()
        {

            RuleFor(x => x.Tarih)
                .NotEmpty().WithMessage("Tarih boş olamaz.")
                .Must(x => x != default(DateTime)).WithMessage("Geçerli bir Tarih giriniz.");

        }
    }
}
