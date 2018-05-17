//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：RoleService.cs
// 功能描述：角色 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using Ku.Core.CMS.IService.System;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.System
{
    public partial class RoleService : BaseService<Role, RoleDto, RoleSearch>, IRoleService
    {
        #region 构造函数

        public RoleService()
        {
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(RoleDto dto)
        {
            Role model = Mapper.Map<Role>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;
                using (var dapper = DapperFactory.Create())
                {
                    await dapper.InsertAsync(model);
                }
            }
            else
            {
                //更新
                using (var dapper = DapperFactory.Create())
                {
                    var item = new
                    {
                        //这里进行赋值
                        model.Name,
                        model.NameEn,
                        model.IsEnable,
                        model.Remarks
                    };
                    await dapper.UpdateAsync<Role>(item, new { model.Id });
                }
            }
        }

        #region 功能权限

        /// <summary>
        /// 取得功能列表（附带角色是否有权限）
        /// </summary>
        public async Task<List<FunctionDto>> GetFunctionsWithRoleAuthAsync(long roleId, long? parentFunctionId)
        {
            //using (var dapper = DapperFactory.Create())
            //{
            //    //取得功能列表
            //    var functions = Mapper.Map<List<FunctionDto>>(await dapper.QueryListAsync<Function>(new { ParentId = parentFunctionId }));

            //    //取得角色功能列表
            //    var roleFunctions = await dapper.QueryListAsync<RoleFunction>(new { RoleId = roleId });

            //    foreach (var function in functions)
            //    {
            //        if (roleFunctions.Any(x => x.FunctionId == function.Id))
            //        {
            //            function.IsRoleHasAuth = true;
            //        }
            //    }

            //    return functions;
            //}
            return null;
        }

        public async Task SaveRoleAuthAsync(long RoleId, long FunctionId, bool hasAuth)
        {
            //using (var dapper = DapperFactory.Create())
            //{
            //    var model = await dapper.QueryOneAsync<RoleFunction>(where: new { RoleId, FunctionId });
            //    if (hasAuth)
            //    {
            //        if (model == null)
            //        {
            //            model = new RoleFunction();
            //            model.RoleId = RoleId;
            //            model.FunctionId = FunctionId;
            //            await dapper.InsertAsync<RoleFunction>(model);
            //        }
            //    }
            //    else if (model != null)
            //    {
            //        await dapper.DeleteAsync<RoleFunction>(where: new { RoleId, FunctionId });
            //    }

            //}
        }

        #endregion
    }
}
