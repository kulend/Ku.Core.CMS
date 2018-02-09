using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.Infrastructure.Extensions
{
    public static class ObjectExtension
    {
        public static string JsonSerialize(this object obj)
        {
            if (obj == null)
            {
                return "";
            }
            return JsonConvert.SerializeObject(obj);
        }
    }
}
