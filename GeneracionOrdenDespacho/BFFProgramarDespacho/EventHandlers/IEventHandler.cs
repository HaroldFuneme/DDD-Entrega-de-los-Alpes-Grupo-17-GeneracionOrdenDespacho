namespace BFFProgramarDespacho.EventHandlers
{
    public interface IEventHandler<TEvent> where TEvent : class
    {
        void Handle(TEvent eventData, IConfiguration configuration);
    }
}
