using CalenderApp.Application.Features.Etkinlikler.Queries.Bases;
using FluentValidation;
using MediatR;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciAylikEtkinlikGetir
{
    public class KullaniciAylikEtkinlikGetirRequest : IRequest<IList<KullaniciEtkinligiGetirResponse>>
    {
        public DateTime Tarih { get; set; }
    }

    public class KullaniciAylikEtkinlikGetirRequestValidator : AbstractValidator<KullaniciAylikEtkinlikGetirRequest>
    {
        public KullaniciAylikEtkinlikGetirRequestValidator()
        {

            RuleFor(x => x.Tarih)
                .NotEmpty().WithMessage("Tarih boş olamaz.")
                .Must(x => x != default(DateTime)).WithMessage("Geçerli bir Tarih giriniz.");

        }
    }
}
