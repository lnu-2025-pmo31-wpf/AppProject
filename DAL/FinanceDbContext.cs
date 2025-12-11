using Microsoft.EntityFrameworkCore;
using FinanceManager.DAL.Entities;

namespace FinanceManager.DAL
{
    public class FinanceDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }

        public FinanceDbContext() { }

        public FinanceDbContext(DbContextOptions<FinanceDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=finance;Username=postgres;Password=1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Description)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.Amount)
                      .IsRequired()
                      .HasPrecision(18, 2);

                entity.Property(e => e.Type)
                      .IsRequired();

                entity.Property(e => e.Date)
                      .HasDefaultValueSql("NOW()");
            });
        }
    }
}
