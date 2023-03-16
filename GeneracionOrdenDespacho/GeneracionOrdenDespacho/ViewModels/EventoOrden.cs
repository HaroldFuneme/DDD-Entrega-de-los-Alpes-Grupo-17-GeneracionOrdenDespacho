namespace GeneracionOrdenDespacho.ViewModels
{
    public class EventoOrden : IEventoIntegracion<PayloadEventoOrden>
    {
        public long eventId { get; set; }
        public Guid? ordenId { get; set; }
        public string eventName { get; set; }
        public string eventDataFormat { get; set; }
        public PayloadEventoOrden payload { get; set; }
    }
}
