//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：RoleService.cs
// 功能描述：角色 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-16 11:27
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.CMS.IService.UserCenter;
using Dnc.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.UserCenter
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
                    var item = new {
                        //TODO:这里进行赋值
                    };
                    await dapper.UpdateAsync<Role>(item, new { model.Id });
                }
            }
        }

        /// <summary>
        /// 保存权限
        /// </summary>
        public async Task SaveRoleAuthAsync(long RoleId, long FunctionId, bool hasAuth)
        {
            using (var dapper = DapperFactory.Create())
            {
                var model = await dapper.QueryOneAsync<RoleFunction>(where: new { RoleId, FunctionId });
                if (hasAuth)
                {
                    if (model == null)
                    {
                        model = new RoleFunction();
                        model.RoleId = RoleId;
                        model.FunctionId = FunctionId;
                        await dapper.InsertAsync<RoleFunction>(model);
                    }
                }
                else if (model != null)
                {
                    await dapper.DeleteAsync<RoleFunction>(where: new { RoleId, FunctionId });
                }

            }
        }
    }
}
