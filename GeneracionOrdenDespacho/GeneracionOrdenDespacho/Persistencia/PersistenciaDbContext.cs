using GeneracionOrdenDespacho.Persistencia.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GeneracionOrdenDespacho.Persistencia
{
    internal class PersistenciaDbContext : DbContext
    {
        private string _connectionString;

        public PersistenciaDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<DespachoUltimaMilla> DespachosUltimaMilla { get; set; }
        public DbSet<SagaLog> SagaLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }
    }
}
