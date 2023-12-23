using Api.Common;
using Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure default schema
        modelBuilder.HasDefaultSchema("public");

        // Map entity to table
        modelBuilder.Entity<Entity>(et =>
        {
            et.UseTpcMappingStrategy();
            et.HasKey(e => e.Id);
            et.Property(e => e.Id).HasValueGenerator<GuidValueGenerator>();
        });

        modelBuilder.Entity<Account>(e =>
        {
            e.ToTable("accounts");
            e.Property(acc => acc.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(100)")
                .IsRequired();
            e.Property(acc => acc.Balance)
                .HasColumnName("balance")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            e.Property(acc => acc.TotalLoan)
                .HasColumnName("total_loan")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            e.Property(acc => acc.Status)
                .HasColumnName("status")
                .HasColumnType("varchar(20)")
                .HasConversion<string>()
                .IsRequired();
        });
    }
    
    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is Entity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified)
            );

        foreach (var entityEntry in entries)
        {
            ((Entity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;

            if (entityEntry.State == EntityState.Added)
            {
                ((Entity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
            }
        }

        return base.SaveChanges();
    }
}