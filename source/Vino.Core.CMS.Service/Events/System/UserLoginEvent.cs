using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.CMS.Service.Events.System
{
    public class UserLoginEvent : BaseEvent
    {
        public long UserId { set; get; }
    }
}
