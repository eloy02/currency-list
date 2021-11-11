using CurrencyApp.Model;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApp.Database
{
    public class CurrencyContext : DbContext
    {
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }

        public CurrencyContext(DbContextOptions<CurrencyContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(ent =>
            {
                ent.ToTable(nameof(Currencies));

                ent.HasKey(e => e.Id);

                ent.Property(e => e.Id)
                    .HasMaxLength(10);

                ent.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(225);

                ent.Property(e => e.EngName)
                    .HasMaxLength(225);

                ent.Property(e => e.ParentCode)
                    .HasMaxLength(10);

                ent.Property(e => e.ISOCode)
                    .HasMaxLength(10);

                ent.HasMany(e => e.Rates).WithOne(r => r.Currency);
            });

            modelBuilder.Entity<CurrencyRate>(ent =>
            {
                ent.ToTable(nameof(CurrencyRates));

                ent.HasKey(e => e.Id);

                ent.Property(e => e.Value)
                    .IsRequired();

                ent.HasOne(r => r.Currency).WithMany(c => c.Rates).HasForeignKey(r => r.CurrencyId);
            });
        }
    }
}