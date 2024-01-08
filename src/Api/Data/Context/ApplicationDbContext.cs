using Api.Common;
using Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Api.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure default schema
        modelBuilder.HasDefaultSchema("personal_finance_management");

        // Map entity to table
        modelBuilder.Entity<Entity>(et =>
        {
            et.UseTpcMappingStrategy();

            et.HasKey(e => e.Id);

            et.Property(e => e.Id)
                .HasValueGenerator<GuidValueGenerator>()
                .HasColumnName("id");

            et.Property(et => et.CreatedAt)
                .HasColumnName("created_at");

            et.Property(et => et.UpdatedAt)
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Account>(e =>
        {
            e.ToTable("accounts");

            e.Property(acc => acc.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(100)")
                .IsRequired();

            e.HasIndex(acc => acc.Name)
                .IsUnique();

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

        modelBuilder.Entity<Category>(e =>
        {
            e.ToTable("categories");

            e.Property(cat => cat.Type)
                .HasColumnName("type")
                .HasColumnType("varchar(20)")
                .HasConversion<string>()
                .IsRequired();

            e.Property(cat => cat.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(30)")
                .IsRequired();

            e.HasIndex(cat => new { cat.Name, cat.Type })
                .IsUnique();
        });

        modelBuilder.Entity<Transaction>(e =>
        {
            e.ToTable("transactions");

            e.Property(trans => trans.Type)
                .HasColumnName("type")
                .HasColumnType("varchar(20)")
                .HasConversion<string>()
                .IsRequired();

            e.Property(trans => trans.Description)
                .HasColumnName("description")
                .HasColumnType("text")
                .HasMaxLength(500);

            e.Property(trans => trans.Amount)
                .HasColumnName("amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            e.Property(trans => trans.Time)
                .HasColumnName("time")
                .IsRequired();

            e.Property(trans => trans.AccountId)
                .HasColumnName("account_id")
                .IsRequired();

            e.Property(trans => trans.CategoryId)
                .HasColumnName("category_id");
        });
    }

    public override int SaveChanges()
    {
        OnBeforeSaving();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnBeforeSaving()
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
    }
}