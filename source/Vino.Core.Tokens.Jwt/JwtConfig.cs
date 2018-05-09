using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.Tokens.Jwt
{
    public class JwtConfig
    {
        public string Key { get; set; }

        public int ExpiredMinutes { get; set; } = 30;

        public string Issuer { set; get; }

        public string Audience { set; get; }
    }
}
