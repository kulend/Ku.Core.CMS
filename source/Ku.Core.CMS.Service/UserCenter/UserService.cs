//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserService.cs
// 功能描述：用户 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-16 10:45
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.UserCenter
{
    public partial class UserService : BaseService<User, UserDto, UserSearch>, IUserService
    {
        #region 构造函数

        public UserService()
        {
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(UserDto dto)
        {
            User model = Mapper.Map<User>(dto);
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
                    await dapper.UpdateAsync<User>(item, new { model.Id });
                }
            }
        }
    }
}
