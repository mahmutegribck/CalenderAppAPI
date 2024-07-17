using FluentValidation;
using MediatR;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.EtkinligeDavetliKullanicilariGetir
{
    public class EtkinligeDavetliKullanicilariGetirRequest : IRequest<IList<EtkinligeDavetliKullanicilariGetirResponse>>
    {
        public int EtkinlikId { get; set; }
    }

    public class EtkinligeDavetliKullanicilariGetirRequestValidator : AbstractValidator<EtkinligeDavetliKullanicilariGetirRequest>
    {
        public EtkinligeDavetliKullanicilariGetirRequestValidator()
        {
            RuleFor(x => x.EtkinlikId)
            .GreaterThan(0).WithMessage("EtkinlikId pozitif bir sayı olmalıdır.")
            .NotEmpty().WithMessage("EtkinlikId boş olamaz.");

        }
    }
}
