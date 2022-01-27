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
        public virtual DbSet<Sehirler> Sehirlers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=Api;User Id=sa;Password=20342034T-d?;MultipleActiveResultSets=true");
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

            modelBuilder.Entity<Sehirler>(entity =>
            {
                entity.ToTable("Sehirler");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Adi)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
