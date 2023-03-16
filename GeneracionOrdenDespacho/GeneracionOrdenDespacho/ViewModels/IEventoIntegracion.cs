namespace GeneracionOrdenDespacho.ViewModels
{
    public interface IEventoIntegracion<T>
    {
        public long eventId { get; set; }
        public string eventName { get; set; }
        public string eventDataFormat { get; set; }

        public Guid? ordenId { get; set; }

        public T payload { get; set; }

    }
}
