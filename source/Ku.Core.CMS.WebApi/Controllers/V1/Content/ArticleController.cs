using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ku.Core.CMS.Web.Base;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.WebApi.Controllers.V1.Content
{
    [ApiVersion("1.0")]
    [Route("api/content/[controller]")]
    public class ArticleController : WebApiController
    {
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            throw new KuArgNullException();
        }

    }
}
