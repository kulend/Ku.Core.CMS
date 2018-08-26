//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserMaterialGroupService.cs
// 功能描述：用户素材分组 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-08-18 11:31
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.MaterialCenter;
using Ku.Core.CMS.Domain.Entity.MaterialCenter;
using Ku.Core.CMS.IService.MaterialCenter;
using Dnc.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.MaterialCenter
{
    public partial class UserMaterialGroupService : BaseService<UserMaterialGroup, UserMaterialGroupDto, UserMaterialGroupSearch>, IUserMaterialGroupService
    {
        #region 构造函数

        public UserMaterialGroupService()
        {
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(UserMaterialGroupDto dto)
        {
            UserMaterialGroup model = Mapper.Map<UserMaterialGroup>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
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
                        //这里进行赋值
                        model.Name
                    };
                    await dapper.UpdateAsync<UserMaterialGroup>(item, new { model.Id });
                }
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public override async Task DeleteAsync(params long[] id)
        {
            using (var _dapper = DapperFactory.CreateWithTrans())
            {
                //删除分组素材关联
                await _dapper.DeleteAsync<UserMaterialGroupRef>(new DapperSql("GroupId IN @Ids", new { Ids = id }));
                await _dapper.DeleteAsync<UserMaterialGroup>(new DapperSql("Id IN @Ids", new { Ids = id }));
                _dapper.Commit();
            }
        }
    }
}
