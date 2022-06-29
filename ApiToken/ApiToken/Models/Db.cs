using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ApiToken.Models
{
    public partial class Db : DbContext
    {
        public Db()
        {
        }

        public Db(DbContextOptions<Db> options)
            : base(options)
        {
        }

        public virtual DbSet<Kullanicilar> Kullanicilars { get; set; }
        public virtual DbSet<Models.City> Sehirlers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Api;User Id=sa;Password=ŞifreniziGiriniz;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<Kullanicilar>(entity =>
            {
                entity.ToTable("Kullanicilar");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Durumu).HasDefaultValueSql("((1))");

                entity.Property(e => e.KullaniciAdi).HasMaxLength(250);

                entity.Property(e => e.Sifre).HasMaxLength(250);
            });

            modelBuilder.Entity<Models.City>(entity =>
            {
                entity.ToTable("Sehirler");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
