using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vino.Core.Communication.SMS
{
    public interface ISmsSender
    {
        Task<string> Send(SmsObject sms, IDictionary<string, string> parms);
    }
}
