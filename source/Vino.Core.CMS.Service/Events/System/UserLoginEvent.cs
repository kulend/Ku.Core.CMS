using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.EventBus;

namespace Vino.Core.CMS.Service.Events.System
{
    public class UserLoginEvent : BaseEvent
    {
        public long UserId { set; get; }
    }
}
