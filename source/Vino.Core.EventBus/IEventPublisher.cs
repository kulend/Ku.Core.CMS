using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ku.Core.EventBus
{
    public interface IEventPublisher
    {
        /// <summary>
        /// 发布消息
        /// </summary>
        void Publish<TEvent>(TEvent @event);

        /// <summary>
        /// 发布消息
        /// </summary>
        void Publish<TEvent>(string name, TEvent @event);

        /// <summary>
        /// 发布消息
        /// </summary>
        Task PublishAsync<TEvent>(TEvent @event);

        /// <summary>
        /// 发布消息
        /// </summary>
        Task PublishAsync<TEvent>(string name, TEvent @event);
    }
}
