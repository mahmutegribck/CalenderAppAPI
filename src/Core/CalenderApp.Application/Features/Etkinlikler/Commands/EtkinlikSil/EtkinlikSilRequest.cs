using FluentValidation;
using MediatR;

namespace CalenderApp.Application.Features.Etkinlikler.Commands.EtkinlikSil
{
    public class EtkinlikSilRequest : IRequest
    {
        public int EtkinlikId { get; set; }
    }


    public class EtkinlikSilRequestValidator : AbstractValidator<EtkinlikSilRequest>
    {
        public EtkinlikSilRequestValidator()
        {
            RuleFor(x => x.EtkinlikId)
            .GreaterThan(0).WithMessage("EtkinlikId pozitif bir sayı olmalıdır.")
            .NotEmpty().WithMessage("EtkinlikId boş olamaz.");

        }
    }
}
