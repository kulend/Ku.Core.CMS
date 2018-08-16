using Dnc.Api.Throttle;
using Ku.Core.Cache;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Domain.Dto.Communication;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.CMS.IService.Communication;
using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Frontend.PcSite.Pages.Account
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class RegisterModel : BasePcPage
    {
        private readonly IUserService _service;
        private readonly ISmsService _smsService;
        private Cache.ICacheProvider _cache;

        public RegisterModel(IUserService service, ISmsService smsService, Cache.ICacheProvider cache)
        {
            _service = service;
            _smsService = smsService;
            _cache = cache;
        }

        public void OnGet()
        {

        }

        [RateValve(Limit = 5, Duration = 60, Policy = Policy.Ip)]
        [RateValve(Limit = 1, Duration = 60, Policy = Policy.Form, PolicyKey = "mobile")]
        public async Task<IActionResult> OnPostSendCodeAsync([Required]string mobile)
        {
            //取得用户信息
            var user = await _service.GetOneAsync(new UserSearch { Mobile = mobile });
            if (user != null)
            {
                throw new KuException("当前手机号已被注册！");
            }

            //生成随机数
            var code = CodeHelper.Create(6, CodeLetterType.Number);

            //保存到缓存
            await _cache.SetAsync(string.Format(CacheKeyDefinition.SmsVerifyCode_Register, mobile), code, TimeSpan.FromMinutes(30));

            //发送短信
            await _smsService.AddAsync(mobile, "vcode", new { code });

            return Json(true);
        }
    }
}