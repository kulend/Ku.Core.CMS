using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.CAP;

namespace Vino.Core.EventBus.CAP
{
    public class CapEventPublisher: IEventPublisher
    {
        private ICapPublisher _publisher;

        public CapEventPublisher(ICapPublisher publisher)
        {
            this._publisher = publisher;
        }

        public void Publish<TEvent>(TEvent @event)
        {
            var name = typeof(TEvent).FullName;
            var attr = typeof(TEvent).GetTypeInfo().GetCustomAttribute<EventAttribute>();
            if (attr != null)
            {
                name = attr.Name;
            }
            Publish(name, @event);
        }

        public void Publish<TEvent>(string name, TEvent @event)
        {
            try
            {
                _publisher.Publish<TEvent>(name, @event);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task PublishAsync<TEvent>(string name, TEvent @event)
        {
            try
            {
                await _publisher.PublishAsync(name, @event);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }   
        }

        public async Task PublishAsync<TEvent>(TEvent @event)
        {
            var name = typeof(TEvent).FullName;
            var attr = typeof(TEvent).GetTypeInfo().GetCustomAttribute<EventAttribute>();
            if (attr != null)
            {
                name = attr.Name;
            }
            await PublishAsync(name, @event);
        }
    }
}
