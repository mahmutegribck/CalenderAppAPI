using CalenderApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CalenderApp.Persistence.Context
{
    public class CalenderAppDbContext : DbContext
    {
        public CalenderAppDbContext(DbContextOptions<CalenderAppDbContext> options) : base(options) { }

        public DbSet<Etkinlik> Etkinliks { get; set; }
        public DbSet<Kullanici> Kullanicis { get; set; }
        public DbSet<KullaniciEtkinlik> KullaniciEtkinliks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Kullanici>()
                .HasIndex(k => k.KullaniciAdi)
                .IsUnique();


            modelBuilder.Entity<Kullanici>(entity =>
            {
                entity.HasMany(e => e.OlusturduguEtkinlikler)
                    .WithOne(e => e.OlusturanKullanici)
                    .HasForeignKey(e => e.OlusturanKullaniciId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.KatildigiEtkinlikler)
                    .WithOne(e => e.Kullanici)
                    .HasForeignKey(e => e.KullaniciId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            //composite keys
            modelBuilder.Entity<KullaniciEtkinlik>().HasKey(e => new { e.KullaniciId, e.EtkinlikId });

            modelBuilder.Entity<KullaniciEtkinlik>(entity =>
            {
                entity.HasOne(e => e.Kullanici)
                    .WithMany(e => e.KatildigiEtkinlikler)
                    .HasForeignKey(e => e.KullaniciId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(e => e.Etkinlik)
                    .WithMany(e => e.KatilanKullanicilar)
                    .HasForeignKey(e => e.EtkinlikId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Etkinlik>(entity =>
            {
                entity.HasOne(e => e.OlusturanKullanici)
                    .WithMany(e => e.OlusturduguEtkinlikler)
                    .HasForeignKey(e => e.OlusturanKullaniciId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasMany(e => e.KatilanKullanicilar)
                    .WithOne(e => e.Etkinlik)
                    .HasForeignKey(e => e.EtkinlikId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
