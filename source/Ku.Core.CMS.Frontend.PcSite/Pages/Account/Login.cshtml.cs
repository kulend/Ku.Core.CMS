using Ku.Core.CMS.IService.UserCenter;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Frontend.PcSite.Pages.Account
{
    public class LoginModel : BasePcPage
    {
        private readonly IUserService _service;

        public LoginModel(IUserService service)
        {
            _service = service;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(string mobile, string password)
        {
            //取得用户信息
            var user = await _service.MobileLoginAsync(mobile, password);
            if (user != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,user.NickName)
                    ,new Claim("HeadImage", user.HeadImage??"")
                    ,new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };
                var identity = new ClaimsIdentity();
                identity.AddClaims(claims);

                SignIn(new ClaimsPrincipal(identity), CookieAuthenticationDefaults.AuthenticationScheme);

                return Json(true);
            }
            else
            {
                throw new Exception("账号或密码出错！");
            }
        }
    }
}