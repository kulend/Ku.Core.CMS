using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.CMS.Service.Events
{
    public interface IEventSubscriber
    {
        void Subscribe<TEvent>(Action<TEvent> handler);
    }
}
