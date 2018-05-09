using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ku.Core.EventBus
{
    public class EmptyEventPublisher : IEventPublisher
    {
        public void Publish<TEvent>(TEvent @event)
        {
        }

        public void Publish<TEvent>(string name, TEvent @event)
        {
        }

        public Task PublishAsync<TEvent>(TEvent @event)
        {
            return Task.CompletedTask;
        }

        public Task PublishAsync<TEvent>(string name, TEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
