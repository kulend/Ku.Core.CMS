using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Vino.Core.Cache.Redis;

namespace Vino.Core.CMS.Service.Events
{
    /// <summary>
    /// 事件消息订阅
    /// </summary>
    public class EventSubscriber: IEventSubscriber
    {
        public EventSubscriber()
        {

        }

        public void Subscribe<TEvent>(Action<TEvent> handler)
        {
            var database = RedisConnectionMultiplexer.Instance.GetDefaultDataBase();
            var subs = database.Multiplexer.GetSubscriber();
            subs.Subscribe(typeof(TEvent).FullName, (channel, value) =>
            {
                handler?.Invoke(JsonConvert.DeserializeObject<TEvent>(value));
            });
        }
    }
}
