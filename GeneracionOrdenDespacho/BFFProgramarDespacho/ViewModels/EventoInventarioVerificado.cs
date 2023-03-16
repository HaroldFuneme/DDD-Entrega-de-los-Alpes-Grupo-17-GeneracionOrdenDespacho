namespace BFFProgramarDespacho.ViewModels
{
    public class EventoOrdenCreada
    {
        public long eventId { get; set; }
        public string eventName { get; set; }
        public string eventDataFormat { get; set; }
        public PayloadEventoOrdenCreada payload { get; set; }

    }
}
