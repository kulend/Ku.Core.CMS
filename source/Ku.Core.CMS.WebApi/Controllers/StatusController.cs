using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ku.Core.CMS.Web.Base;

namespace Ku.Core.CMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class StatusController : WebApiController
    {
        [HttpGet]
        public bool Get()
        {
            return true;
        }
    }
}
