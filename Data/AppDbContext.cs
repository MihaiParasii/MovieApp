using ChineseNetflix.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChineseNetflix.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public DbSet<Customer> Customers { get; init; }
    public DbSet<Movie> Movies { get; init; }
    public DbSet<MovieDetail> MovieDetail { get; init; }
    public DbSet<Actor> Actors { get; init; }
    public DbSet<Genre> Genres { get; init; }
    public DbSet<AppUser> AppUsers { get; init; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        var serverVersion = new MySqlServerVersion(new Version(8, 3, 0));
        const string connectionString = "server=localhost;user=root;password=;database=ChineseNetflix";
        optionsBuilder.UseMySql(connectionString, serverVersion);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AppUserEntityConfiguration());
    }
}

public class AppUserEntityConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(u => u.Nickname).HasMaxLength(40);
        builder.Property(u => u.Surname).HasMaxLength(40);
    }
}
