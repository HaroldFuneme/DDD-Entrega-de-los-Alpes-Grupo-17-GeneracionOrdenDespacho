namespace BFFProgramarDespacho.ViewModels
{
    public class EventoInventarioVerificado
    {
        public long eventId { get; set; }
        public string eventName { get; set; }
        public string eventDataFormat { get; set; }
        public PayloadEventoInventarioVerificado payload { get; set; }

    }
}
