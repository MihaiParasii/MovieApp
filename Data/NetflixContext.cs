using ChineseNetflix.Models;
using Microsoft.EntityFrameworkCore;

namespace ChineseNetflix.Data;

public class NetflixContext : DbContext
{
    public DbSet<Customer> Customers { get; init; }
    public DbSet<Movie> Movies { get; init; }
    public DbSet<MovieDetail> MovieDetail { get; init; }
    public DbSet<Actor> Actors { get; init; }
    public DbSet<Genre> Genres { get; init; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        var serverVersion = new MySqlServerVersion(new Version(8, 3, 0));
        const string connectionString = "server=localhost;user=root;password=;database=ChineseNetflix";
        optionsBuilder.UseMySql(connectionString, serverVersion);
    }
}
