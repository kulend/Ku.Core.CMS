using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.Cache.Redis
{
    public class RedisConfig
    {
        public string ConnectionString { set; get; }

        public string ApplicationKey { set; get; }
    }
}
