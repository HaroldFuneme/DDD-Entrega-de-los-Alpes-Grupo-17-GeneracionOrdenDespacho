namespace GeneracionOrdenDespacho.ViewModels
{
    public class EventoInventarioVerificado : IEventoIntegracion<PayloadEventoInventarioVerificado>
    {
        public long eventId { get; set; }
        public Guid? ordenId { get; set; }
        public string eventName { get; set; }
        public string eventDataFormat { get; set; }
        public PayloadEventoInventarioVerificado payload { get; set; }

    }
}
