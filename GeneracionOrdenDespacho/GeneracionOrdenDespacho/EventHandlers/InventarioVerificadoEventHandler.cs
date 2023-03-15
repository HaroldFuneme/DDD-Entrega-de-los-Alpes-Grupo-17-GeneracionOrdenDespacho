using GeneracionOrdenDespacho.Persistencia.Repositorios;
using GeneracionOrdenDespacho.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneracionOrdenDespacho.EventHandlers
{
    internal class EventoBaseHandler : IEventHandler<EventoInventarioVerificado>
    {
        public async void Handle(EventoInventarioVerificado eventData, IConfiguration configuration)
        {
            Console.WriteLine($"Handling Orden {eventData.eventId}");
            using (var db = new Persistencia.PersistenciaDbContext(configuration["MySQL:ConnectionString"]))
            {
                var repositorio = new OrdenesDespachoRepositorio(db);
                var resultado = repositorio.AgregarOrden(eventData.payload);
                if (resultado.guardado)
                {
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
                        var messageId = await HelperPulsarBroker.SendMessage(configuration["Pulsar:Uri"], configuration["Pulsar:TopicoIntegracionDespachadores"], configuration["Pulsar:Subscription"], payloadDespachador);
                    }
                }
            }
        }
    }
}
