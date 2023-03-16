using GeneracionOrdenDespacho.Persistencia.Modelos;
using GeneracionOrdenDespacho.Persistencia.Repositorios;
using GeneracionOrdenDespacho.ViewModels;
using System.Text.Json;

namespace GeneracionOrdenDespacho.EventHandlers
{
    internal class InventarioVerificadoEventHandler : IEventHandler<EventoInventarioVerificado>
    {
        public async void Handle(EventoInventarioVerificado eventData, IConfiguration configuration)
        {
            Console.WriteLine($"Handling InventarioVerificado {eventData.eventId}");
            using (var db = new Persistencia.PersistenciaDbContext(configuration["MySQL:ConnectionString"]))
            {
                var repositorio = new OrdenesDespachoRepositorio(db);
                var resultado = repositorio.AgregarOrden(eventData.payload);
                if (resultado.guardado)
                {
                    if (resultado.despachos != null)
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
                        foreach (var despacho in resultado.despachos)
                        {
                            var payloadDespachador = new EventoIntegracionDespachador()
                            {
                                eventDataFormat = "JSON",
                                eventId = DateTime.Now.Ticks,
                                eventName = "EventoIntegracionDespachador",
                                payload = new PayloadEventoIntegracionDespachador
                                {
                                    delivery_address = despacho.Orden.DireccionUsuario,
                                    ordenId = despacho.OrdenId,
                                    items = despacho.Items,
                                    user = despacho.Orden.Usuario,
                                    user_addres = despacho.Orden.DireccionUsuario,
                                }
                            };
                            var messageId = await HelperPulsarBroker.SendMessage(configuration["Pulsar:Uri"], configuration["Pulsar:topico-integracion-despachadores"], configuration["Pulsar:Subscription"], payloadDespachador);
                            var sagaLogDespacho = new SagaLog
                            {
                                OrdenId = eventData.payload.ordenId,
                                EventoId = eventData.eventId,
                                Fecha = DateTime.Now,
                                NombreEvento = "EventoIntegracionDespachador",
                                PayloadSerializado = JsonSerializer.Serialize(payloadDespachador.payload),
                            };
                            repositorioSagaLog.AgregarLog(sagaLogDespacho);
                        }
                    }
                }
                else /// Hubo una excepción y hay que reversar la transacción
                {
                    eventData.eventName += "Compensacion";
                    var messageId = await HelperPulsarBroker.SendMessage(configuration["Pulsar:Uri"], configuration["Pulsar:topico-verifica-inventario-compensacion"], configuration["Pulsar:Subscription"], eventData);
                }
            }
        }
    }
}
