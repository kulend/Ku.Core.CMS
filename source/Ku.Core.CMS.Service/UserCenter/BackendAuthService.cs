using Ku.Core.Cache;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.UserCenter
{
    public class BackendAuthService : IBackendAuthService
    {
        protected readonly ICacheService _cache;

        #region 构造函数

        public BackendAuthService(ICacheService cache)
        {
            this._cache = cache;
        }

        #endregion

        /// <summary>
        /// 权限认证
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="authCode"></param>
        /// <returns></returns>
        public async Task<bool> CheckUserAuthAsync(long userId, string authCode)
        {
            if (authCode.IsNullOrEmpty())
            {
                return true;
            }
            var key = string.Format(CacheKeyDefinition.UserAuthCode, userId);
            var authcodes = _cache.Get<List<string>>(key);
            if (authcodes == null)
            {
                //取得用户所有权限码
                List<string> codes = new List<string>();
                //取得所有角色
                IEnumerable<UserRole> roles = null;
                using (var dapper = DapperFactory.Create())
                {
                    var sql = "SELECT t1.*, t2.* FROM usercenter_user_role t1 INNER JOIN usercenter_role t2 ON t1.RoleId=t2.Id AND t2.IsEnable=@IsEnable WHERE t1.UserId=@UserId";
                    roles = await dapper.QueryAsync<UserRole, Role, UserRole>(sql, (t1, t2) =>
                    {
                        t1.Role = t2;
                        return t1;
                    }, param: new { IsEnable = true, UserId = userId }, buffered: true, splitOn: "Id");

                    authcodes = new List<string>();
                    if (roles != null && roles.Any(x => x.Role.NameEn.Equals("ku.developer")))
                    {
                        authcodes.Add("ku.develop");
                    }
                    else if (roles != null)
                    {
                        foreach (var role in roles)
                        {
                            var cds = await GetRoleAuthCodesAsync(dapper, role.RoleId);
                            codes.AddRange(cds);
                        }
                        //去重
                        authcodes = codes.Distinct().ToList();
                    }

                    //缓存
                    _cache.Add(key, authcodes);
                }
            }
            if (authcodes.Contains("vino.develop"))
            {
                return true;
            }
            //验证
            //去除空格
            string tagnobk = authCode.Replace(" ", "");
            //去除括号和操作符
            string tagnofh = tagnobk.Replace("(", ",").Replace(")", ",").Replace("&&", ",").Replace("||", ",");
            //取得所有code
            string[] codeSplits = tagnofh.Split(',');
            if (codeSplits.Length == 1)
            {
                return authcodes.Contains(codeSplits[0]);
            }
            //TODO:以后扩展
            //foreach (var cd in codeSplits)
            //{
            //    if (cd.IsNullOrEmpty())
            //    {
            //        continue;
            //    }

            //    bool auth = authcodes.Contains(cd);
            //    if (auth)
            //    {
            //        Regex reg = new Regex(cd);
            //        tagnobk = reg.Replace(tagnobk, "true", 1);
            //    }
            //    else
            //    {
            //        Regex reg = new Regex(cd);
            //        tagnobk = reg.Replace(tagnobk, "false", 1);
            //    }
            //}

            return authcodes.Contains(authCode);
        }

        private async Task<List<string>> GetRoleAuthCodesAsync(IDapper dapper, long roleId)
        {
            var sql = "SELECT t1.*, t2.* FROM usercenter_role_function t1 INNER JOIN system_function t2 ON t1.FunctionId=t2.Id AND t2.IsEnable=@IsEnable WHERE t1.RoleId=@RoleId";
            var items = await dapper.QueryAsync<RoleFunction, Domain.Entity.System.Function, RoleFunction>(sql, (t1, t2) =>
            {
                t1.Function = t2;
                return t1;
            }, param: new { IsEnable = true, RoleId = roleId }, buffered: true, splitOn: "Id");
            return items.Select(x => x.Function.AuthCode).ToList();
        }

        /// <summary>
        /// 取得用户AuthCodes
        /// </summary>
        public async Task<List<string>> GetUserAuthCodesAsync(long userId, bool encrypt = false)
        {
            var key = string.Format(encrypt ? CacheKeyDefinition.UserAuthCodeEncrypt : CacheKeyDefinition.UserAuthCode, userId);
            var authcodes = _cache.Get<List<string>>(key);
            if (authcodes == null)
            {
                //取得用户所有权限码
                List<string> codes = new List<string>();
                //取得所有角色
                IEnumerable<UserRole> roles = null;
                using (var dapper = DapperFactory.Create())
                {
                    var sql = "SELECT t1.*, t2.* FROM usercenter_user_role t1 INNER JOIN usercenter_role t2 ON t1.RoleId=t2.Id AND t2.IsEnable=@IsEnable WHERE t1.UserId=@UserId";
                    roles = await dapper.QueryAsync<UserRole, Role, UserRole>(sql, (t1, t2) =>
                    {
                        t1.Role = t2;
                        return t1;
                    }, param: new { IsEnable = true, UserId = userId }, buffered: true, splitOn: "Id");

                    authcodes = new List<string>();
                    if (roles != null && roles.Any(x => x.Role.NameEn.Equals("ku.developer")))
                    {
                        authcodes.Add("ku.develop");
                    }
                    else if (roles != null)
                    {
                        foreach (var role in roles)
                        {
                            var cds = await GetRoleAuthCodesAsync(dapper, role.RoleId);
                            codes.AddRange(cds);
                        }
                        //去重
                        authcodes = codes.Distinct().ToList();
                    }
                    if (encrypt)
                    {
                        authcodes = authcodes.Select(CryptHelper.EncryptMD5).ToList();
                    }
                    //缓存
                    _cache.Add(key, authcodes);
                }
            }
            return authcodes;
        }
    }
}
