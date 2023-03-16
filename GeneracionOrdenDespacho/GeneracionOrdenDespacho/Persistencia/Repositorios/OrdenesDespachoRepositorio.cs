using GeneracionOrdenDespacho.Persistencia.Modelos;
using GeneracionOrdenDespacho.ViewModels;
using System.Text.Json;

namespace GeneracionOrdenDespacho.Persistencia.Repositorios
{
    internal class OrdenesDespachoRepositorio
    {
        private PersistenciaDbContext _db;
        private List<string> _listadoDeliveries;

        public OrdenesDespachoRepositorio(PersistenciaDbContext db)
        {
            _db = db;
            _listadoDeliveries = new List<string>()
            {
                "Servientrega",
                "Deprisa",
                "Propio",
            };
        }

        public (bool guardado, List<DespachoUltimaMilla> despachos) AgregarOrden(PayloadEventoInventarioVerificado eventoInventario)
        {
            if (_db.Ordenes.Find(eventoInventario.ordenId) == null)
            {
                var orden = new Orden
                {
                    OrdenId = eventoInventario.ordenId,
                    DireccionUsuario = eventoInventario.user_addres,
                    Usuario = eventoInventario.user
                };
                _db.Ordenes.Add(orden);

                var random = new Random();
                var retorno = new List<DespachoUltimaMilla>();
                if (eventoInventario.items_bodegas != null)
                {
                    foreach (var bodega in eventoInventario.items_bodegas)
                    {
                        var despachoBodega = new DespachoUltimaMilla
                        {
                            OrdenId = orden.OrdenId,
                            IdentificadorDelivery = _listadoDeliveries[random.Next(_listadoDeliveries.Count() - 1)],
                            NombreBodega = bodega.Key,
                            Items = JsonSerializer.Serialize(bodega.Value)
                        };
                        _db.DespachosUltimaMilla.Add(despachoBodega);
                        retorno.Add(despachoBodega);
                    }
                    try
                    {
                        if (_db.SaveChanges() > 0)
                        {
                            return (true, retorno);
                        }
                        else
                        {
                            return (false, null);
                        }
                    }
                    catch
                    {
                        return (false, null);
                    }
                }
                else
                {
                    return (true, null);
                }
            }
            else
            {
                return (true, null);
            }
        }
    }
}
