using Microsoft.EntityFrameworkCore;
using MeuDiarioSENAC.Model;

namespace MeuDiarioSENAC.Data;

public class MeuDiarioSENACContext : DbContext
{
    public DbSet<Registro> Registros { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    private const string ConnectionStringPadrao =
        "server=localhost;database=MeuDiarioSENAC;uid=root;pwd=S&nac2024;";

    public MeuDiarioSENACContext()
    {
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }

        string connectionString = Environment.GetEnvironmentVariable("MEUDIARIOSENAC_CONNECTION_STRING")
            ?? ConnectionStringPadrao;

        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Registros)
            .WithOne(r => r.Usuario)
            .HasForeignKey(r => r.UsuarioId);
    }
}