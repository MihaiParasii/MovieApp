using ChineseNetflix.Models;
using Microsoft.EntityFrameworkCore;

namespace ChineseNetflix.Database;

public class NetflixContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Actor> Actors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        var serverVersion = new MySqlServerVersion(new Version(8, 3, 0));
        const string connectionString = "server=localhost;user=root;password=;database=ChineseNetflix";
        optionsBuilder.UseMySql(connectionString, serverVersion);
    }
}
