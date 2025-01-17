﻿using CalenderApp.Domain.Enums;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.Bases
{
    public class KullaniciEtkinligiGetirResponse
    {
        public int Id { get; set; }
        public string? Baslik { get; set; }
        public string? Aciklama { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public TekrarEnum TekrarDurumu { get; set; }
    }
}
