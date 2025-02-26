using Microsoft.EntityFrameworkCore;
using Exemplo1.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Exemplo1.Data
{
    public class AppDbContext : DbContext   // Herda a classe principal do Entity Framework
    {
        public DbSet<Usuario> Usuarios { get; set; }    // Define o DbSet para o modelo Usuario (tabela)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // Configurações do banco de dados
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseNpgsql(config.GetConnectionString("PostgresConnection"));
        }

        // Método para configurar o mapeamento de entidades(tabelas)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("usuarios"); // Mapeia a tabela usuários
            // Garante que a tabela usuarios vai ser usada para a tabela usuarios do banco de dados
        }
    }
}