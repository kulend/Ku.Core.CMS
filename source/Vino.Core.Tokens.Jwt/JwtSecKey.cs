using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.Tokens.Jwt
{
    public class JwtSecKey
    {
        public string Key { get; set; }

        public int ExpiredMinutes { get; set; } = 30;
    }
}
