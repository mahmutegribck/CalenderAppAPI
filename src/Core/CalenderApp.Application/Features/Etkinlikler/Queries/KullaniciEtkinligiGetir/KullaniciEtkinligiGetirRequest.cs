using CalenderApp.Application.Features.Etkinlikler.Queries.Bases;
using FluentValidation;
using MediatR;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciEtkinligiGetir
{
    public class KullaniciEtkinligiGetirRequest : IRequest<KullaniciEtkinligiGetirResponse>
    {
        public int EtkinlikId { get; set; }
    }

    public class KullaniciEtkinligiGetirRequestValidator : AbstractValidator<KullaniciEtkinligiGetirRequest>
    {
        public KullaniciEtkinligiGetirRequestValidator()
        {
            RuleFor(x => x.EtkinlikId)
            .GreaterThan(0).WithMessage("EtkinlikId pozitif bir sayı olmalıdır.")
            .NotEmpty().WithMessage("EtkinlikId boş olamaz.");

        }
    }
}
