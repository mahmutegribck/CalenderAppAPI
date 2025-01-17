﻿using FluentValidation;
using MediatR;

namespace CalenderApp.Application.Features.Etkinlikler.Commands.EtkinliktenDavetliKullanicilariSil
{
    public class EtkinliktenDavetliKullanicilariSilRequest : IRequest
    {
        public int EtkinlikId { get; set; }
        public required List<string> KullaniciIds { get; set; }
    }

    public class EtkinliktenDavetliKullanicilariSilRequestValidator : AbstractValidator<EtkinliktenDavetliKullanicilariSilRequest>
    {
        public EtkinliktenDavetliKullanicilariSilRequestValidator()
        {
            RuleFor(e => e.EtkinlikId)
                .NotEmpty().WithMessage("Etkinlik Id Boş Olamaz.")
                .GreaterThanOrEqualTo(0).WithMessage("Etkinlik Id 0'dan küçük olamaz.");

            RuleFor(e => e.KullaniciIds)
                .NotEmpty().WithMessage("Kullanici Ids Boş Olamaz.")
                .Must(ids => ids.All(id => !string.IsNullOrWhiteSpace(id)))
                .WithMessage("Kullanici Ids içinde boş veya sadece boşluk karakterleri içeremez.");

        }
    }
}
