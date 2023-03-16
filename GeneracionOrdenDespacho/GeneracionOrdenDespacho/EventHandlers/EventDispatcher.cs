using System;
using System.Linq;
using System.Reflection;

namespace GeneracionOrdenDespacho.EventHandlers
{
    internal class EventDispatcher
    {
        public void Dispatch<TEvent>(TEvent eventData, IConfiguration configuration) where TEvent : class
        {
            var handler = typeof(IEventHandler<>);
            var handlerType = handler.MakeGenericType(eventData.GetType());
            var concreteTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.IsClass && x.GetInterfaces().Contains(handlerType))
                .ToList();
            if (!concreteTypes.Any())
            {
                return;
            }
            foreach (var concreteType in concreteTypes)
            {
                var concreteHandler = Activator.CreateInstance(concreteType) as IEventHandler<TEvent>;
                concreteHandler?.Handle(eventData, configuration);
            }
        }
    }
}
