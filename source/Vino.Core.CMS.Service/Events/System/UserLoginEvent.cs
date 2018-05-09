using System;
using System.Collections.Generic;
using System.Text;
using Ku.Core.EventBus;

namespace Ku.Core.CMS.Service.Events.System
{
    public class UserLoginEvent : BaseEvent
    {
        public long UserId { set; get; }
    }
}
