using CalenderApp.Application.Features.Etkinlikler.Commands.EtkinligeKullaniciEkle;
using CalenderApp.Domain.Enums;
using FluentValidation;
using MediatR;

namespace CalenderApp.Application.Features.Etkinlikler.Commands.EtkinlikGuncelle
{
    public class EtkinlikGuncelleRequest : IRequest
    {
        public int Id { get; set; }
        public required string Baslik { get; set; }
        public string? Aciklama { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public TekrarEnum TekrarDurumu { get; set; }
    }


    public class EtkinlikGuncelleRequestValidator : AbstractValidator<EtkinlikGuncelleRequest>
    {
        public EtkinlikGuncelleRequestValidator()
        {
            RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id pozitif bir sayı olmalıdır.");

            RuleFor(x => x.Baslik)
                .NotEmpty().WithMessage("Başlık boş olamaz.")
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Başlık sadece boşluk karakterlerinden oluşamaz.");

            RuleFor(x => x.BaslangicTarihi)
                .NotEmpty().WithMessage("Başlangıç Tarihi boş olamaz.")
                .Must(x => x != default(DateTime)).WithMessage("Geçerli bir Başlangıç Tarihi giriniz.");

            RuleFor(x => x.BitisTarihi)
                .NotEmpty().WithMessage("Bitiş Tarihi boş olamaz.")
                .Must(x => x != default(DateTime)).WithMessage("Geçerli bir Bitiş Tarihi giriniz.")
                .GreaterThanOrEqualTo(x => x.BaslangicTarihi).WithMessage("Bitiş Tarihi Başlangıç Tarihi'nden önce olamaz.");

            RuleFor(x => x.TekrarDurumu)
                .IsInEnum().WithMessage("Geçerli bir Tekrar Durumu seçiniz.");
        }
    }
}
