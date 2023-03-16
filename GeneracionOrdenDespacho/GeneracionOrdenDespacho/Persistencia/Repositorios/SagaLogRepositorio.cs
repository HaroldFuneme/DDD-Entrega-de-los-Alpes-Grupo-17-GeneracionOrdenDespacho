using GeneracionOrdenDespacho.Persistencia.Modelos;

namespace GeneracionOrdenDespacho.Persistencia.Repositorios
{
    internal class SagaLogRepositorio
    {
        private PersistenciaDbContext _db;

        public SagaLogRepositorio(PersistenciaDbContext db)
        {
            _db = db;
        }

        public bool AgregarLog(SagaLog eventoLog)
        {
            _db.SagaLog.Add(eventoLog);
            return _db.SaveChanges() > 0;
        }

        public IQueryable<SagaLog> ListarPorOrden(Guid ordenId)
        {
            return _db.SagaLog.Where(x => x.OrdenId == ordenId);

        }
    }
}
