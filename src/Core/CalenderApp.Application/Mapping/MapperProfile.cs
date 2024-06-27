using AutoMapper;
using CalenderApp.Application.Features.Etkinlikler.Commands.EtkinlikGuncelle;
using CalenderApp.Application.Features.Etkinlikler.Commands.EtkinlikOlustur;
using CalenderApp.Application.Features.Etkinlikler.Queries.Bases;
using CalenderApp.Application.Features.Etkinlikler.Queries.EklenenEtkinlikleriGetir;
using CalenderApp.Application.Features.Etkinlikler.Queries.EtkinligeDavetliKullanicilariGetir;
using CalenderApp.Application.Features.Kullanicilar.Queries.MevcutKullaniciGetir;
using CalenderApp.Application.Features.Kullanicilar.Queries.TumKullanicilariGetir;
using CalenderApp.Application.Features.OturumYonetimi.Commands.KayitOl;
using CalenderApp.Domain.Entities;

namespace CalenderApp.Application.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Etkinlik, EtkinlikOlusturRequest>().ReverseMap();
            CreateMap<Etkinlik, EtkinlikGuncelleRequest>().ReverseMap();

            CreateMap<Kullanici, EtkinligeDavetliKullanicilariGetirResponse>().ReverseMap();
            CreateMap<Etkinlik, KullaniciEtkinligiGetirResponse>().ReverseMap();
            CreateMap<Etkinlik, EklenenEtkinlikleriGetirResponse>()
                .ForMember(dest => dest.EkleyenKullaniciId, opt => opt.MapFrom(src => src.OlusturanKullaniciId))
                .ForMember(dest => dest.EkleyenKullaniciAdi, opt => opt.MapFrom(src => src.OlusturanKullanici.KullaniciAdi));


            CreateMap<Kullanici, KayitOlRequest>().ReverseMap();

            CreateMap<Kullanici, TumKullanicilariGetirResponse>().ReverseMap();
            CreateMap<Kullanici, MevcutKullaniciGetirResponse>().ReverseMap();

        }
    }
}
