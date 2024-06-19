using ChineseNetflix.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChineseNetflix.Areas.Identity.Data;

public class IdentityContext(DbContextOptions<IdentityContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        var serverVersion = new MySqlServerVersion(new Version(8, 3, 0));
        const string connectionString = "server=localhost;user=root;password=;database=ChineseNetflix";
        optionsBuilder.UseMySql(connectionString, serverVersion);
    }
}
