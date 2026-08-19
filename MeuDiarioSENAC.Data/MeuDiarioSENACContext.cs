using Microsoft.EntityFrameworkCore;

class MeuDiarioSENACContext : DbContext
{
    public DbSet<Registro> Registros { get; set; }
    private readonly string connectionString =
        "server=localhost;database=MeuDiarioSENAC;uid=root;pwd=S&nac2024;";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString));
    }
}