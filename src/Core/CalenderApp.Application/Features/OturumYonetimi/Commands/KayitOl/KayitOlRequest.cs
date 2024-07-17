using FluentValidation;
using MediatR;

namespace CalenderApp.Application.Features.OturumYonetimi.Commands.KayitOl
{
    public class KayitOlRequest : IRequest
    {
        public required string KullaniciAdi { get; set; }
        public required string Isim { get; set; }
        public required string Soyisim { get; set; }
        public required string KullaniciSifresi { get; set; }
        public required string KullaniciSifresiTekrar { get; set; }
    }


    public class KayitOlRequestValidator : AbstractValidator<KayitOlRequest>
    {
        public KayitOlRequestValidator()
        {
            RuleFor(x => x.KullaniciAdi)
                .NotEmpty().WithMessage("Kullanıcı Adı boş olamaz.")
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Kullanıcı Adı sadece boşluk karakterlerinden oluşamaz.");

            RuleFor(x => x.Isim)
                .NotEmpty().WithMessage("İsim boş olamaz.")
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("İsim sadece boşluk karakterlerinden oluşamaz.");

            RuleFor(x => x.Soyisim)
                .NotEmpty().WithMessage("Soyisim boş olamaz.")
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Soyisim sadece boşluk karakterlerinden oluşamaz.");

            RuleFor(x => x.KullaniciSifresi)
                .NotEmpty().WithMessage("Kullanıcı Şifresi boş olamaz.")
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Kullanıcı Şifresi sadece boşluk karakterlerinden oluşamaz.");

            RuleFor(x => x.KullaniciSifresiTekrar)
                .NotEmpty().WithMessage("Kullanıcı Şifresi Tekrar boş olamaz.")
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Kullanıcı Şifresi Tekrar sadece boşluk karakterlerinden oluşamaz.")
                .Equal(x => x.KullaniciSifresi).WithMessage("Kullanıcı Şifresi ve Kullanıcı Şifresi Tekrar eşleşmiyor.");
        }
    }
}
