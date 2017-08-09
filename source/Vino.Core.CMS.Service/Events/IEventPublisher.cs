using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vino.Core.CMS.Service.Events
{
    public interface IEventPublisher
    {
        /// <summary>
        /// Publish event
        /// </summary>
        void Publish<TEvent>(TEvent @event);

        /// <summary>
        /// Publish event
        /// </summary>
        Task PublishAsync<TEvent>(TEvent @event);
    }
}
