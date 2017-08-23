using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.Tools.VerificationCode;

namespace Vino.Core.CMS.Web.Admin.Controllers
{
    public class ValidateController: Controller
    {
        private readonly IVerificationCodeGen _verificationCodeGen;

        public ValidateController(IVerificationCodeGen verificationCodeGen)
        {
            this._verificationCodeGen = verificationCodeGen;
        }

        /// <summary>
        /// 图形验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult ImageCode(string type)
        {
            var ms = _verificationCodeGen.Create(out string code, 4);
            HttpContext.Session.SetString($"ImageValidateCode_{type}", code);
            Response.Body.Dispose();
            return File(ms.ToArray(), @"image/png");
        }
    }
}
