namespace GeneracionOrdenDespacho.ViewModels
{
    public class EventoIntegracionDespachador : IEventoIntegracion<PayloadEventoIntegracionDespachador>
    {
        public long eventId { get; set; }
        public Guid? ordenId { get; set; }
        public string eventName { get; set; }
        public string eventDataFormat { get; set; }
        public PayloadEventoIntegracionDespachador payload { get; set; }

    }
}
