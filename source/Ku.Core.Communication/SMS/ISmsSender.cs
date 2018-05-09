using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ku.Core.Communication.SMS
{
    public interface ISmsSender
    {
        Task<(bool success, string response)> Send(SmsObject sms);
    }
}
