using GeneracionOrdenDespacho.Persistencia.Modelos;
using GeneracionOrdenDespacho.Persistencia.Repositorios;
using GeneracionOrdenDespacho.ViewModels;
using System.Text.Json;

namespace GeneracionOrdenDespacho.EventHandlers
{
    internal class InventarioVerificadoCompensacionEventHandler : IEventHandler<EventoInventarioVerificadoCompensacion>
    {
        public async void Handle(EventoInventarioVerificadoCompensacion eventData, IConfiguration configuration)
        {
            Console.WriteLine($"Handling InventarioVerificadoCompensacion {eventData.eventId}");
            using (var db = new Persistencia.PersistenciaDbContext(configuration["MySQL:ConnectionString"]))
            {
                var repositorioSagaLog = new SagaLogRepositorio(db);
                var sagaLog = new SagaLog
                {
                    OrdenId = eventData.payload.ordenId,
                    EventoId = eventData.eventId,
                    Fecha = DateTime.Now,
                    NombreEvento = eventData.eventName,
                    PayloadSerializado = JsonSerializer.Serialize(eventData.payload),
                };
                repositorioSagaLog.AgregarLog(sagaLog);
            }
        }
    }
}
