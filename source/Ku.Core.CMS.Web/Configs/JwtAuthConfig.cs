using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.CMS.Web.Configs
{
    public class JwtAuthConfig
    {
        public string CookieName { set; get; }

        public string LoginPath { set; get; }

        public string AccessDeniedPath { set; get; }
    }
}
