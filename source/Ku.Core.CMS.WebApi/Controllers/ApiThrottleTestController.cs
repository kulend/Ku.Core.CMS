using Dnc.Api.Throttle;
using Ku.Core.CMS.Web.Base;
using Ku.Core.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ku.Core.CMS.WebApi.Controllers
{
    [Route("api/apithrottle/test")]
    public class ApiThrottleTestController : WebApiController
    {
        private readonly IApiThrottleService _service;

        public ApiThrottleTestController(IApiThrottleService service)
        {
            _service = service;
        }

        [HttpGet("1")]
        [RateValve(Limit = 1, Duration = 60, Policy = Policy.Ip)]
        public bool Get1()
        {
            return true;
        }

        [HttpGet("2")]
        [RateValve(Limit = 5, Duration = 60, Policy = Policy.Header, PolicyKey = "p", WhenNull = WhenNull.Pass)]
        public IActionResult Get2()
        {
            return Json(true);
        }

        [HttpPost("3")]
        public async Task<IActionResult> Post1()
        {
            await _service.AddBlackListAsync(Policy.Ip, TimeSpan.FromSeconds(60), HttpContext.IpAddress());
            var data = await _service.GetBlackListAsync(Policy.Ip);
            return Json(data);
        }
    }
}
