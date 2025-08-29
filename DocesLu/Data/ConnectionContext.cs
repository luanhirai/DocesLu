using DocesLu.Model.Doces;
using DocesLu.Model.User;
using Microsoft.EntityFrameworkCore;

namespace DocesLu.Data
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Doces> Doces { get; set; }
        public DbSet<Auth> Auths { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseMySql(
                "Server=localhost;" +
                "Database=docesdalu;" +
                "User=root;" +
                "Password=123;",
                new MySqlServerVersion(new Version(8, 0, 36))
            );

    }
}
