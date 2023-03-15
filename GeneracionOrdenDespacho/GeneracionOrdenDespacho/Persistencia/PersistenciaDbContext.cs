using GeneracionOrdenDespacho.Persistencia.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }
    }
}
