using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vino.Core.CMS.Web.Base;
using Vino.Core.Infrastructure.Exceptions;

namespace Vino.Core.CMS.WebApi.Controllers.V1.Content
{
    [ApiVersion("1.0")]
    [Route("api/content/[controller]")]
    public class ArticleController : WebApiController
    {
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            throw new VinoArgNullException();
        }

    }
}
