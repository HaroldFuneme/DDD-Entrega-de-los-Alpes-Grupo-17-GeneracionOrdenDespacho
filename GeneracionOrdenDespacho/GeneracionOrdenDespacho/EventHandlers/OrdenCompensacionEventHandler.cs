using GeneracionOrdenDespacho.Persistencia.Modelos;
using GeneracionOrdenDespacho.Persistencia.Repositorios;
using GeneracionOrdenDespacho.ViewModels;
using System.Text.Json;

namespace GeneracionOrdenDespacho.EventHandlers
{
    internal class OrdenCompensacionEventHandler : IEventHandler<EventoOrdenCompensacion>
    {
        public async void Handle(EventoOrdenCompensacion eventData, IConfiguration configuration)
        {
            Console.WriteLine($"Handling OrdenCompensacion {eventData.eventId}");
            using (var db = new Persistencia.PersistenciaDbContext(configuration["MySQL:ConnectionString"]))
            {
                var repositorio = new SagaLogRepositorio(db);
                var sagaLog = new SagaLog
                {
                    OrdenId = eventData.payload.ordenId,
                    EventoId = eventData.eventId,
                    Fecha = DateTime.Now,
                    NombreEvento = eventData.eventName,
                    PayloadSerializado = JsonSerializer.Serialize(eventData.payload),
                };
                var resultado = repositorio.AgregarLog(sagaLog);
            }
        }
    }
}
