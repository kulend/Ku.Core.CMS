using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vino.Core.Cache;
using Vino.Core.Cache.Redis;
using Vino.Core.Infrastructure.DependencyResolver;

namespace Vino.Core.CMS.Service.Events
{
    public class EventPublisher : IEventPublisher
    {
        private readonly ICacheService _cache;
        private readonly IIocResolver _ioc;
        public EventPublisher(ICacheService cache, IIocResolver ioc)
        {
            this._cache = cache;
            this._ioc = ioc;
        }

        ///// <summary>
        ///// Publish to cunsumer
        ///// </summary>
        //protected virtual void PublishToConsumer<TEvent>(IEventSubscriber<TEvent> x, TEvent @event)
        //{
        //    try
        //    {
        //        x.Handle(@event);
        //    }
        //    catch (Exception exc)
        //    {
        //        //log error
        //        //var logger = EngineContext.Current.Resolve<ILogger>();
        //        ////we put in to nested try-catch to prevent possible cyclic (if some error occurs)
        //        //try
        //        //{
        //        //    logger.Error(exc.Message, exc);
        //        //}
        //        //catch (Exception)
        //        //{
        //        //    //do nothing
        //        //}
        //    }
        //}

        /// <summary>
        /// Publish event
        /// </summary>
        public virtual void Publish<TEvent>(TEvent @event)
        {
            var database = RedisConnectionMultiplexer.Instance.GetDefaultDataBase();
            var subscribers = database.Multiplexer.GetSubscriber();
            subscribers.Publish(typeof(TEvent).FullName, JsonConvert.SerializeObject(@event));
            //var subscribers = GetSubscribers<TEvent>();
            //subscribers.ToList().ForEach(x => PublishToConsumer(x, @event));
        }

        /// <summary>
        /// Publish event
        /// </summary>
        public virtual async Task PublishAsync<TEvent>(TEvent @event)
        {
            var database = RedisConnectionMultiplexer.Instance.GetDefaultDataBase();
            var subscribers = database.Multiplexer.GetSubscriber();
            await subscribers.PublishAsync(typeof(TEvent).FullName, JsonConvert.SerializeObject(@event));
        }

        //private IEnumerable<IEventSubscriber<T>> GetSubscribers<T>()
        //{
        //    return _ioc.ResolveAll<IEventSubscriber<T>>();
        //}
    }
}
