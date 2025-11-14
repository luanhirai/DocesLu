using DocesLu.Model.Doces;
using DocesLu.Model.User;
using Microsoft.EntityFrameworkCore;
using System;

namespace DocesLu.Data
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Doces> Doces { get; set; }
        public DbSet<Auth> Auths { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = "Server=rds-docesdalu.cilgqiygo8zx.us-east-1.rds.amazonaws.com;Database=doceslu;User=admin;Password=luanhirai10!;Port=3306;";
                Console.WriteLine("[ConnectionContext] Usando string de conexão padrão (local ou fallback).");
            }
            else
            {
                Console.WriteLine("[ConnectionContext] Usando string de conexão da variável de ambiente.");
            }

            Console.WriteLine($"[ConnectionContext] Conectando ao banco com: {connectionString}");

            optionsBuilder
                .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)))
                // Habilita logging do EF Core (opcional, para ver queries SQL no console)
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                .EnableSensitiveDataLogging(); // mostra parâmetros das queries (atenção em produção)
        }
    }
}