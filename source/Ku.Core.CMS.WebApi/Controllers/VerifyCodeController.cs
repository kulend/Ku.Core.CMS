using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ku.Core.Cache;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Web.Base;
using Ku.Core.Tools.VerificationCode;

namespace Ku.Core.CMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class VerifyCodeController : WebApiController
    {
        private ICacheProvider _cacheService;
        private readonly IVerificationCodeGen _verificationCodeGen;

        public VerifyCodeController(ICacheProvider cacheService, 
            IVerificationCodeGen verificationCodeGen)
        {
            this._cacheService = cacheService;
            this._verificationCodeGen = verificationCodeGen;
        }

        [HttpGet]
        public IActionResult Get(string key)
        {
            var ms = _verificationCodeGen.Create(out string code, 4);
            var cacheKey = string.Format(CacheKeyDefinition.VerifyCode, key);
            _cacheService.Set(cacheKey, code, TimeSpan.FromMinutes(10));
            Response.Body.Dispose();
            return File(ms.ToArray(), @"image/png");
        }
    }
}
